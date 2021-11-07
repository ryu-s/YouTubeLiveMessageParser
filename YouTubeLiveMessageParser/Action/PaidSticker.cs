namespace ryu_s.YouTubeLive.Message.Action
{
    class PaidSticker : IAction
    {
        public static PaidSticker Parse(dynamic json)
        {
            return new PaidSticker();
        }
    }
}
