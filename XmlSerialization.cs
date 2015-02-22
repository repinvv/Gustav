namespace Gustav
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    class XmlSerialization
    {
        public string Serialize<T>(T source) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = XmlWriter.Create(memoryStream,
                                                        new XmlWriterSettings
                                                        {
                                                            OmitXmlDeclaration = true,
                                                            Encoding = Encoding.Unicode
                                                        }))
                {
                    new XmlSerializer(typeof(T))
                        .Serialize(
                            streamWriter,
                            source,
                            new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                }

                var xml = Encoding.Unicode.GetString(memoryStream.ToArray());

                // XmlWriter adds character encoding code in the beginning of the memorystream 
                // that may cause problems when we will try to save result string in the database 
                // so we are deleting this code
                return xml[0] != '\xfeff' ? xml : xml.Substring(1);
            }
        }

        public T Deserialize<T>(string serialized) where T : class
        {
            using (var stringReader = new StringReader(serialized))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}
