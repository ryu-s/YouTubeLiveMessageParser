namespace ryu_s.YouTubeLive.Message.Action
{
    public class PaidSticker : IAction
    {
        public static PaidSticker Parse(dynamic json)
        {
            return new PaidSticker();
        }
    }
}
