namespace ryu_s.YouTubeLive.Message
{
    public class CustomEmojiPart : IMessagePart
    {
        public CustomEmojiPart(string url, int width, int height, string tooltip)
        {
            Url = url;
            Width = width;
            Height = height;
            Tooltip = tooltip;
        }

        public string Url { get; }
        public int Width { get; }
        public int Height { get; }
        public string Tooltip { get; }
    }
}
