namespace ryu_s.YouTubeLive.Message.Action
{
    public class DeletedByAuthor : IAction
    {
        public string DeletedStateMessage { get; }
        public DeletedByAuthor(string deletedStateMessage)
        {
            DeletedStateMessage = deletedStateMessage;
        }
        public static DeletedByAuthor Parse(dynamic json)
        {
            var message = (string)json.deletedStateMessage.runs[0].text;
            return new DeletedByAuthor(message);
        }
    }
}
