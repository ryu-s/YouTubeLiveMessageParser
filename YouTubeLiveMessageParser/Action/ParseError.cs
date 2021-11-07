namespace ryu_s.YouTubeLive.Message.Action
{
    public class ParseError : IAction
    {
        public ParseError(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; }
    }
}
