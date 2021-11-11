namespace ryu_s.YouTubeLive.Message.Action
{
    class DeletedMessage : IAction
    {
        public static DeletedMessage Parse(dynamic json)
        {
            return new DeletedMessage();
        }
    }
}
