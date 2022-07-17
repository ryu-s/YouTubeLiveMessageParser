using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ryu_s.YouTubeLive.Message;
using System.Linq;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class LiveChatSponsorshipsGiftRedemptionAnnouncementMessage : IAction
    {
        public IReadOnlyList<IMessagePart> MessageItems { get; }
        public string Id { get; }
        public string? AuthorName { get; }
        public long TimestampUsec { get; }
        public Thumbnail2 AuthorPhoto { get; }
        public string AuthorExternalChannelId { get; }
        public List<IAuthorBadge> AuthorBadges { get; }
        public LiveChatSponsorshipsGiftRedemptionAnnouncementMessage(List<IMessagePart> messageItems, string id, string? authorName, long timestampUsec, Thumbnail2 authorPhoto, string channelId, List<IAuthorBadge> authorBadges)
        {
            MessageItems = messageItems;
            Id = id;
            AuthorName = authorName;
            TimestampUsec = timestampUsec;
            AuthorPhoto = authorPhoto;
            AuthorExternalChannelId = channelId;
            AuthorBadges = authorBadges;
        }
        public static LiveChatSponsorshipsGiftRedemptionAnnouncementMessage Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        internal static LiveChatSponsorshipsGiftRedemptionAnnouncementMessage Parse(dynamic json)
        {
            var renderer = json.item.liveChatSponsorshipsGiftRedemptionAnnouncementRenderer;
            var messageItems = ActionTools.RunsToString(renderer.message);

            var timestampUsec = long.Parse((string)renderer.timestampUsec);
            var id = (string)renderer.id;

            string? authorName;
            if (renderer.ContainsKey("authorName"))
            {
                authorName = ActionTools.SimpleTextToString(renderer.authorName);
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

            return new LiveChatSponsorshipsGiftRedemptionAnnouncementMessage(messageItems, id, authorName, timestampUsec, authorPhoto, authorExternalChannelId, authorBadges);
        }
    }
}
