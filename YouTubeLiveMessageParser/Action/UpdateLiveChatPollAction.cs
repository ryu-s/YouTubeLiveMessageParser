namespace ryu_s.YouTubeLive.Message.Action
{
    public class UpdateLiveChatPollAction : IAction
    {
        internal static UpdateLiveChatPollAction Parse(dynamic addChatItemAction)
        {
            return new UpdateLiveChatPollAction();
        }
    }
}
