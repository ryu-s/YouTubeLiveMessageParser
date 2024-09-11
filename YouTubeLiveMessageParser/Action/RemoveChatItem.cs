namespace ryu_s.YouTubeLive.Message.Action
{
    public class RemoveChatItem : IAction
    {
        public string TargetItemId { get; }
        private RemoveChatItem(string targetItemId)
        {
            TargetItemId = targetItemId;
        }
        internal static RemoveChatItem Parse(dynamic removeChatItemAction)
        {
            var id = (string)removeChatItemAction.targetItemId;
            return new RemoveChatItem(id);
        }
    }
}
