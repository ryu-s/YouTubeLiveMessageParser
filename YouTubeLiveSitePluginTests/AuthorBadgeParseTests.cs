
using NUnit.Framework;
using ryu_s.YouTubeLive.Message.Action;
using System.Collections.Generic;

namespace YouTubeLiveSitePluginTests
{
    public class SiteMessageParseTests
    {
        [Test]
        public void Test()
        {
            var s = @"{""item"":{""liveChatTextMessageRenderer"":{""message"":{""runs"":[{""text"":""abc""}]},""authorName"":{""simpleText"":""name""},""authorPhoto"":{""thumbnails"":[{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s32-c-k-c0x00ffffff-no-rj"",""width"":32,""height"":32},{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s64-c-k-c0x00ffffff-no-rj"",""width"":64,""height"":64}]},""id"":""id"",""timestampUsec"":""1631202054190683"",""authorBadges"":[{""liveChatAuthorBadgeRenderer"":{""icon"":{""iconType"":""MODERATOR""},""tooltip"":""モデレーター""}},{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}],""authorExternalChannelId"":""UC6vWy2N0Ochgx7uIeOPg9nQ""}},""clientId"":""CJSj3v2Y8vICFZYgYAodcEcE0g3""}";
            var text = TextMessage.Parse(s);
            Assert.AreEqual(new List<IAuthorBadge>
            {
                new AuthorBadgeIcon("MODERATOR","モデレーター"),
                new AuthorBadgeCustomThumb(new List<Thumbnail1>{
                    new Thumbnail1("https://example.com/1"),
                    new Thumbnail1("https://example.com/2")
                },"メンバー（6 か月）"),
            }, text.AuthorBadges);
        }
    }
    public class AuthorBadgeParseTests
    {
        [Test]
        public void CustomBadgeTest()
        {
            var s = @"{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}";
            var badge = AuthorBadgeFactory.Parse(s);
            if (badge is not AuthorBadgeCustomThumb custom)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual(new List<Thumbnail1> { new Thumbnail1("https://example.com/1"), new Thumbnail1("https://example.com/2") }, custom.Thumbnails);
            Assert.AreEqual("メンバー（6 か月）", custom.Tooltip);
        }
        [Test]
        public void IconTest()
        {
            var s = @"{""liveChatAuthorBadgeRenderer"":{""icon"":{""iconType"":""VERIFIED""},""tooltip"":""確認済み""}}";
            var badge = AuthorBadgeFactory.Parse(s);
            if (badge is not AuthorBadgeIcon icon)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("VERIFIED", icon.IconType);
            Assert.AreEqual("確認済み", icon.Tooltip);
        }
    }
}