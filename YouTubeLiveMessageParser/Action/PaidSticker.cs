using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    public class PaidSticker : IAction
    {
        public string Id { get; private set; }
        public long TimestampUsec { get; private set; }
        public string PurchaseAmount { get; private set; }
        public string StickerThumbnailUrl { get; private set; }
        public int StickerThumbnailWidth { get; private set; }
        public int StickerThumbnailHeight { get; private set; }
        public string StickerTooltip { get; private set; }
        public string AuthorName { get; private set; }
        public Thumbnail2 AuthorPhoto { get; private set; }
        public string ChannelId { get; private set; }
        public List<IAuthorBadge> AuthorBadges { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private PaidSticker() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static PaidSticker Parse(dynamic json)
        {
            var renderer = json.item.liveChatPaidStickerRenderer;

            var id = (string)renderer.id;
            var timestampUsec = long.Parse((string)renderer.timestampUsec);
            var purchaseAmount = (string)renderer.purchaseAmountText.simpleText;
            var preStickerThumbnailUrl = (string)renderer.sticker.thumbnails[0].url;
            if (preStickerThumbnailUrl.StartsWith("//"))
            {
                preStickerThumbnailUrl = "https:" + preStickerThumbnailUrl;
            }
            var stickerThumbnailUrl = preStickerThumbnailUrl;
            var stickerThumbnailWidth = (int)renderer.sticker.thumbnails[0].width;
            var stickerThumbnailHeight = (int)renderer.sticker.thumbnails[0].height;
            var stickerTooltip = (string)renderer.sticker.accessibility.accessibilityData.label;
            var authorName = ActionTools.SimpleTextToString(renderer.authorName);
            var authorPhoto = Thumbnail2.Parse(renderer.authorPhoto.thumbnails[0]);
            var channelId = (string)renderer.authorExternalChannelId;
            var authorBadges = ActionTools.GetAuthorBadges(renderer);
            return new PaidSticker
            {
                Id = id,
                PurchaseAmount = purchaseAmount,
                TimestampUsec = timestampUsec,
                StickerThumbnailHeight = stickerThumbnailHeight,
                StickerThumbnailUrl = stickerThumbnailUrl,
                StickerThumbnailWidth = stickerThumbnailWidth,
                StickerTooltip = stickerTooltip,
                AuthorName = authorName,
                AuthorPhoto = authorPhoto,
                ChannelId = channelId,
                AuthorBadges = authorBadges,
            };
        }
    }
}
