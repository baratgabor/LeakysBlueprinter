using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        /// <summary>
        /// Base class for creating concrete builder classes
        /// </summary>
        public abstract class BuilderBase<TParent, TDerivedSelf> : IXElementBuilder
            where TParent : IXElementBuilder
            where TDerivedSelf : BuilderBase<TParent, TDerivedSelf>
        {
            private XElement _data; // Private: Concrete implementations should use helper methods to modify _data
            protected TParent _parent;

            public BuilderBase(TParent parent, string dataTemplate)
            {
                _parent = parent;
                _data = XElement.Parse(dataTemplate);
            }

            /// <summary>
            /// Creates or updates an element at the specified location.
            /// </summary>
            /// <param name="elementPath">XPath compatible path of element, without element name.</param>
            /// <param name="elementName">Name of the element.</param>
            /// <param name="elementValue">Value of the element.</param>
            protected void UpdateOrAddElementAt(string elementPath, string elementName, string elementValue)
            {
                var context = _data.XPathSelectElement(elementPath);
                var element = context.Element(elementName);
                if (element == null)
                    context.Add(XElement.Parse($"<{elementName}>{elementValue}</{elementName}>"));
                else
                    element.Value = elementValue;
            }

            /// <summary>
            /// Creates or updates an element at the root of this structure.
            /// </summary>
            /// <param name="elementName">Name of the element.</param>
            /// <param name="elementValue">Value of the element.</param>
            protected void UpdateOrAddElement(string elementName, string elementValue)
                => UpdateOrAddElementAt(".", elementName, elementValue);

            /// <summary>
            /// Adds or updates an element attribute at the specified location.
            /// If the element doesn't exist, adds a new element without element value.
            /// </summary>
            /// <param name="elementPath">XPath compatible path of the element that holds the attribute.</param>
            /// <param name="elementName">Name of the element that holds the attribute.</param>
            /// <param name="attributeName">Name of the attribute.</param>
            /// <param name="attributeValue">Value of the attribute.</param>
            protected void AddOrUpdateAttributeAt(string elementPath, string elementName, string attributeName, string attributeValue)
            {
                var context = _data.XPathSelectElement(elementPath);
                var element = context.Element(elementName);
                if (element == null)
                {
                    element = XElement.Parse($"<{elementName}/>");
                    context.Add(element);
                }
                element.SetAttributeValue(attributeName, attributeValue);
            }

            /// <summary>
            /// Adds or updates an element attribute at the root of this structure.
            /// If the element doesn't exist, adds a new element without element value.
            /// </summary>
            /// <param name="elementName">Name of the element that holds the attribute.</param>
            /// <param name="attributeName">Name of the attribute.</param>
            /// <param name="attributeValue">Value of the attribute.</param>
            protected void AddOrUpdateAttribute(string elementName, string attributeName, string attributeValue)
                => AddOrUpdateAttributeAt(".", elementName, attributeName, attributeValue);

            /// <summary>
            /// Adds a new child element to the parent element at the specified path.
            /// Throws <see cref="NullReferenceException"/> if parent element can't be found at the specified path.
            /// </summary>
            /// <param name="insertionPath">XPath compatible path of the element that should contain the new child element.</param>
            /// <param name="element">The element to be added as a child.</param>
            protected void AddNewChildAt(string insertionPath, XElement element)
                => _data.XPathSelectElement(insertionPath).Add(element);

            /// <summary>
            /// Adds a new child element to the root of this structure.
            /// </summary>
            /// <param name="element">The element to be added as a child.</param>
            protected void AddNewChild(XElement element)
                => AddNewChildAt(".", element);

            /// <summary>
            /// Exports the <see cref="XElement"/> data structure of the current builder
            /// without breaking the fluent method chain.
            /// </summary>
            public TDerivedSelf ExportThis(out XElement currentBuilderData)
            {
                currentBuilderData = _data;
                return (TDerivedSelf)this;
            }

            /// <summary>
            /// Finishes the current object, and returns to parent.
            /// </summary>
            public TParent ThatsAll()
                => _parent;

            /// <summary>
            /// Returns the full built <see cref="XElement"/> structure.
            /// </summary>
            public XElement Build()
                => _parent.Build();

            /// <summary>
            /// Returns the <see cref="XElement"/> equivalent of the current object.
            /// </summary>
            /// <returns></returns>
            public XElement BuildCurrent()
                => _data;

            public static implicit operator XElement(BuilderBase<TParent, TDerivedSelf> b)
                => b.Build();
        }
    }
}