using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class SponsorshipsGiftPurchaseAnnouncement : IAction
    {
        public string Id { get; private set; }
        public long TimestampUsec { get; private set; }
        public string ChannelId { get; private set; }
        public string AuthorName { get; private set; }
        public List<IMessagePart> HeaderPrimaryText { get; private set; }
        public Thumbnail2 AuthorPhoto { get; private set; }
        public List<IAuthorBadge> AuthorBadges { get; private set; }
        public Thumbnail1 Image { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private SponsorshipsGiftPurchaseAnnouncement() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static SponsorshipsGiftPurchaseAnnouncement Parse(dynamic json)
        {
            var renderer = json.item.liveChatSponsorshipsGiftPurchaseAnnouncementRenderer;
            var id = (string)renderer.id;
            var timestampUsec = long.Parse((string)renderer.timestampUsec);
            var channelId = (string)renderer.authorExternalChannelId;

            var header = renderer.header.liveChatSponsorshipsHeaderRenderer;

            var authorName = ActionTools.SimpleTextToString(header.authorName);
            var headerPrimaryText = ActionTools.RunsToString(header.primaryText);
            var authorPhoto = Thumbnail2.Parse(header.authorPhoto.thumbnails[0]);
            var authorBadges = ActionTools.GetAuthorBadges(header);
            var image = Thumbnail1.Parse(header.image.thumbnails[0]);
            return new SponsorshipsGiftPurchaseAnnouncement
            {
                AuthorBadges = authorBadges,
                AuthorName = authorName,
                AuthorPhoto = authorPhoto,
                ChannelId = channelId,
                HeaderPrimaryText = headerPrimaryText,
                Id = id,
                TimestampUsec = timestampUsec,
                Image = image,
            };
        }
    }
}
