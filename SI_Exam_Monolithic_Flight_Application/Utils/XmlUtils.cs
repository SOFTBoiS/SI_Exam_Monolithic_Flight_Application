using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Utils
{
    public class XmlUtils<T>
    {
        public static string SerializeToString(T value)
        {
            using (var writer = new ExtentedStringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(value.GetType(), "");
                XmlSerializerNamespaces emptyNS = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                serializer.Serialize(writer, value, emptyNS);
                var res = writer.ToString();
                return res;
            }
        }

        public static T DeserializeToType(string xml)
        {

            XmlSerializer serializer =
                new XmlSerializer(typeof(T));
            T result;
            using (TextReader reader = new StringReader(xml))
            {
                result = (T) serializer.Deserialize(reader);
            }

            return result;

        }
    }
}
