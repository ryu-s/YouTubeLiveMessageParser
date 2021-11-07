using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class MemberShip : IAction
    {
        public IReadOnlyList<IMessagePart> HeaderPrimaryTextItems { get; private set; }
        public IReadOnlyList<IMessagePart> HeaderSubTextItems { get; private set; }
        public IReadOnlyList<IMessagePart> MessageItems { get; private set; }
        public string Id { get; private set; }
        public string? AuthorName { get; private set; }
        public long TimestampUsec { get; private set; }
        public Thumbnail2 AuthorPhoto { get; private set; }
        public string AuthorExternalChannelId { get; private set; }
        public List<IAuthorBadge> AuthorBadges { get; private set; }
        public static SuperChat Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static MemberShip Parse(dynamic json)
        {
            var renderer = json.item.liveChatMembershipItemRenderer;

            List<IMessagePart> headerPrimaryTextItems;
            if (renderer.ContainsKey("headerPrimaryText"))
            {
                headerPrimaryTextItems = RunsToString(renderer.headerPrimaryText);
            }
            else
            {
                headerPrimaryTextItems = new List<IMessagePart>();
            }
            List<IMessagePart> headerSubTextItems;
            if (renderer.ContainsKey("headerSubtext"))
            {
                headerSubTextItems = RunsToString(renderer.headerSubtext);
            }
            else
            {
                headerSubTextItems = new List<IMessagePart>();
            }
            List<IMessagePart> messageItems;
            if (renderer.ContainsKey("message"))
            {
                messageItems = RunsToString(renderer.message);
            }
            else
            {
                messageItems = new List<IMessagePart>();
            }

            var timestampUsec = long.Parse((string)renderer.timestampUsec);
            var id = (string)renderer.id;

            string? authorName;
            if (renderer.ContainsKey("authorName"))
            {
                authorName = SimpleTextToString(renderer.authorName);
            }
            else
            {
                authorName = null;
            }

            var authorPhoto = Thumbnail2.Parse(renderer.authorPhoto.thumbnails[0]);

            var authorBadges = new List<IAuthorBadge>();
            if (renderer.ContainsKey("authorBadges"))
            {
                foreach (var badge in renderer.authorBadges)
                {
                    var c = AuthorBadgeFactory.Parse(badge);
                    authorBadges.Add(c);
                }
            }
            var authorExternalChannelId = (string)renderer.authorExternalChannelId;

            return new MemberShip
            {
                AuthorBadges = authorBadges,
                AuthorExternalChannelId = authorExternalChannelId,
                AuthorName = authorName,
                AuthorPhoto = authorPhoto,
                Id = id,
                MessageItems = messageItems,
                TimestampUsec = timestampUsec,
                HeaderPrimaryTextItems = headerPrimaryTextItems,
                HeaderSubTextItems = headerSubTextItems,
            };
        }
        private static string SimpleTextToString(dynamic obj)
        {
            return (string)obj.simpleText;
        }
        private static List<IMessagePart> RunsToString(dynamic obj)
        {
            var messageItems = new List<IMessagePart>();
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

                }
            }
            return messageItems;
        }
    }
}
