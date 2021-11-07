namespace ryu_s.YouTubeLive.Message
{
    public interface ITextPart : IMessagePart
    {
        string Raw { get; }
    }
}
