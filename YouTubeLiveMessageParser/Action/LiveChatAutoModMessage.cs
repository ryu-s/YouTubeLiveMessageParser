using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class LiveChatAutoModMessage : IAction
    {
        public string Id { get; }
        public string TimestampUsec { get; }
        public List<IMessagePart> AutoModeratedItemMessage { get; }
        public string AutoModeratedItemAuthorName { get; }
        private LiveChatAutoModMessage(string id, string timestampUsec, List<IMessagePart> autoModeratedItemMessage, string autoModeratedItemAuthorName)
        {
            Id = id;
            TimestampUsec = timestampUsec;
            AutoModeratedItemMessage = autoModeratedItemMessage;
            AutoModeratedItemAuthorName = autoModeratedItemAuthorName;

        }
        internal static LiveChatAutoModMessage Parse(dynamic addChatItemAction)
        {
            var renderer = addChatItemAction.item.liveChatAutoModMessageRenderer;
            var id = (string)renderer.id;
            var timestampUsec = (string)renderer.timestampUsec;
            var autoModeratedItemMessage = ActionTools.RunsToString(renderer.autoModeratedItem.liveChatTextMessageRenderer.message);
            var autoModeratedItemAuthorName = ActionTools.SimpleTextToString(renderer.autoModeratedItem.liveChatTextMessageRenderer.authorName);
            //var subtext = Tools.RunsToString(renderer.subtext);
            //return new LiveChatAutoModMessage(id, timestampUsec, text, subtext);
            return new LiveChatAutoModMessage(id,timestampUsec, autoModeratedItemMessage,autoModeratedItemAuthorName);
        }

    }
}
