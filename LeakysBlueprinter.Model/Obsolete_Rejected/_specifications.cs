using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Obsolete
{

    internal interface IMySpecification<TEntity, TResult>
    {
        Func<TEntity, bool> Where { get; }
        Func<TEntity, TResult> Select { get; }
        bool IsSatisfiedBy(TEntity t);
        //IEnumerable<TResult> GetResultsFrom(IEnumerable<TEntity> collection);
    }

    internal interface IMyRepository<TEntity>
    {
        // ...
        TResult GetSingle<TResult>(IMySpecification<TEntity, TResult> spec);
        IEnumerable<TResult> List<TResult>(IMySpecification<TEntity, TResult> spec);
    }

    internal class MyGenericRepository<T> : IMyRepository<T>
    {
        protected IEnumerable<T> _collection;

        public MyGenericRepository(IEnumerable<T> list)
            => _collection = list;

        // ...

        public TResult GetSingle<TResult>(IMySpecification<T, TResult> spec)
            => List(spec).Single();

        public IEnumerable<TResult> List<TResult>(IMySpecification<T, TResult> spec)
            => (IEnumerable<TResult>)_collection.Where(spec.Where).Select(spec.Select);
    }


    internal class MyRepository : IMyRepository<XElement>
    {
        private IEnumerable<XElement> _list;

        public MyRepository(IEnumerable<XElement> list)
            => _list = list;

        public TResult GetSingle<TResult>(IMySpecification<XElement, TResult> spec)
            => List(spec).Single();

        public IEnumerable<TResult> List<TResult>(IMySpecification<XElement, TResult> spec)
            => (IEnumerable<TResult>)_list.Where(spec.Where).Select(spec.Select);
    }


    public class LocalizationIdToNameLookup_Specification : IMySpecification<XElement, string>
    {
        public Func<XElement, bool> Where { get; }
        public Func<XElement, string> Select { get; }

        // Where to set name?
        private string _name;

        public LocalizationIdToNameLookup_Specification()
        {
            Where = ((el) => el.Attribute("name").Value.Contains(_name));
            Select = ((el) => el.Element("value").Value);
        }

        public IEnumerable<string> GetResultsFrom(IEnumerable<XElement> collection)
        {
            return (IEnumerable<string>)collection.AsQueryable().Where(Where).Select(Select);
        }

        public bool IsSatisfiedBy(XElement t)
            => Where(t);
    }






}
