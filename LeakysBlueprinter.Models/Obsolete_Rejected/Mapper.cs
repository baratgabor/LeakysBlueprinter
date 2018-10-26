using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Obsolete
{
    internal interface IMapper<TIn, TOut>
    {
        TOut Map(TIn elem);
    }

    internal class BlockMapper : IMapper<XElement, Block>
    {
        public Block Map(XElement elem)
        {
            throw new NotImplementedException();
        }
    }

    internal class ComponentMapper : IMapper<XElement, Component>
    {
        public Component Map(XElement e)
            => new Component(
                id: new ItemId(
                    typeID: e.Element("Id").Element("TypeId").Value,
                    subtypeID: e.Element("Id").Element("SubtypeId").Value),
                displayName: e.Element("DisplayName").Value,
                icon: e.Element("Icon").Value,
                size: new ItemSize(
                    x: float.Parse(e.Element("Size").Element("X").Value),
                    y: float.Parse(e.Element("Size").Element("Y").Value),
                    z: float.Parse(e.Element("Size").Element("Z").Value)),
                mass: int.Parse(e.Element("Mass").Value),
                volume: int.Parse(e.Element("Volume").Value),
                model: e.Element("Model").Value,
                physicalMaterial: e.Element("PhysicalMaterial").Value,
                maxIntegrity: int.Parse(e.Element("MaxIntegrity").Value),
                dropProbability: float.Parse(e.Element("DropProbability").Value),
                health: int.Parse(e.Element("Health").Value)
                );
    }
}
