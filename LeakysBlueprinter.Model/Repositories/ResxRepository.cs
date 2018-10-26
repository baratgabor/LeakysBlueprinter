using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    internal class ResxRepository : IRepository<string, string>
    {
        protected IEnumerable<XElement> _collection;

        public ResxRepository(Stream resxStream)
        {
            _collection = LoadAndParse(resxStream);
        }

        /// <summary>
        /// Returns localized value corresponding to the provided ID.
        /// Returns the same ID if value not found.
        /// </summary>
        /// <param name="id">The ID to look up.</param>
        public string GetById(string id)
        {
            var res = from e in _collection
                      where e.Attribute("name").Value == id
                      select e.Element("value").Value;

            if (res == null || !res.Any())
                return id;
            else
                return res.First(); // TODO: Consider using "Single()" if we're feeling lucky...
        }

        protected IEnumerable<XElement> LoadAndParse(Stream resxStream)
        {
            using (resxStream)
            {
                return XElement.Load(resxStream).Descendants("data");
            }
        }
    }
}