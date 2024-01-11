using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Runtime.InteropServices;


using Foundation;
using UIKit;
using ObjCRuntime;

namespace NavitasBeta.iOS
{
    class NSLogWriter :TextWriter
    {

        [DllImport(Constants.FoundationLibrary)]
        extern static void NSLog(IntPtr format, IntPtr s);

        static NSString format = new NSString("%@");

        StringBuilder sb;

        public NSLogWriter()
        {
            sb = new StringBuilder();
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }

        public override void Flush()
        {
            try
            {
                using (var ns = new NSString(sb.ToString()))
                    NSLog(format.Handle, ns.Handle);
                sb.Length = 0;
            }
            catch (Exception)
            {
            }
        }

        // minimum to override - see http://msdn.microsoft.com/en-us/library/system.io.textwriter.aspx
        public override void Write(char value)
        {
            try
            {
                sb.Append(value);
            }
            catch (Exception)
            {
            }
        }

        // optimization (to avoid concatening chars)
        public override void Write(string value)
        {
            try
            {
                sb.Append(value);
                if (value != null && value.Length >= CoreNewLine.Length && EndsWithNewLine(value))
                    Flush();
            }
            catch (Exception)
            {
            }
        }

        bool EndsWithNewLine(string value)
        {
            for (int i = 0, v = value.Length - CoreNewLine.Length; i < CoreNewLine.Length; ++i, ++v)
            {
                if (value[v] != CoreNewLine[i])
                    return false;
            }

            return true;
        }

        public override void WriteLine()
        {
            try
            {
                Flush();
            }
            catch (Exception)
            {
            }
        }
    }
}
