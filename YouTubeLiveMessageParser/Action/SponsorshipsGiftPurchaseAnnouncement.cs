using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class SponsorshipsGiftPurchaseAnnouncement : IAction
    {
        public string Id { get; }
        public string AuthorName { get; }
        public List<IMessagePart> HeaderPrimaryText { get; }
        public Thumbnail2 AuthorPhoto { get; }
        public string AuthorExternalChannelId { get; }
        public List<IAuthorBadge> AuthorBadges { get; }
        private SponsorshipsGiftPurchaseAnnouncement(string id, string authorName, List<IMessagePart> headerPrimaryText,
            Thumbnail2 authorPhoto, string channelId, List<IAuthorBadge> authorBadges)
        {
            Id = id;
            AuthorName = authorName;
            HeaderPrimaryText = headerPrimaryText;
            AuthorPhoto = authorPhoto;
            AuthorExternalChannelId = channelId;
            AuthorBadges = authorBadges;
        }
        public static SponsorshipsGiftPurchaseAnnouncement Parse(dynamic json)
        {
            var renderer = json.item.liveChatTickerSponsorItemRenderer;

            var membershipItemRenderer = renderer.showItemEndpoint.showLiveChatItemEndpoint.renderer.liveChatSponsorshipsGiftPurchaseAnnouncementRenderer;
            var id = (string)renderer.id;
            var authorName = ActionTools.SimpleTextToString(membershipItemRenderer.header.liveChatSponsorshipsHeaderRenderer.authorName);
            var headerPrimaryText = ActionTools.RunsToString(membershipItemRenderer.header.liveChatSponsorshipsHeaderRenderer.primaryText);
            var authorPhoto = Thumbnail2.Parse(membershipItemRenderer.header.liveChatSponsorshipsHeaderRenderer.authorPhoto.thumbnails[0]);
            var channelId = (string)membershipItemRenderer.authorExternalChannelId;
            var authorBadges = GetAuthorBadges(membershipItemRenderer.header.liveChatSponsorshipsHeaderRenderer);
            return new SponsorshipsGiftPurchaseAnnouncement(id, authorName, headerPrimaryText, authorPhoto, channelId, authorBadges);
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
