using Newtonsoft.Json;
using System;

namespace ryu_s.YouTubeLive.Message
{
    public class LiveChatYtCfg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>コメント投稿時に必要</remarks>
        public string? DelegatedSessionId { get; }
        public string? IdToken { get; }
        public string InnertubeApiKey { get; }
        public string InnertubeContext { get; }
        public string XsrfToken { get; }
        public bool IsLoggedIn { get; }
        public LiveChatYtCfg(string json)
        {
            dynamic? obj = JsonConvert.DeserializeObject(json);
            if (obj == null)
            {
                throw new ArgumentException();
            }
            DelegatedSessionId = (string?)obj.DELEGATED_SESSION_ID;
            IdToken = (string?)obj.ID_TOKEN;
            InnertubeApiKey = (string)obj.INNERTUBE_API_KEY;
            InnertubeContext = (string)obj.INNERTUBE_CONTEXT.ToString(Formatting.None);
            XsrfToken = (string)obj.XSRF_TOKEN;
            IsLoggedIn = (bool)obj.LOGGED_IN;
        }
    }
}
