using System;
using System.Xml;
using System.IO;

namespace Soundscape
{
    class SoundFile
    {
        public string Filepath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SoundFile(string filepath)
        {
            if (Path.GetExtension(filepath).ToLower() != ".wav")
            {
                throw new Exception("Unsupported data type");
            }

            Filepath = filepath;
            Name = Path.GetFileNameWithoutExtension(filepath);
        }

        public void Parse(XmlNode node)
        {
            // validate node name
            if (node.Name != "sound")
            {
                throw new Exception("Invalid node name");
            }

            // parse attributes
            Filepath = Utilities.GetAttributeValue(node, "path");
            Name = Utilities.GetAttributeValue(node, "name");
            Description = Utilities.GetAttributeValue(node, "description");
        }

        public void Save(XmlDocument doc, XmlNode rootNode)
        {
            // create sound node
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "sound", string.Empty);

            // create path attribute and add to sound node
            Utilities.CreateAttribute(doc, node, "path", Filepath);
            Utilities.CreateAttribute(doc, node, "name", Name);
            Utilities.CreateAttribute(doc, node, "description", Description);

            // add sound node to soundscape node children
            rootNode.AppendChild(node);
        }

        public string[] GetFields()
        {
            return new string[] { Name, Description, Filepath };
        }

        public SoundFile(XmlNode node)
        {
            Parse(node);
        }
    }
}
