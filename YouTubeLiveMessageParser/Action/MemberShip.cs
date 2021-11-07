namespace ryu_s.YouTubeLive.Message.Action
{
    public class MemberShip : IAction
    {
        public static MemberShip Parse(dynamic json)
        {
            return new MemberShip();
        }
    }
}
