namespace ryu_s.YouTubeLive.Message
{
    public class TextPart : ITextPart
    {
        public TextPart(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; }
        public override bool Equals(object? obj)
        {
            if (!(obj is TextPart b))
            {
                return false;
            }
            return Raw == b.Raw;
        }
        public override int GetHashCode()
        {
            return Raw.GetHashCode();
        }
    }
}
