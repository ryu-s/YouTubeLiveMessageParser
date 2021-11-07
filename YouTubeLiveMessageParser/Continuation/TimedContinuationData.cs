namespace ryu_s.YouTubeLive.Message.Continuation
{
    public class TimedContinuationData : IContinuation
    {
        public TimedContinuationData(int timeoutMs, string continaution)
        {
            TimeoutMs = timeoutMs;
            Continaution = continaution;
        }

        public int TimeoutMs { get; }
        public string Continaution { get; }
    }
}
