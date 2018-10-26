using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Obsolete
{
    interface IQuery<TQuery, TResult>
    {
        TResult Get(TQuery query);
    }

    /// <summary>
    /// Simply caches newly created objects, and returns the cached one for identical subsequent queries
    /// </summary>
    /// <typeparam name="TQuery">Type of the query that maps to the result.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    class CachingProxy<TQuery, TResult> : IQuery<TQuery, TResult>
    {
        private IQuery<TQuery, TResult> _resolver;
        private Dictionary<TQuery, TResult> _cache;

        public CachingProxy(IQuery<TQuery, TResult> resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _cache = new Dictionary<TQuery, TResult>();
        }

        public TResult Get(TQuery query)
        {
            if ( ! _cache.TryGetValue(query, out TResult result))
            {
                result = _resolver.Get(query);
                _cache.Add(query, result);
            }

            return result;
        }
    }

    /// <summary>
    /// Just makes mock component
    /// </summary>
    class MockComponentFactory : IQuery<string, Component>
    {
        public Component Get(string subtypeId) =>
            new Component(
                    new ItemId(
                        "Component",
                        subtypeId),
                    "Mock Component",
                    @"Media\mystery_component_icon.jpg",
                    new ItemSize(
                        0.2f, 
                        0.2f, 
                        0.2f),
                    10, /*Mass*/
                    20, /*Volume*/
                    "MockModel",
                    "MockMaterial",
                    30,
                    1f,
                    40);
    }

    /// <summary>
    /// Just makes a mock block
    /// </summary>
    class MockBlockFactory : IQuery<string, Block>
    {
        public Block Get(string subtypeId) =>
            new Block(
                    new ItemId(
                        "Component",
                        subtypeId),
                    "Mock Component",
                    @"Media\mystery_component_icon.jpg",
                    CubeSize.Large,
                    BlockTopology.Cube,
                    new BlockSize(1,1,1),
                    new BlockModelOffset(0,0,0),
                    "MockModel",
                    new List<(string Subtype, int Count)> () { ("SteelPlate", 9) }
                    );
    }

    /// <summary>
    /// Provides a mock component when a query made for a given component ID.
    /// Provided mock components are stored, so subsequent queries for the same component ID return the same object.
    /// </summary>
    public class MockComponentDefitions : IComponentDefinitionsProvider
    {
        private IQuery<string, Component> _resolver = new CachingProxy<string, Component>(new MockComponentFactory());
        
        public Component GetComponent(string componentSubtypeId)
            => _resolver.Get(componentSubtypeId);
    }

    /// <summary>
    /// Provides a mock block when a query made for a given block ID.
    /// Provided mock blocks are stored, so subsequent queries for the same block ID return the same object.
    /// </summary>
    public class MockBlockDefinitions : IBlockDefinitionsProvider
    {
        private IQuery<string, Block> _resolver = new CachingProxy<string, Block>(new MockBlockFactory());

        public Block GetBlock(string blockSubtypeId)
            => _resolver.Get(blockSubtypeId);
    }

    public class MockIconProvider : IIconProvider
    {
        public string GetIconPath(ItemId id)
        {
            return @"Media\mystery_component_icon.jpg";
        }
    }
}
