using System;

namespace ryu_s.YouTubeLive.Message
{
    public class GetLiveChatParseException : ParseException
    {
        public GetLiveChatParseException(Exception ex, string raw)
            : base(ex, raw)
        {
        }
    }
}
