using System;
using System.Text.RegularExpressions;

namespace ryu_s.YouTubeLive.Message
{
    public class LiveChat
    {
        public LiveChatYtCfg YtCfg { get; }
        public LiveChatYtInitialData YtInitialData { get; }
        private LiveChat(LiveChatYtCfg ytCfg, LiveChatYtInitialData ytInitialData)
        {
            YtCfg = ytCfg;
            YtInitialData = ytInitialData;
        }
        public static LiveChat Parse(string html)
        {
            try
            {
                var ytCfg = ExtractYtCfg(html);
                var ytInitialData = ExtractYtInitialData(html);
                return new LiveChat(ytCfg, ytInitialData);
            }
            catch (Exception ex)
            {
                throw new ParseException(ex, html);
            }
        }
        private static LiveChatYtCfg ExtractYtCfg(string html)
        {
            var match = Regex.Match(html, "<script[^>]*>ytcfg\\.set\\(({.+?})\\);");//</script>");
            if (!match.Success)
            {
                throw new YtCfgParseException();
            }
            var raw = match.Groups[1].Value;
            return new LiveChatYtCfg(raw);
        }
        private static LiveChatYtInitialData ExtractYtInitialData(string html)
        {
            var match = Regex.Match(html, "<script[^>]*>window\\[\"ytInitialData\"\\]\\s*=\\s*({.+?});</script>");
            if (!match.Success)
            {
                throw new ParseException(html);
            }
            var raw = match.Groups[1].Value;
            return LiveChatYtInitialData.Parse(raw);
        }
    }
}
