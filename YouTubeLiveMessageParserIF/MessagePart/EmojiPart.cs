namespace ryu_s.YouTubeLive.Message
{
    public class EmojiPart : IMessagePart
    {
        public string EmojiId { get; }
        public string Url { get; }
        public EmojiPart(string emojiId, string url)
        {
            EmojiId = emojiId;
            Url = url;
        }
    }
}
