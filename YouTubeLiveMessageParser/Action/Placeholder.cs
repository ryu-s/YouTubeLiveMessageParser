using Newtonsoft.Json;
using System;

namespace ryu_s.YouTubeLive.Message.Action
{
    /// <summary>
    /// 配信者にはこのメッセージは確認待ちですってでるやつ
    /// </summary>
    public class Placeholder : IAction
    {
        public string Id { get; }
        public long TimestampUsec { get; }
        public string ClientId { get; }
        private Placeholder(string id, long timestampUsec, string clientId)
        {
            Id = id;
            TimestampUsec = timestampUsec;
            ClientId = clientId;
        }
        public static TextMessage Parse(string json)
        {
            dynamic? d = JsonConvert.DeserializeObject(json);
            if (d == null)
            {
                throw new ArgumentException();
            }
            return Parse(d);
        }
        public static Placeholder Parse(dynamic json)
        {
            var renderer = json.item.liveChatPlaceholderItemRenderer;
            var id = (string)renderer.id;
            var timestampUsec = (long)renderer.timestampUsec;
            var clientId = (string)json.clientId;
            return new Placeholder(id, timestampUsec, clientId);
        }
    }
}
