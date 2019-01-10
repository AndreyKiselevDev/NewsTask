using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NewsTask.Service.Extensions
{
    public static class XmlParser
    {
        public static T Deserialize<T>(string value) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(value))
            {
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
