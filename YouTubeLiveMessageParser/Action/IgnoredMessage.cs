using Newtonsoft.Json;

namespace ryu_s.YouTubeLive.Message.Action
{
    /// <summary>
    /// 使い道が無さそうなやつ
    /// </summary>
    class IgnoredMessage : IAction
    {
        public IgnoredMessage(string raw)
        {
            Raw = raw;
        }
        public static IgnoredMessage Parse(dynamic json)
        {
            var raw = (string)json.ToString(Formatting.None);
            return new IgnoredMessage(raw);
        }
        public string Raw { get; }
    }
}
