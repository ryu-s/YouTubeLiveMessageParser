namespace ryu_s.YouTubeLive.Message.Action
{
    class TickerSponser : IAction
    {
        public static TickerSponser Parse(dynamic json)
        {
            var id = (string)json.item.liveChatTickerSponsorItemRenderer.id;
            return new TickerSponser();
        }
    }
}
