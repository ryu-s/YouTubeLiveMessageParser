namespace ryu_s.YouTubeLive.Message.Continuation
{
    public class UnknownContinuationData : IContinuation
    {
        public UnknownContinuationData(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; }
    }
}
