using System;
using System.IO;
using System.Xml.Linq;
using RSSFeeder.Model.Config;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using System.Xml;
using System.Text;

namespace RSSFeeder.Utils
{
    public class OptionIO
    {
        /// <summary>
        /// Default options
        /// </summary>
        private Option defaultOption;

        public OptionIO()
        {
            defaultOption = new Option
            {
                NewsFeedOptions = new List<NewsFeedOption>
                {
                    new NewsFeedOption{
                        MaxArticles = 10, Title = "Habr",
                        UpdateFrequency =10, NumberOfRSSFeed=0,
                        RSSUrl = @"https://habr.com/rss/interesting/",
                        IsFormattedTags = false
                    },
                }
            };
        }

        /// <summary>
        /// Sets new settings
        /// </summary>
        /// <param name="newOption">new option</param>
        public void UpdateOption(Option newOption)
        {
            if (File.Exists("options.xml"))
                File.Delete("options.xml");    
            
            XDocument xdoc = new XDocument(CreateXElement(newOption));

            xdoc.Save("options.xml");
        }

        /// <summary>
        /// Loads settings from the file if there is a configuration file, otherwise it creates a default custom setting
        /// </summary>
        /// <returns></returns>
        public Option GetOption()
        {
            Option option;
            if (!File.Exists("option.xsd"))
                CreateXSD();

            if (!File.Exists("options.xml"))
            {
                XDocument xdoc = new XDocument(CreateXElement(defaultOption));
                xdoc.Save("options.xml");
                option = defaultOption;
            }
            else
            {
                option = LoadOptions();
            }

            return option;
        }

        /// <summary>
        /// Loads settings from a file
        /// </summary>
        /// <returns></returns>
        private Option LoadOptions()
        {
            var newOption = new Option()
            {
                NewsFeedOptions = new List<NewsFeedOption> { NewsFeedOption.DefaultOption() }
            };
            XDocument xdoc = XDocument.Load("options.xml");
            
            if (Validate(xdoc))
            {
                var newsFeedOptions = from xe in xdoc.Element("option").
                                  Elements("newsFeedOptions").Elements("newsFeedOption")
                                      select new NewsFeedOption
                                      {
                                          MaxArticles = int.Parse(xe.Element("maxArticles").Value),
                                          NumberOfRSSFeed = int.Parse(xe.Element("numberOfRSSFeed").Value),
                                          Title = xe.Element("title").Value,
                                          UpdateFrequency = int.Parse(xe.Element("updateFrequency").Value),
                                          RSSUrl = xe.Element("rSSUrl").Value,
                                          IsFormattedTags = bool.Parse(xe.Element("isFormattedTags").Value)
                                      };
                 newOption = new Option() { NewsFeedOptions = newsFeedOptions.ToList() };
            }
            else
            {
                var newsFeedOptions = new Option()
                {
                    NewsFeedOptions = new List<NewsFeedOption>
                    {
                        NewsFeedOption.DefaultOption()
                    }
                };
            }
            
            return newOption;
        }

        /// <summary>
        /// Validation based on xcd template
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        private bool Validate(XDocument xdoc)
        {
            bool isValidly = true;
            using (StreamReader stream = new StreamReader("option.xsd"))
            {
                var s = stream.ReadToEnd();
                var xmlSchemaSet = new XmlSchemaSet();
                xmlSchemaSet.Add("", XmlReader.Create(
                    new StringReader(s)
                    ));
                xdoc.Validate(xmlSchemaSet, (o, e) => 
                {
                    isValidly = false;
                });
            }
            
            return isValidly;
        }

        /// <summary>
        /// Creates an internal frame of the xml configuration file
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private XElement CreateXElement(Option option)
        {
            XElement xElement = new XElement("option");
            XElement newsFeedOptions = new XElement("newsFeedOptions");
            
            foreach(var newsFeedOption in option.NewsFeedOptions)
            {
                XElement XnewsFeedOption = new XElement("newsFeedOption",
                    new XElement("maxArticles", newsFeedOption.MaxArticles),
                    new XElement("numberOfRSSFeed", newsFeedOption.NumberOfRSSFeed),
                    new XElement("title", newsFeedOption.Title),
                    new XElement("updateFrequency", newsFeedOption.UpdateFrequency),
                    new XElement("rSSUrl", newsFeedOption.RSSUrl),
                    new XElement("isFormattedTags", newsFeedOption.IsFormattedTags));

                newsFeedOptions.Add(XnewsFeedOption);
            }
            xElement.Add(newsFeedOptions);
            return xElement;
        }

        /// <summary>
        /// Creates a xsd scheme for validation
        /// </summary>
        private void CreateXSD()
        {
            var schema = new XmlSchema();
            schema.AttributeFormDefault = XmlSchemaForm.Unqualified;
            schema.ElementFormDefault = XmlSchemaForm.Qualified;

            // <xs:element name="option"/>
            XmlSchemaElement elementOption = new XmlSchemaElement();
            schema.Items.Add(elementOption);
            elementOption.Name = "option";

            // <xs:complexType>
            var complexType = new XmlSchemaComplexType();
            elementOption.SchemaType = complexType;

            // <xs:sequence>
            var xmlSchemaSequence = new XmlSchemaSequence();
            complexType.Particle = xmlSchemaSequence;

            // <xs:element name="newsFeedOptions"/>
            var elementNewsFeedOptions = new XmlSchemaElement();
            xmlSchemaSequence.Items.Add(elementNewsFeedOptions);
            elementNewsFeedOptions.Name = "newsFeedOptions";

            // <xs:complexType>
            var complexType2 = new XmlSchemaComplexType();
            elementNewsFeedOptions.SchemaType = complexType2;

            // <xs:sequence>
            var xmlSchemaSequence2 = new XmlSchemaSequence();
            complexType2.Particle = xmlSchemaSequence2;

            // <xs:element name="newsFeedOption"/>
            var elementNewsFeedOption = new XmlSchemaElement();
            xmlSchemaSequence2.Items.Add(elementNewsFeedOption);
            elementNewsFeedOption.Name = "newsFeedOption";
            elementNewsFeedOption.MinOccurs = 0;
            elementNewsFeedOption.MaxOccursString = "unbounded";

            // <xs:complexType>
            var complexType3 = new XmlSchemaComplexType();
            elementNewsFeedOption.SchemaType = complexType3;

            // <xs:sequence>
            var xmlSchemaSequence3 = new XmlSchemaSequence();
            complexType3.Particle = xmlSchemaSequence3;

            // <xs:element name="maxArticles">
            var elementMaxArticles = new XmlSchemaElement();
            elementMaxArticles.Name = "maxArticles";
            elementMaxArticles.SchemaTypeName = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementMaxArticles);

            // <xs:element name="numberOfRSSFeed">
            var elementNumberOfRSSFeed = new XmlSchemaElement();
            elementNumberOfRSSFeed.Name = "numberOfRSSFeed";
            elementNumberOfRSSFeed.SchemaTypeName = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementNumberOfRSSFeed);

            // <xs:element name="title">
            var elementTitle = new XmlSchemaElement();
            elementTitle.Name = "title";
            elementTitle.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementTitle);

            // <xs:element name="updateFrequency">
            var elementUpdateFrequency = new XmlSchemaElement();
            elementUpdateFrequency.Name = "updateFrequency";
            elementUpdateFrequency.SchemaTypeName = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementUpdateFrequency);

            // <xs:element name="rSSUrl">
            var elementRSSUrl = new XmlSchemaElement();
            elementRSSUrl.Name = "rSSUrl";
            elementRSSUrl.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementRSSUrl);

            // <xs:element name="isFormattedTags">
            var elementIsFormattedTags = new XmlSchemaElement();
            elementIsFormattedTags.Name = "isFormattedTags";
            elementIsFormattedTags.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            xmlSchemaSequence3.Items.Add(elementIsFormattedTags);

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.ValidationEventHandler += new ValidationEventHandler(ValidationCallbackOne);
            schemaSet.Add(schema);
            schemaSet.Compile();

            XmlSchema compiledSchema = null;

            foreach (XmlSchema schema1 in schemaSet.Schemas())
            {
                compiledSchema = schema1;
            }

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

            compiledSchema.Write(Console.Out, nsmgr);

            FileStream file = new FileStream("option.xsd", FileMode.Create, FileAccess.ReadWrite);
            using(var xwriter = new XmlTextWriter(file, new UTF8Encoding()))
            {
                xwriter.Formatting = Formatting.Indented;
                compiledSchema.Write(xwriter);
            }
        }

        public static void ValidationCallbackOne(object sender, ValidationEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}