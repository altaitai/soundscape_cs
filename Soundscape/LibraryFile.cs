using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Soundscape
{
    class LibraryFile
    {
        public List<SoundFile> Sounds { get; set; }

        public string Name { get; set; }

        public string Filepath { get; set; }

        public void SetDefaults()
        {
            Sounds = new List<SoundFile>();
        }

        public void Open(string filepath)
        {
            if (!File.Exists(filepath)) 
            {
                throw new Exception($"File {filepath} not found");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            Parse(doc);
        }


        public void Parse(XmlDocument contents)
        {
            XmlNode rootNode = contents.SelectSingleNode($"/{Constants.RootNodeName}");
            Name = Utilities.GetAttributeValue(rootNode, "name");

            foreach (XmlNode node in contents.SelectNodes($"/{Constants.RootNodeName}/{Constants.SoundNodeName}"))
            {
                try
                {
                    Sounds.Add(new SoundFile(node));
                }
                catch (Exception) { }
            }
        }

        public void Save(string filepath)
        {
            // create root node
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateNode(XmlNodeType.Element, Constants.RootNodeName, string.Empty);
            doc.AppendChild(rootNode);

            // create attributes
            Utilities.CreateAttribute(doc, rootNode, "version", Constants.Version);
            Utilities.CreateAttribute(doc, rootNode, "name", Name);

            // create sound nodes
            foreach (SoundFile sound in Sounds)
            {
                sound.Save(doc, rootNode);
            }

            // save to file
            doc.Save(filepath);
            Filepath = filepath;
        }

        public LibraryFile()
        {
            SetDefaults();
        }

        public LibraryFile(string filepath)
        {
            SetDefaults();
            Open(filepath);
        }
    }
}
