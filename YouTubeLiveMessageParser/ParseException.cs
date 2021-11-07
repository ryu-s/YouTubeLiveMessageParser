using System;
using System.Collections.Generic;
using System.Text;

namespace ryu_s.YouTubeLive.Message
{
    public class ParseException : Exception
    {
        public ParseException(string raw)
        {
            Raw = raw;
        }
        public ParseException(Exception ex, string raw)
            : base("", ex)
        {
            Raw = raw;
        }
        public string Raw { get; }
    }
}
