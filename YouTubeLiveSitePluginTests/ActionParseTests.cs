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
            if (err == null)
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
        [Test]
        public void ParseMonthlySpecialMembershipTest()
        {
            var raw = "{\"clickTrackingParams\":\"CAEQl98BIhMIuKKZ4by48wIV5YBjBh1cJAiN\",\"addChatItemAction\":{\"item\":{\"liveChatMembershipItemRenderer\":{\"id\":\"Ci8KLUNKUEI0Sktvb19NQ0ZUcnU0d2NkNmpzRUtBLUxveU1lc0lELTMyNjcyMzEzNw%3D%3D\",\"timestampUsec\":\"1633615685384473\",\"authorExternalChannelId\":\"UC-qpLhjn7jMv3ZtP9u_Q_wQ\",\"headerPrimaryText\":{\"runs\":[{\"text\":\"メンバー歴 \"},{\"text\":\"13\"},{\"text\":\" か月\"}]},\"message\":{\"runs\":[{\"text\":\"シオンちゃんおかえり！大好き！\"}]},\"authorName\":{\"simpleText\":\"atama\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLSRpro6cwncjDlybze0U0rVpF1GVeDXPkbhxCHV=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLSRpro6cwncjDlybze0U0rVpF1GVeDXPkbhxCHV=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"authorBadges\":[{\"liveChatAuthorBadgeRenderer\":{\"customThumbnail\":{\"thumbnails\":[{\"url\":\"https://yt3.ggpht.com/qabvdYzMJWP6yh-wTIk7c0kenIrl-6P5GY3ejegv31N9pdb3UWasxr4uv_YQq5u9IJHYT2aDnw=s16-c-k\"},{\"url\":\"https://yt3.ggpht.com/qabvdYzMJWP6yh-wTIk7c0kenIrl-6P5GY3ejegv31N9pdb3UWasxr4uv_YQq5u9IJHYT2aDnw=s32-c-k\"}]},\"tooltip\":\"メンバー（1 年）\",\"accessibility\":{\"accessibilityData\":{\"label\":\"メンバー（1 年）\"}}}}],\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CPABEOH9BiITCLiimeG8uPMCFeWAYwYdXCQIjQ==\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2pFS0x3b3RRMHBRUWpSS1MyOXZYMDFEUmxSeWRUUjNZMlEyYW5ORlMwRXRURzk1VFdWelNVUXRNekkyTnpJek1UTTNHaWtxSndvWVZVTllWSEJHYzE4elVIRkpOREZ4V0RKa09YUk1NbEozRWd0bFNrcDFlVFZ5V1RVM2R5QUNLQVF5R2dvWVZVTXRjWEJNYUdwdU4ycE5kak5hZEZBNWRWOVJYM2RS\"}},\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"コメントの操作\"}},\"trackingParams\":\"CPABEOH9BiITCLiimeG8uPMCFeWAYwYdXCQIjQ==\"}}}}";
            var a = ActionFactory.Parse(raw);
            if (a is not MemberShip member)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UC-qpLhjn7jMv3ZtP9u_Q_wQ", member.AuthorExternalChannelId);
            Assert.AreEqual("atama", member.AuthorName);
            Assert.AreEqual("Ci8KLUNKUEI0Sktvb19NQ0ZUcnU0d2NkNmpzRUtBLUxveU1lc0lELTMyNjcyMzEzNw%3D%3D", member.Id);
            Assert.AreEqual(1633615685384473, member.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("メンバー歴 "),
                new TextPart("13"),
                new TextPart(" か月"),
            }, member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>(), member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart> { new TextPart("シオンちゃんおかえり！大好き！") }, member.MessageItems);
        }
        [Test]
        public void ParseNormalMembershipTest()
        {
            var raw = "{\"clickTrackingParams\":\"CAEQl98BIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\",\"addChatItemAction\":{\"item\":{\"liveChatMembershipItemRenderer\":{\"id\":\"ChwKGkNLN0Q4WTcyaHZRQ0ZhMFRyUVlkQmVNSEpn\",\"timestampUsec\":\"1636311167306349\",\"authorExternalChannelId\":\"UC1THu2OG8m7uS46YeBW8iiA\",\"headerSubtext\":{\"runs\":[{\"text\":\"Member sheep\"},{\"text\":\" へようこそ！\"}]},\"authorName\":{\"simpleText\":\"ワイマイカりパファ\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLQKu_mziWOWRhIaseSpZo0Uf-CFgGOX2XeC4A=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLQKu_mziWOWRhIaseSpZo0Uf-CFgGOX2XeC4A=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"authorBadges\":[{\"liveChatAuthorBadgeRenderer\":{\"customThumbnail\":{\"thumbnails\":[{\"url\":\"https://yt3.ggpht.com/UanRDIHj88A3kRRt5CF2jgjboyCEbv_p3cXxpDhvBz5IvfQzKLpDEkcpDuvogDnJuL914cVX=s16-c-k\"},{\"url\":\"https://yt3.ggpht.com/UanRDIHj88A3kRRt5CF2jgjboyCEbv_p3cXxpDhvBz5IvfQzKLpDEkcpDuvogDnJuL914cVX=s32-c-k\"}]},\"tooltip\":\"新規メンバー\",\"accessibility\":{\"accessibilityData\":{\"label\":\"新規メンバー\"}}}}],\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CBEQ4P0GIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHMzUkRoWk56Sm9kbEZEUm1Fd1ZISlJXV1JDWlUxSVNtY2FLU29uQ2hoVlEzRnRNMEpSVEd4S1puWnJWSE5ZWDJoMmJUQlZiVUVTQzFjelZGSlNXa3d3WWtkM0lBSW9CRElhQ2hoVlF6RlVTSFV5VDBjNGJUZDFVelEyV1dWQ1Z6aHBhVUUlM0Q=\"}},\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"コメントの操作\"}},\"trackingParams\":\"CBEQ4P0GIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\"}}}}";
            var a = ActionFactory.Parse(raw);
            if (a is not MemberShip member)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UC1THu2OG8m7uS46YeBW8iiA", member.AuthorExternalChannelId);
            Assert.AreEqual("ワイマイカりパファ", member.AuthorName);
            Assert.AreEqual("ChwKGkNLN0Q4WTcyaHZRQ0ZhMFRyUVlkQmVNSEpn", member.Id);
            Assert.AreEqual(1636311167306349, member.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>(), member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("Member sheep"),
                new TextPart(" へようこそ！"),
            }, member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart>(), member.MessageItems);
        }

        [Test]
        public void MemberMilestoneChatTest()
        {
            var raw = "{ \"clickTrackingParams\": \"CAEQl98BIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\", \"addChatItemAction\": { \"item\": { \"liveChatMembershipItemRenderer\": { \"id\": \"Ci8KLUNLQ0MxX0xBaF9RQ0ZTOEZoQW9kb0JFQTVBLUxveU1lc0lELTMyNzI5MTkzMQ%3D%3D\", \"timestampUsec\": \"1636459655369057\", \"authorExternalChannelId\": \"UC_mV25pZDNAyev6B4nGAfqQ\", \"headerPrimaryText\": { \"runs\": [ { \"text\": \"メンバー歴 \" }, { \"text\": \"3\" }, { \"text\": \" か月\" } ] }, \"headerSubtext\": { \"simpleText\": \"うどん\" }, \"message\": { \"runs\": [ { \"text\": \"8\" } ] }, \"authorName\": { \"simpleText\": \"Fong\" }, \"authorPhoto\": { \"thumbnails\": [ { \"url\": \"https://yt4.ggpht.com/ytc/AKedOLQxO0uZRUPj0EmkpL73enICkZLwRxcVDq6dyMO39w=s32-c-k-c0x00ffffff-no-rj\", \"width\": 32, \"height\": 32 }, { \"url\": \"https://yt4.ggpht.com/ytc/AKedOLQxO0uZRUPj0EmkpL73enICkZLwRxcVDq6dyMO39w=s64-c-k-c0x00ffffff-no-rj\", \"width\": 64, \"height\": 64 } ] }, \"authorBadges\": [ { \"liveChatAuthorBadgeRenderer\": { \"customThumbnail\": { \"thumbnails\": [ { \"url\": \"https://yt3.ggpht.com/mG7DlQTEhdQ_KxMSe_S6VsXJ5n4vtb4FkQJAF8KqI5wiCcf1YPcokVbaJpy74sKv58hOlGEcwN8=s16-c-k\" }, { \"url\": \"https://yt3.ggpht.com/mG7DlQTEhdQ_KxMSe_S6VsXJ5n4vtb4FkQJAF8KqI5wiCcf1YPcokVbaJpy74sKv58hOlGEcwN8=s32-c-k\" } ] }, \"tooltip\": \"メンバー（2 か月）\", \"accessibility\": { \"accessibilityData\": { \"label\": \"メンバー（2 か月）\" } } } } ], \"contextMenuEndpoint\": { \"clickTrackingParams\": \"CAUQ4f0GIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\", \"commandMetadata\": { \"webCommandMetadata\": { \"ignoreNavigation\": true } }, \"liveChatItemContextMenuEndpoint\": { \"params\": \"Q2pFS0x3b3RRMHREUXpGZlRFRm9YMUZEUmxNNFJtaEJiMlJ2UWtWQk5VRXRURzk1VFdWelNVUXRNekkzTWpreE9UTXhHaWtxSndvWVZVTlJNRlZFVEZGRGFsa3djbTExZUVORVJUTTRSa2RuRWd0blpIbHhNRlExVGtST1FTQUNLQUV5R2dvWVZVTmZiVll5TlhCYVJFNUJlV1YyTmtJMGJrZEJabkZS\" } }, \"contextMenuAccessibility\": { \"accessibilityData\": { \"label\": \"コメントの操作\" } }, \"trackingParams\": \"CAUQ4f0GIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\" } } } }";
            var a = ActionFactory.Parse(raw);
            if (a is not MemberShip member)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UC_mV25pZDNAyev6B4nGAfqQ", member.AuthorExternalChannelId);
            Assert.AreEqual("Fong", member.AuthorName);
            Assert.AreEqual("Ci8KLUNLQ0MxX0xBaF9RQ0ZTOEZoQW9kb0JFQTVBLUxveU1lc0lELTMyNzI5MTkzMQ%3D%3D", member.Id);
            Assert.AreEqual(1636459655369057, member.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("メンバー歴 "),
                new TextPart("3"),
                new TextPart(" か月"),
            }, member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("うどん"),
            }, member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart> {new TextPart("8") }, member.MessageItems);
        }
    }
}