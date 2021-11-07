using ryu_s.YouTubeLive.Message.Action;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using ryu_s.YouTubeLive.Message;

namespace YouTubeLiveSitePluginTests
{
    public class ActionParseTests
    {
        [Test]
        public void ParseInvalidDataTest()
        {
            var s = "abc";
            var err = ActionFactory.Parse(s) as ParseError;
            if(err == null)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("abc", err.Raw);
        }
        [Test]
        public void ParseTextMessageTest()
        {
            var s = @"{""addChatItemAction"":{""item"":{""liveChatTextMessageRenderer"":{""message"":{""runs"":[{""text"":""abc""}]},""authorName"":{""simpleText"":""name""},""authorPhoto"":{""thumbnails"":[{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s32-c-k-c0x00ffffff-no-rj"",""width"":32,""height"":32},{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s64-c-k-c0x00ffffff-no-rj"",""width"":64,""height"":64}]},""id"":""id"",""timestampUsec"":""1631202054190683"",""authorBadges"":[{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""メンバー（6 か月）""}}],""authorExternalChannelId"":""UC6vWy2N0Ochgx7uIeOPg9nQ""}},""clientId"":""CJSj3v2Y8vICFZYgYAodcEcE0g3""}}";
            var a = ActionFactory.Parse(s);
            if (a is not TextMessage text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("name", text.AuthorName);
            Assert.AreEqual("id", text.Id);
            Assert.AreEqual(1631202054190683, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart> {
                new TextPart("abc")
            }, text.MessageItems);

        }
        [Test]
        public void ParseSuperchatTest()
        {
            var s = "{\"clickTrackingParams\":\"CAEQl98BIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"addChatItemAction\":{\"item\":{\"liveChatPaidMessageRenderer\":{\"id\":\"ChwKGkNLREw3djZoblBJQ0ZSZ1ByUVlkX25VRklR\",\"timestampUsec\":\"1628248435329905\",\"authorName\":{\"simpleText\":\"しらたき。\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLRhr_7inzkWxqDKyBereHQZugB8ecDCad=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLRhr_7inzkWxqDKyBereHQZugB8ecaW=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"purchaseAmountText\":{\"simpleText\":\"￥300\"},\"message\":{\"runs\":[{\"text\":\"abcdefg\"}]},\"headerBackgroundColor\":4278237396,\"headerTextColor\":4278190080,\"bodyBackgroundColor\":4278248959,\"bodyTextColor\":4278190080,\"authorExternalChannelId\":\"UCsduCAn_VyCdvlqA_D36j9A\",\"authorNameTextColor\":3003121664,\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CAIQ7rsEIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHRFVERkMk5taHVVRWxEUmxKblVISlJXTRTk0VUhNMFdIWnpiVGhJTUZwNFdFZHBRbmNTQzFkTFYwMWZTR3d3Vlc0MElBRW9CRElhQ2hoVlEzTmtkVU5CYmw5V2VXUlNkbXh4UVY5RU16WnFPVUUlM0Q=\"}},\"timestampColor\":2147483648,\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"コメントの操作\"}},\"trackingParams\":\"CAIQ7rsEIhMIg96XjaKc8gIVSUJYCh1RmgaF\"}}}}";
            var a = ActionFactory.Parse(s);
            if (a is not SuperChat text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UCsduCAn_VyCdvlqA_D36j9A", text.AuthorExternalChannelId);
            Assert.AreEqual("￥300", text.PurchaseAmount);
            Assert.AreEqual("しらたき。", text.AuthorName);
            Assert.AreEqual("ChwKGkNLREw3djZoblBJQ0ZSZ1ByUVlkX25VRklR", text.Id);
            Assert.AreEqual(1628248435329905, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart> {
                new TextPart("abcdefg")
            }, text.MessageItems);

        }
        [Test]
        public void ParseSuperchatNoMessageTest()
        {
            var s = "{\"clickTrackingParams\":\"CAEQl98BIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"addChatItemAction\":{\"item\":{\"liveChatPaidMessageRenderer\":{\"id\":\"ChwKGkNKZXZ2Y2VFaHZRQ0ZiNE1yUVlkOUpRQ1Vn\",\"timestampUsec\":\"1636280729048459\",\"authorName\":{\"simpleText\":\"斎藤ひろし\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTXI1_Vgn1T_fbtAYFKVG4yMMZ53GU7nleM8w=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTXI1_Vgn1T_fbtAYFKVG4yMMZ53GU7nleM8w=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"purchaseAmountText\":{\"simpleText\":\"NT$15.00\"},\"headerBackgroundColor\":4279592384,\"headerTextColor\":4294967295,\"bodyBackgroundColor\":4280191205,\"bodyTextColor\":4294967295,\"authorExternalChannelId\":\"UCg6fWFQ93ETVFUQ2PVXN6ag\",\"authorNameTextColor\":3019898879,\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CAIQ7rsEIhMI5rzw3YSG9AIVEFpYCh0xsQTM\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHBsZG5aalpVVm9kbEZEUm1JMFRYSlJXV1E1U2xGRFZXY2FLU29uQ2hoVlF6bHlkVlpaVUhZM2VVcHRWakJTYURCT1MwRXRUSGNTQzJ0Zk9HdzRNVkpPYTNkSklBSW9CRElhQ2hoVlEyYzJabGRHVVRrelJWUldSbFZSTWxCV1dFNDJZV2MlM0Q=\"}},\"timestampColor\":2164260863,\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"コメントの操作\"}},\"trackingParams\":\"CAIQ7rsEIhMI5rzw3YSG9AIVEFpYCh0xsQTM\"}}}}";
            var a = ActionFactory.Parse(s);
            if (a is not SuperChat text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UCg6fWFQ93ETVFUQ2PVXN6ag", text.AuthorExternalChannelId);
            Assert.AreEqual("NT$15.00", text.PurchaseAmount);
            Assert.AreEqual("斎藤ひろし", text.AuthorName);
            Assert.AreEqual("ChwKGkNKZXZ2Y2VFaHZRQ0ZiNE1yUVlkOUpRQ1Vn", text.Id);
            Assert.AreEqual(1636280729048459, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>(), text.MessageItems);
        }
    }
}