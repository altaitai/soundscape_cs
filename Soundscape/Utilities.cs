using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Soundscape
{
    static class Utilities
    {
        public static string GetAttributeValue(XmlNode node, string attrName)
        {
            XmlAttribute attr = node.Attributes[attrName];
            if (attr == null)
            {
                throw new Exception($"{attrName} attribute not found");
            }
            return attr.Value;
        }

        public static void CreateAttribute(XmlDocument doc, XmlNode node, string attrName, string attrValue)
        {
            XmlAttribute attr = doc.CreateAttribute(attrName);
            attr.Value = attrValue;
            node.Attributes.Append(attr);
        }
    }
}
