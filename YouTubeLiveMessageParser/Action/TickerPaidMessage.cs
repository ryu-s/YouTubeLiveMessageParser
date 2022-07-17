namespace ryu_s.YouTubeLive.Message.Action
{
    public class TickerPaidMessage : IAction
    {
        public static TickerPaidMessage Parse(dynamic json)
        {
            var id = (string)json.item.liveChatTickerPaidMessageItemRenderer.id;
            return new TickerPaidMessage();
        }
    }
}
