using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Action
{
    /// <summary>
    /// メンバーシップ登録、メンバーシップメッセージ
    /// </summary>
    public class MemberShip : IAction
    {
        //メンバーシップ登録と月１かなんかで送れるメンバーシップメッセージ（正式名称ではない）で兼用される。
        //公式でどちらもliveChatMembershipItemRendererだからここでも同じものとして扱う。
        //公式のメッセージの見分け方は、headerPrimaryTextがあるかどうか。
        //メンバーシップ登録には無くて、メンバーシップメッセージにはある。
        //ちなみにメンバーシップ登録にはmessageも無いけど、メンバーシップメッセージにはあるやつと無いやつがある。多分。
        public IReadOnlyList<IMessagePart> HeaderPrimaryTextItems { get; private set; }
        public IReadOnlyList<IMessagePart> HeaderSubTextItems { get; private set; }
        public IReadOnlyList<IMessagePart> MessageItems { get; private set; }
        public string Id { get; private set; }
        public string? AuthorName { get; private set; }
        public long TimestampUsec { get; private set; }
        public Thumbnail2 AuthorPhoto { get; private set; }
        public string AuthorExternalChannelId { get; private set; }
        public List<IAuthorBadge> AuthorBadges { get; private set; }
        public static MemberShip Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static MemberShip Parse(dynamic json)
        {
            var renderer = json.item.liveChatMembershipItemRenderer;

            List<IMessagePart> headerPrimaryTextItems;
            if (renderer.ContainsKey("headerPrimaryText"))
            {
                headerPrimaryTextItems = ActionTools.RunsToString(renderer.headerPrimaryText);
            }
            else
            {
                headerPrimaryTextItems = new List<IMessagePart>();
            }
            List<IMessagePart> headerSubTextItems;
            if (renderer.ContainsKey("headerSubtext") && renderer.headerSubtext.ContainsKey("simpleText"))
            {
                headerSubTextItems = new List<IMessagePart> { new TextPart(ActionTools.SimpleTextToString(renderer.headerSubtext)) };
            }
            else if (renderer.ContainsKey("headerSubtext") && renderer.headerSubtext.ContainsKey("runs"))
            {
                headerSubTextItems = ActionTools.RunsToString(renderer.headerSubtext);
            }
            else
            {
                headerSubTextItems = new List<IMessagePart>();
            }
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

            return new MemberShip
            {
                AuthorBadges = authorBadges,
                AuthorExternalChannelId = authorExternalChannelId,
                AuthorName = authorName,
                AuthorPhoto = authorPhoto,
                Id = id,
                MessageItems = messageItems,
                TimestampUsec = timestampUsec,
                HeaderPrimaryTextItems = headerPrimaryTextItems,
                HeaderSubTextItems = headerSubTextItems,
            };
        }
    }
}
