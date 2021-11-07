namespace ryu_s.YouTubeLive.Message.Continuation
{
    public class ReloadContinuationData : IContinuation
    {
        public ReloadContinuationData(string continaution)
        {
            Continaution = continaution;
        }

        public string Continaution { get; }
    }
}
