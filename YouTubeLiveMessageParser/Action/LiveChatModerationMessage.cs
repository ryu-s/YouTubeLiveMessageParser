namespace ryu_s.YouTubeLive.Message.Action
{
    public class LiveChatModerationMessage : IAction
    {
        internal static LiveChatModerationMessage Parse(dynamic addChatItemAction)
        {
            return new LiveChatModerationMessage();
        }
    }
}
