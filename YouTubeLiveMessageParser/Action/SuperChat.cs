using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class SuperChat : IAction
    {
        public IReadOnlyList<IMessagePart> MessageItems { get; private set; }
        public string Id { get; private set; }
        public string? AuthorName { get; private set; }
        public long TimestampUsec { get; private set; }
        public Thumbnail2 AuthorPhoto { get; private set; }
        public string AuthorExternalChannelId { get; private set; }
        public List<IAuthorBadge> AuthorBadges { get; private set; }
        public string PurchaseAmount { get; private set; }

        public static SuperChat Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static SuperChat Parse(dynamic json)
        {
            var renderer = json.item.liveChatPaidMessageRenderer;

            var purchaseAmount = (string)renderer.purchaseAmountText.simpleText;
            List<IMessagePart> messageItems;
            if (renderer.ContainsKey("message"))
            {
                messageItems = ActionTools.RunsToString(renderer.message);
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

            //return new TextMessage(messageItems, id, authorName, timestampUsec, authorPhoto, authorExternalChannelId, authorBadges);
            return new SuperChat
            {
                AuthorBadges = authorBadges,
                AuthorExternalChannelId = authorExternalChannelId,
                AuthorName = authorName,
                AuthorPhoto = authorPhoto,
                Id = id,
                MessageItems = messageItems,
                PurchaseAmount = purchaseAmount,
                TimestampUsec = timestampUsec,
            };
        }
    }
}
