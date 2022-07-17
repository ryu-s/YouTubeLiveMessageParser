using Newtonsoft.Json;
using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    static class ActionTools
    {
        public static string SimpleTextToString(dynamic obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return (string)obj.simpleText;
        }
        public static List<IMessagePart> RunsToString(dynamic obj)
        {
            var messageItems = new List<IMessagePart>();
            if (obj == null)
            {
                return messageItems;
            }
            foreach (var item in obj.runs)
            {
                if (item.ContainsKey("text"))
                {
                    var text = new TextPart((string)item.text);
                    messageItems.Add(text);
                }
                else if (item.ContainsKey("emoji") && item.emoji.ContainsKey("isCustomEmoji"))
                {
                    var id = (string)item.emoji.emojiId;
                    var thumbnail = item.emoji.image.thumbnails[0];
                    var url = (string)thumbnail.url;
                    var width = (int)thumbnail.width;
                    var height = (int)thumbnail.height;
                    var tooltip = (string)item.emoji.image.accessibility.accessibilityData.label;
                    var emoji = new CustomEmojiPart(url, width, height, tooltip);
                    messageItems.Add(emoji);
                }
                else if (item.ContainsKey("emoji"))
                {
                    var id = (string)item.emoji.emojiId;
                    var url = (string)item.emoji.image.thumbnails[0].url;
                    var emoji = new EmojiPart(id, url);
                    messageItems.Add(emoji);
                }
                else
                {
                    throw new ParseException(item.ToString(Formatting.None));
                }
            }
            return messageItems;
        }
    }
}
