namespace ryu_s.YouTubeLive.Message.Action
{
    class Placeholder : IAction
    {
        public static Placeholder Parse(dynamic json)
        {
            var id = (string)json.item.liveChatPlaceholderItemRenderer.id;
            return new Placeholder();
        }
    }
}
