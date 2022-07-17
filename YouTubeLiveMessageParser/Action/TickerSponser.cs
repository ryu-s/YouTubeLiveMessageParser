using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class TickerSponser : IAction
    {
        public string Id { get; }
        public long TimestampUsec { get; }
        public string AuthorName { get; }
        public List<IMessagePart> HeaderPrimaryText { get; }
        public Thumbnail2 AuthorPhoto { get; }
        public string AuthorExternalChannelId { get; }
        public IReadOnlyList<IMessagePart> MessageItems { get; }
        public List<IAuthorBadge> AuthorBadges { get; }
        private TickerSponser(string id, long timestampUsec, string authorName, List<IMessagePart> headerPrimaryText,
            Thumbnail2 authorPhoto, string channelId, List<IMessagePart> messageItems, List<IAuthorBadge> authorBadges)
        {
            Id = id;
            TimestampUsec = timestampUsec;
            AuthorName = authorName;
            HeaderPrimaryText = headerPrimaryText;
            AuthorPhoto = authorPhoto;
            AuthorExternalChannelId = channelId;
            MessageItems = messageItems;
            AuthorBadges = authorBadges;
        }
        public static TickerSponser Parse(dynamic json)
        {
            var renderer = json.item.liveChatTickerSponsorItemRenderer;

            var membershipItemRenderer = renderer.showItemEndpoint.showLiveChatItemEndpoint.renderer.liveChatMembershipItemRenderer;
            var id = (string)membershipItemRenderer.id;
            var timestampUsec = (long)membershipItemRenderer.timestampUsec;
            var authorName = ActionTools.SimpleTextToString(membershipItemRenderer.authorName);
            var headerPrimaryText = ActionTools.RunsToString(membershipItemRenderer.headerPrimaryText);
            var authorPhoto = Thumbnail2.Parse(membershipItemRenderer.authorPhoto.thumbnails[0]);
            var channelId = (string)membershipItemRenderer.authorExternalChannelId;
            var message = ActionTools.RunsToString(membershipItemRenderer.message);
            var authorBadges = GetAuthorBadges(membershipItemRenderer);
            return new TickerSponser(id, timestampUsec, authorName, headerPrimaryText, authorPhoto, channelId, message, authorBadges);
        }
        private static List<IAuthorBadge> GetAuthorBadges(dynamic renderer)
        {
            var authorBadges = new List<IAuthorBadge>();
            if (renderer.ContainsKey("authorBadges"))
            {
                foreach (var badge in renderer.authorBadges)
                {
                    var c = AuthorBadgeFactory.Parse(badge);
                    authorBadges.Add(c);
                }
            }
            return authorBadges;
        }
    }
}
