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
        public static LiveChat Parse(LiveChatHtml html)
        {
            try
            {
                var ytCfg = ExtractYtCfg(html);
                var ytInitialData = ExtractYtInitialData(html);
                return new LiveChat(ytCfg, ytInitialData);
            }
            catch (Exception ex)
            {
                throw new ParseException(ex, html.Raw);
            }
        }
        private static LiveChatYtCfg ExtractYtCfg(LiveChatHtml html)
        {
            var match = Regex.Match(html.Raw, "<script[^>]*>ytcfg\\.set\\(({.+?})\\);");//</script>");
            if (!match.Success)
            {
                throw new YtCfgParseException();
            }
            var raw = match.Groups[1].Value;
            return new LiveChatYtCfg(raw);
        }
        private static LiveChatYtInitialData ExtractYtInitialData(LiveChatHtml html)
        {
            var match = Regex.Match(html.Raw, "<script[^>]*>window\\[\"ytInitialData\"\\]\\s*=\\s*({.+?});</script>");
            if (!match.Success)
            {
                throw new ParseException(html.Raw);
            }
            var raw = match.Groups[1].Value;
            return LiveChatYtInitialData.Parse(raw);
        }
    }
}
