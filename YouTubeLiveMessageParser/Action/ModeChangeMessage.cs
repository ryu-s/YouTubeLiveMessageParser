using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class ModeChangeMessage : IAction
    {
        public string Id { get; }
        public string TimestampUsec { get; }
        public List<IMessagePart> Text { get; }
        public List<IMessagePart> Subtext { get; }
        private ModeChangeMessage(string id, string timestampUsec, List<IMessagePart> text, List<IMessagePart> subtext)
        {
            Id = id;
            TimestampUsec = timestampUsec;
            Text = text;
            Subtext = subtext;
        }
        internal static ModeChangeMessage Parse(dynamic addChatItemAction)
        {
            var renderer = addChatItemAction.item.liveChatModeChangeMessageRenderer;
            var id = (string)renderer.id;
            var timestampUsec = (string)renderer.timestampUsec;
            var text = ActionTools.RunsToString(renderer.text);
            var subtext = ActionTools.RunsToString(renderer.subtext);
            return new ModeChangeMessage(id, timestampUsec, text, subtext);
        }
    }
}
