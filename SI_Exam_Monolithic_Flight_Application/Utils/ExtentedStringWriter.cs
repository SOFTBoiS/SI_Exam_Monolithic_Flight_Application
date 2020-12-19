using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Utils
{
    // Thanks to https://blog.jsinh.in/use-utf-8-encoding-for-stringwriter-in-c/
    public sealed class ExtentedStringWriter : StringWriter
    {
        private readonly Encoding stringWriterEncoding;
        public ExtentedStringWriter(StringBuilder builder, Encoding desiredEncoding)
            : base(builder)
        {
            this.stringWriterEncoding = desiredEncoding;
        }

        public ExtentedStringWriter()
        {
        }

        public override Encoding Encoding
        {
            get
            {
                return this.stringWriterEncoding;
            }
        }
    }

}
