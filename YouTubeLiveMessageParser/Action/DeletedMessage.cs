namespace ryu_s.YouTubeLive.Message.Action
{
    public class DeletedMessage : IAction
    {
        public static DeletedMessage Parse(dynamic json)
        {
            return new DeletedMessage();
        }
    }
}
