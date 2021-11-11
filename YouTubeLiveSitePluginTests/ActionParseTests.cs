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
            var s = @"{""addChatItemAction"":{""item"":{""liveChatTextMessageRenderer"":{""message"":{""runs"":[{""text"":""abc""}]},""authorName"":{""simpleText"":""name""},""authorPhoto"":{""thumbnails"":[{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s32-c-k-c0x00ffffff-no-rj"",""width"":32,""height"":32},{""url"":""https://yt4.ggpht.com/ytc/AKedOLTicsJrdOe2bVq7fTx5iNAvyEBPNkL8QcspMO0s=s64-c-k-c0x00ffffff-no-rj"",""width"":64,""height"":64}]},""id"":""id"",""timestampUsec"":""1631202054190683"",""authorBadges"":[{""liveChatAuthorBadgeRenderer"":{""customThumbnail"":{""thumbnails"":[{""url"":""https://example.com/1""},{""url"":""https://example.com/2""}]},""tooltip"":""�����o�[�i6 �����j""}}],""authorExternalChannelId"":""UC6vWy2N0Ochgx7uIeOPg9nQ""}},""clientId"":""CJSj3v2Y8vICFZYgYAodcEcE0g3""}}";
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
            var s = "{\"clickTrackingParams\":\"CAEQl98BIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"addChatItemAction\":{\"item\":{\"liveChatPaidMessageRenderer\":{\"id\":\"ChwKGkNLREw3djZoblBJQ0ZSZ1ByUVlkX25VRklR\",\"timestampUsec\":\"1628248435329905\",\"authorName\":{\"simpleText\":\"���炽���B\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLRhr_7inzkWxqDKyBereHQZugB8ecDCad=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLRhr_7inzkWxqDKyBereHQZugB8ecaW=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"purchaseAmountText\":{\"simpleText\":\"��300\"},\"message\":{\"runs\":[{\"text\":\"abcdefg\"}]},\"headerBackgroundColor\":4278237396,\"headerTextColor\":4278190080,\"bodyBackgroundColor\":4278248959,\"bodyTextColor\":4278190080,\"authorExternalChannelId\":\"UCsduCAn_VyCdvlqA_D36j9A\",\"authorNameTextColor\":3003121664,\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CAIQ7rsEIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHRFVERkMk5taHVVRWxEUmxKblVISlJXTRTk0VUhNMFdIWnpiVGhJTUZwNFdFZHBRbmNTQzFkTFYwMWZTR3d3Vlc0MElBRW9CRElhQ2hoVlEzTmtkVU5CYmw5V2VXUlNkbXh4UVY5RU16WnFPVUUlM0Q=\"}},\"timestampColor\":2147483648,\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"�R�����g�̑���\"}},\"trackingParams\":\"CAIQ7rsEIhMIg96XjaKc8gIVSUJYCh1RmgaF\"}}}}";
            var a = ActionFactory.Parse(s);
            if (a is not SuperChat text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UCsduCAn_VyCdvlqA_D36j9A", text.AuthorExternalChannelId);
            Assert.AreEqual("��300", text.PurchaseAmount);
            Assert.AreEqual("���炽���B", text.AuthorName);
            Assert.AreEqual("ChwKGkNLREw3djZoblBJQ0ZSZ1ByUVlkX25VRklR", text.Id);
            Assert.AreEqual(1628248435329905, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart> {
                new TextPart("abcdefg")
            }, text.MessageItems);

        }
        [Test]
        public void ParseSuperchatNoMessageTest()
        {
            var s = "{\"clickTrackingParams\":\"CAEQl98BIhMIg96XjaKc8gIVSUJYCh1RmgaF\",\"addChatItemAction\":{\"item\":{\"liveChatPaidMessageRenderer\":{\"id\":\"ChwKGkNKZXZ2Y2VFaHZRQ0ZiNE1yUVlkOUpRQ1Vn\",\"timestampUsec\":\"1636280729048459\",\"authorName\":{\"simpleText\":\"�֓��Ђ낵\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTXI1_Vgn1T_fbtAYFKVG4yMMZ53GU7nleM8w=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTXI1_Vgn1T_fbtAYFKVG4yMMZ53GU7nleM8w=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"purchaseAmountText\":{\"simpleText\":\"NT$15.00\"},\"headerBackgroundColor\":4279592384,\"headerTextColor\":4294967295,\"bodyBackgroundColor\":4280191205,\"bodyTextColor\":4294967295,\"authorExternalChannelId\":\"UCg6fWFQ93ETVFUQ2PVXN6ag\",\"authorNameTextColor\":3019898879,\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CAIQ7rsEIhMI5rzw3YSG9AIVEFpYCh0xsQTM\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHBsZG5aalpVVm9kbEZEUm1JMFRYSlJXV1E1U2xGRFZXY2FLU29uQ2hoVlF6bHlkVlpaVUhZM2VVcHRWakJTYURCT1MwRXRUSGNTQzJ0Zk9HdzRNVkpPYTNkSklBSW9CRElhQ2hoVlEyYzJabGRHVVRrelJWUldSbFZSTWxCV1dFNDJZV2MlM0Q=\"}},\"timestampColor\":2164260863,\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"�R�����g�̑���\"}},\"trackingParams\":\"CAIQ7rsEIhMI5rzw3YSG9AIVEFpYCh0xsQTM\"}}}}";
            var a = ActionFactory.Parse(s);
            if (a is not SuperChat text)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UCg6fWFQ93ETVFUQ2PVXN6ag", text.AuthorExternalChannelId);
            Assert.AreEqual("NT$15.00", text.PurchaseAmount);
            Assert.AreEqual("�֓��Ђ낵", text.AuthorName);
            Assert.AreEqual("ChwKGkNKZXZ2Y2VFaHZRQ0ZiNE1yUVlkOUpRQ1Vn", text.Id);
            Assert.AreEqual(1636280729048459, text.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>(), text.MessageItems);
        }
        [Test]
        public void ParseMonthlySpecialMembershipTest()
        {
            var raw = "{\"clickTrackingParams\":\"CAEQl98BIhMIuKKZ4by48wIV5YBjBh1cJAiN\",\"addChatItemAction\":{\"item\":{\"liveChatMembershipItemRenderer\":{\"id\":\"Ci8KLUNKUEI0Sktvb19NQ0ZUcnU0d2NkNmpzRUtBLUxveU1lc0lELTMyNjcyMzEzNw%3D%3D\",\"timestampUsec\":\"1633615685384473\",\"authorExternalChannelId\":\"UC-qpLhjn7jMv3ZtP9u_Q_wQ\",\"headerPrimaryText\":{\"runs\":[{\"text\":\"�����o�[�� \"},{\"text\":\"13\"},{\"text\":\" ����\"}]},\"message\":{\"runs\":[{\"text\":\"�V�I������񂨂�����I��D���I\"}]},\"authorName\":{\"simpleText\":\"atama\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLSRpro6cwncjDlybze0U0rVpF1GVeDXPkbhxCHV=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLSRpro6cwncjDlybze0U0rVpF1GVeDXPkbhxCHV=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"authorBadges\":[{\"liveChatAuthorBadgeRenderer\":{\"customThumbnail\":{\"thumbnails\":[{\"url\":\"https://yt3.ggpht.com/qabvdYzMJWP6yh-wTIk7c0kenIrl-6P5GY3ejegv31N9pdb3UWasxr4uv_YQq5u9IJHYT2aDnw=s16-c-k\"},{\"url\":\"https://yt3.ggpht.com/qabvdYzMJWP6yh-wTIk7c0kenIrl-6P5GY3ejegv31N9pdb3UWasxr4uv_YQq5u9IJHYT2aDnw=s32-c-k\"}]},\"tooltip\":\"�����o�[�i1 �N�j\",\"accessibility\":{\"accessibilityData\":{\"label\":\"�����o�[�i1 �N�j\"}}}}],\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CPABEOH9BiITCLiimeG8uPMCFeWAYwYdXCQIjQ==\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2pFS0x3b3RRMHBRUWpSS1MyOXZYMDFEUmxSeWRUUjNZMlEyYW5ORlMwRXRURzk1VFdWelNVUXRNekkyTnpJek1UTTNHaWtxSndvWVZVTllWSEJHYzE4elVIRkpOREZ4V0RKa09YUk1NbEozRWd0bFNrcDFlVFZ5V1RVM2R5QUNLQVF5R2dvWVZVTXRjWEJNYUdwdU4ycE5kak5hZEZBNWRWOVJYM2RS\"}},\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"�R�����g�̑���\"}},\"trackingParams\":\"CPABEOH9BiITCLiimeG8uPMCFeWAYwYdXCQIjQ==\"}}}}";
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
                new TextPart("�����o�[�� "),
                new TextPart("13"),
                new TextPart(" ����"),
            }, member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>(), member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart> { new TextPart("�V�I������񂨂�����I��D���I") }, member.MessageItems);
        }
        [Test]
        public void ParseNormalMembershipTest()
        {
            var raw = "{\"clickTrackingParams\":\"CAEQl98BIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\",\"addChatItemAction\":{\"item\":{\"liveChatMembershipItemRenderer\":{\"id\":\"ChwKGkNLN0Q4WTcyaHZRQ0ZhMFRyUVlkQmVNSEpn\",\"timestampUsec\":\"1636311167306349\",\"authorExternalChannelId\":\"UC1THu2OG8m7uS46YeBW8iiA\",\"headerSubtext\":{\"runs\":[{\"text\":\"Member sheep\"},{\"text\":\" �ւ悤�����I\"}]},\"authorName\":{\"simpleText\":\"���C�}�C�J��p�t�@\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLQKu_mziWOWRhIaseSpZo0Uf-CFgGOX2XeC4A=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLQKu_mziWOWRhIaseSpZo0Uf-CFgGOX2XeC4A=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"authorBadges\":[{\"liveChatAuthorBadgeRenderer\":{\"customThumbnail\":{\"thumbnails\":[{\"url\":\"https://yt3.ggpht.com/UanRDIHj88A3kRRt5CF2jgjboyCEbv_p3cXxpDhvBz5IvfQzKLpDEkcpDuvogDnJuL914cVX=s16-c-k\"},{\"url\":\"https://yt3.ggpht.com/UanRDIHj88A3kRRt5CF2jgjboyCEbv_p3cXxpDhvBz5IvfQzKLpDEkcpDuvogDnJuL914cVX=s32-c-k\"}]},\"tooltip\":\"�V�K�����o�[\",\"accessibility\":{\"accessibilityData\":{\"label\":\"�V�K�����o�[\"}}}}],\"contextMenuEndpoint\":{\"clickTrackingParams\":\"CBEQ4P0GIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\",\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2g0S0hBb2FRMHMzUkRoWk56Sm9kbEZEUm1Fd1ZISlJXV1JDWlUxSVNtY2FLU29uQ2hoVlEzRnRNMEpSVEd4S1puWnJWSE5ZWDJoMmJUQlZiVUVTQzFjelZGSlNXa3d3WWtkM0lBSW9CRElhQ2hoVlF6RlVTSFV5VDBjNGJUZDFVelEyV1dWQ1Z6aHBhVUUlM0Q=\"}},\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"�R�����g�̑���\"}},\"trackingParams\":\"CBEQ4P0GIhMIuI-jtvaG9AIVB5hWAR2EdA0Y\"}}}}";
            var a = ActionFactory.Parse(raw);
            if (a is not MemberShip member)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("UC1THu2OG8m7uS46YeBW8iiA", member.AuthorExternalChannelId);
            Assert.AreEqual("���C�}�C�J��p�t�@", member.AuthorName);
            Assert.AreEqual("ChwKGkNLN0Q4WTcyaHZRQ0ZhMFRyUVlkQmVNSEpn", member.Id);
            Assert.AreEqual(1636311167306349, member.TimestampUsec);
            Assert.AreEqual(new List<IMessagePart>(), member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("Member sheep"),
                new TextPart(" �ւ悤�����I"),
            }, member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart>(), member.MessageItems);
        }

        [Test]
        public void MemberMilestoneChatTest()
        {
            var raw = "{ \"clickTrackingParams\": \"CAEQl98BIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\", \"addChatItemAction\": { \"item\": { \"liveChatMembershipItemRenderer\": { \"id\": \"Ci8KLUNLQ0MxX0xBaF9RQ0ZTOEZoQW9kb0JFQTVBLUxveU1lc0lELTMyNzI5MTkzMQ%3D%3D\", \"timestampUsec\": \"1636459655369057\", \"authorExternalChannelId\": \"UC_mV25pZDNAyev6B4nGAfqQ\", \"headerPrimaryText\": { \"runs\": [ { \"text\": \"�����o�[�� \" }, { \"text\": \"3\" }, { \"text\": \" ����\" } ] }, \"headerSubtext\": { \"simpleText\": \"���ǂ�\" }, \"message\": { \"runs\": [ { \"text\": \"8\" } ] }, \"authorName\": { \"simpleText\": \"Fong\" }, \"authorPhoto\": { \"thumbnails\": [ { \"url\": \"https://yt4.ggpht.com/ytc/AKedOLQxO0uZRUPj0EmkpL73enICkZLwRxcVDq6dyMO39w=s32-c-k-c0x00ffffff-no-rj\", \"width\": 32, \"height\": 32 }, { \"url\": \"https://yt4.ggpht.com/ytc/AKedOLQxO0uZRUPj0EmkpL73enICkZLwRxcVDq6dyMO39w=s64-c-k-c0x00ffffff-no-rj\", \"width\": 64, \"height\": 64 } ] }, \"authorBadges\": [ { \"liveChatAuthorBadgeRenderer\": { \"customThumbnail\": { \"thumbnails\": [ { \"url\": \"https://yt3.ggpht.com/mG7DlQTEhdQ_KxMSe_S6VsXJ5n4vtb4FkQJAF8KqI5wiCcf1YPcokVbaJpy74sKv58hOlGEcwN8=s16-c-k\" }, { \"url\": \"https://yt3.ggpht.com/mG7DlQTEhdQ_KxMSe_S6VsXJ5n4vtb4FkQJAF8KqI5wiCcf1YPcokVbaJpy74sKv58hOlGEcwN8=s32-c-k\" } ] }, \"tooltip\": \"�����o�[�i2 �����j\", \"accessibility\": { \"accessibilityData\": { \"label\": \"�����o�[�i2 �����j\" } } } } ], \"contextMenuEndpoint\": { \"clickTrackingParams\": \"CAUQ4f0GIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\", \"commandMetadata\": { \"webCommandMetadata\": { \"ignoreNavigation\": true } }, \"liveChatItemContextMenuEndpoint\": { \"params\": \"Q2pFS0x3b3RRMHREUXpGZlRFRm9YMUZEUmxNNFJtaEJiMlJ2UWtWQk5VRXRURzk1VFdWelNVUXRNekkzTWpreE9UTXhHaWtxSndvWVZVTlJNRlZFVEZGRGFsa3djbTExZUVORVJUTTRSa2RuRWd0blpIbHhNRlExVGtST1FTQUNLQUV5R2dvWVZVTmZiVll5TlhCYVJFNUJlV1YyTmtJMGJrZEJabkZS\" } }, \"contextMenuAccessibility\": { \"accessibilityData\": { \"label\": \"�R�����g�̑���\" } }, \"trackingParams\": \"CAUQ4f0GIhMIxum6pZ-L9AIVpeE4Bh3DeQ-3\" } } } }";
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
                new TextPart("�����o�[�� "),
                new TextPart("3"),
                new TextPart(" ����"),
            }, member.HeaderPrimaryTextItems);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("���ǂ�"),
            }, member.HeaderSubTextItems);
            Assert.AreEqual(new List<IMessagePart> {new TextPart("8") }, member.MessageItems);
        }
        [Test]
        public void ParseRemoveBannerForLiveChatCommandTest()
        {
            var raw = "{\"removeBannerForLiveChatCommand\":{\"targetActionId\":\"ChwKGkNQR0lyOWpTamZRQ0ZXb0dyUVlkX0U4R3dB\"}}";
            var a = ActionFactory.Parse(raw);
            if (a is not IgnoredMessage ignored)
            {
                Assert.Fail();
                return;
            }
        }
        [Test]
        public void ParseModeChangeMessageTest()
        {
            var raw = "{\"addChatItemAction\":{\"item\":{\"liveChatModeChangeMessageRenderer\":{\"id\":\"ChwKGkNKZWd5dUd2amZRQ0ZUVWI1d29kZmdNTFJn\",\"timestampUsec\":\"1636532799875477\",\"icon\":{\"iconType\":\"SLOW_MODE\"},\"text\":{\"runs\":[{\"text\":\"�ᑬ���[�h���L���ł�\",\"bold\":true}]},\"subtext\":{\"runs\":[{\"text\":\"10�b\",\"italics\":true},{\"text\":\"���ƂɃ��b�Z�[�W�𑗐M���܂�\",\"italics\":true}]}}}}}";
            var a = ActionFactory.Parse(raw);
            if (a is not ModeChangeMessage modeChange)
            {
                Assert.Fail();
                return;
            }
        }
        [Test]
        public void ParseLiveChatAutoModMessageTest()
        {
            var raw = "{\"addChatItemAction\":{\"item\":{\"liveChatAutoModMessageRenderer\":{\"id\":\"CjoKGkNLcUM3c0hwamZRQkZmdNTFJnEhxDSTZPN3Vfb2pmUUNGUXMxandvZHViQUgtQS0z\",\"timestampUsec\":\"1636548302569808\",\"autoModeratedItem\":{\"liveChatTextMessageRenderer\":{\"message\":{\"runs\":[{\"text\":\"����ς肱�̐l�B�B�B�n�����B\"}]},\"authorName\":{\"simpleText\":\"y h\"},\"authorPhoto\":{\"thumbnails\":[{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTrEop9FvheO48wNB2vq3rzlVk_RZEslN-zLg=s32-c-k-c0x00ffffff-no-rj\",\"width\":32,\"height\":32},{\"url\":\"https://yt4.ggpht.com/ytc/AKedOLTrEop9FvheO48wNB2vq3rzlVk_RZEslN-zLg=s64-c-k-c0x00ffffff-no-rj\",\"width\":64,\"height\":64}]},\"contextMenuEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"ignoreNavigation\":true}},\"liveChatItemContextMenuEndpoint\":{\"params\":\"Q2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE1hSMyZFZhV3hZVVRGbFZFd3dJQUlvQVRJYUNoaFZRM1JEYmxCWk5uTlVXVVpPUVhwSVRVVlNOMlJzWW5jJTNE\"}},\"id\":\"CjoKGkNLcUM3c0hwamZRQkZmdNTFJnEhxDSTZPN3Vfb2pmUUNGUXMxandvZHViQUgtQS0z\",\"timestampUsec\":\"1636548302569808\",\"authorExternalChannelId\":\"UCtCnPY6sTYFNAzHMER7dlbw\",\"contextMenuAccessibility\":{\"accessibilityData\":{\"label\":\"�R�����g�̑���\"}}}},\"headerText\":{\"runs\":[{\"text\":\"���̃��b�Z�[�W�͊m�F�҂��ł��B\"}]},\"infoDialogButton\":{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"icon\":{\"iconType\":\"WARNING\"},\"navigationEndpoint\":{\"liveChatDialogEndpoint\":{\"content\":{\"liveChatDialogRenderer\":{\"title\":{\"runs\":[{\"text\":\"�K�؂łȂ��\�������郁�b�Z�[�W�͊m�F�҂��ƂȂ�܂�\"}]},\"dialogMessages\":[{\"runs\":[{\"text\":\"�m�F�҂��̃`���b�g ���b�Z�[�W�͎����ɂ͕\������܂����A���J����܂���B���J����ɂ� [�\��]�A��\���̂܂܂ɂ���ɂ� [��\��] ��I�����Ă��������B\"}]},{\"runs\":[{\"text\":\"���̋@�\��\"},{\"text\":\"�ݒ�\",\"navigationEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"url\":\"https://studio.youtube.com/channel/UC?d=sd&sds=community\",\"webPageType\":\"WEB_PAGE_TYPE_UNKNOWN\",\"rootVe\":83769}},\"urlEndpoint\":{\"url\":\"https://studio.youtube.com/channel/UC?d=sd&sds=community\",\"target\":\"TARGET_NEW_WINDOW\"}}},{\"text\":\"�Ŗ����ɂł��܂��B\"}]}],\"confirmButton\":{\"buttonRenderer\":{\"style\":\"STYLE_BLUE_TEXT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"text\":{\"simpleText\":\"OK\"},\"accessibility\":{\"label\":\"OK\"}}}}}}},\"accessibility\":{\"label\":\"�ڍ�\"},\"tooltip\":\"�ڍ�\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"�ڍ�\"}}}},\"moderationButtons\":[{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"text\":{\"simpleText\":\"�\��\"},\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NRUktQ2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE5RQWxnQw==\"}},\"accessibility\":{\"label\":\"�\��\"}}},{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"text\":{\"simpleText\":\"��\��\"},\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NRW8tQ2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE5RQWxnQw==\"}},\"accessibility\":{\"label\":\"��\��\"}}}],\"authorExternalChannelId\":\"UCtCnPY6sTYFNBzHMER7dlbk\",\"inlineActionButtons\":[{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NQkktQ2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE5RQWxnRA==\"}},\"icon\":{\"iconType\":\"DELETE\"},\"accessibility\":{\"label\":\"�폜\"},\"tooltip\":\"�폜\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"�폜\"}}}},{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NRElZQ2haMFEyNVFXVFp6VkZsR1RrRjZTRTFGVWpka2JHSjNVQUpZQXclM0QlM0Q=\"}},\"icon\":{\"iconType\":\"HOURGLASS\"},\"accessibility\":{\"label\":\"���[�U�[���^�C���A�E�g�ɂ���\"},\"tooltip\":\"���[�U�[���^�C���A�E�g�ɂ���\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"���[�U�[���^�C���A�E�g�ɂ���\"}}}},{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NQ0lZQ2haMFEyNVFXVFp6VkZsR1RrRjZTRTFGVWpka2JHSjNVQUpZQXclM0QlM0Q=\"}},\"icon\":{\"iconType\":\"REMOVE_CIRCLE\"},\"accessibility\":{\"label\":\"���̃`�����l���̃��[�U�[��\�����Ȃ�\"},\"tooltip\":\"���̃`�����l���̃��[�U�[��\�����Ȃ�\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"���̃`�����l���̃��[�U�[��\�����Ȃ�\"}}}}],\"additionalInlineActionButtons\":[{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NRUktQ2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE5RQWxnRA==\"}},\"icon\":{\"iconType\":\"VISIBILITY\"},\"accessibility\":{\"label\":\"�\��\"},\"tooltip\":\"�\��\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"�\��\"}}}},{\"buttonRenderer\":{\"style\":\"STYLE_DEFAULT\",\"size\":\"SIZE_DEFAULT\",\"isDisabled\":false,\"serviceEndpoint\":{\"commandMetadata\":{\"webCommandMetadata\":{\"sendPost\":true,\"apiUrl\":\"/youtubei/v1/live_chat/moderate\"}},\"moderateLiveChatEndpoint\":{\"params\":\"Q2lrcUp3b1lWVU52VDNOWlQwbFBXWGhLU0VaQk9HaE9ZbE5UWW5KQkVndG5WV2xzV0ZFeFpWUk1NRW8tQ2p3S09nb2FRMHR4UXpkelNIQnFabEZEUmxSVllqVjNiMlJtWjAxTVVtY1NIRU5KTms4M2RWOXZhbVpSUTBaUmN6RnFkMjlrZFdKQlNDMUJMVE5RQWxnRA==\"}},\"icon\":{\"iconType\":\"VISIBILITY_OFF\"},\"accessibility\":{\"label\":\"��\��\"},\"tooltip\":\"��\��\",\"accessibilityData\":{\"accessibilityData\":{\"label\":\"��\��\"}}}}]}},\"clientId\":\"CI6O7u_ojfQCFQs1jwodubAH-A-3\"}}";
            var a = ActionFactory.Parse(raw);
            if (a is not LiveChatAutoModMessage autoMod)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual("y h", autoMod.AutoModeratedItemAuthorName);
            Assert.AreEqual(new List<IMessagePart>
            {
                new TextPart("����ς肱�̐l�B�B�B�n�����B"),
            }, autoMod.AutoModeratedItemMessage);
            Assert.AreEqual("CjoKGkNLcUM3c0hwamZRQkZmdNTFJnEhxDSTZPN3Vfb2pmUUNGUXMxandvZHViQUgtQS0z", autoMod.Id);
            Assert.AreEqual("1636548302569808", autoMod.TimestampUsec);
        }
    }
}