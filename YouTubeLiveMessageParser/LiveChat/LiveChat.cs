using System;
using System.Text.RegularExpressions;

namespace ryu_s.YouTubeLive.Message
{
    public class LiveChat
    {
        public YtCfg YtCfg { get; }
        public YtInitialData YtInitialData { get; }
        private LiveChat(YtCfg ytCfg, YtInitialData ytInitialData)
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
                throw new ParseException(ex,html);
            }
        }
        private static YtCfg ExtractYtCfg(string html)
        {
            var match = Regex.Match(html, "<script[^>]*>ytcfg\\.set\\(({.+?})\\);");//</script>");
            if (!match.Success)
            {
                throw new YtCfgParseException();
            }
            var raw = match.Groups[1].Value;
            return new YtCfg(raw);
        }
        private static YtInitialData ExtractYtInitialData(string html)
        {
            var match = Regex.Match(html, "<script[^>]*>window\\[\"ytInitialData\"\\]\\s*=\\s*({.+?});</script>");
            if (!match.Success)
            {
                throw new ParseException(html);
            }
            var raw = match.Groups[1].Value;
            return YtInitialData.Parse(raw);
        }
    }
}
