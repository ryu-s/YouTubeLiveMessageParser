using NUnit.Framework;
using ryu_s.YouTubeLive.Message;

namespace YouTubeLiveSitePluginTests
{
    public class LiveChatParseTests
    {
        [Test]
        public void ParseLiveChatTest()
        {
            var s = Tools.GetSampleData("LiveChat.txt");
            var liveChat = LiveChat.Parse(new LiveChatHtml(s));
            Assert.That(liveChat.YtCfg.DelegatedSessionId, Is.EqualTo("103208314919748213421"));
            Assert.That(liveChat.YtCfg.IdToken, Is.EqualTo("QUFFLUhqazJ3MlF5aWxmbkFOaFhyOUFKSG9kaE10d19zUXw="));
            Assert.That(liveChat.YtCfg.InnertubeApiKey, Is.EqualTo("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"));
            Assert.That(liveChat.YtCfg.InnertubeContext, Does.StartWith("{"));
            Assert.That(liveChat.YtCfg.XsrfToken, Does.StartWith("QUFF"));
            Assert.That(liveChat.YtCfg.IsLoggedIn, Is.True);
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpoint, Does.StartWith("{"));
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix, Is.EqualTo("CKuAp6qd8vICFQZEWAodY3oNlQ"));
            Assert.That(liveChat.YtInitialData.JouiChatContinuation, Is.EqualTo("0ofMyAN5GlhDaWtxSndvWVZVTXRhRTAyV1VwMVRsbFdRVzFWVjNobFNYSTVSbVZCRWd0TGVYcFBTV3Q1YUcxVVRSb1Q2cWpkdVFFTkNndExlWHBQU1d0NWFHMVVUU0FDMAFKFggAGAAgAFDx7Z6qnfLyAlgDeACiAQCCAQIIBA%3D%3D"));
            Assert.That(liveChat.YtInitialData.AllChatContinuation, Is.EqualTo("0ofMyAN5GlhDaWtxSndvWVZVTXRhRTAyV1VwMVRsbFdRVzFWVjNobFNYSTVSbVZCRWd0TGVYcFBTV3Q1YUcxVVRSb1Q2cWpkdVFFTkNndExlWHBQU1d0NWFHMVVUU0FDMAFKFggAGAAgAFDx7Z6qnfLyAlgDeACiAQCCAQIIAQ%3D%3D"));
        }
        [Test]
        public void ParseLiveChatTest2()
        {
            var s = Tools.GetSampleData("LiveChat2.txt");
            var liveChat = LiveChat.Parse(new LiveChatHtml(s));
            Assert.That(liveChat.YtCfg.DelegatedSessionId, Is.EqualTo("103208314919748213421"));
            Assert.That(liveChat.YtCfg.IdToken, Is.EqualTo("QUFFLUhqazJ3MlF5aWxmbkFOaFhyOUFKSG9kaE10d19zUXw="));
            Assert.That(liveChat.YtCfg.InnertubeApiKey, Is.EqualTo("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"));
            Assert.That(liveChat.YtCfg.InnertubeContext, Does.StartWith("{"));
            Assert.That(liveChat.YtCfg.XsrfToken, Does.StartWith("QUFF"));
            Assert.That(liveChat.YtCfg.IsLoggedIn, Is.True);
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpoint, Does.StartWith("{"));
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix, Is.EqualTo("COSCmeO_gvQCFYJXhQodWpsFcQ"));
            Assert.That(liveChat.YtInitialData.JouiChatContinuation, Is.EqualTo("0ofMyAOBARpYQ2lrcUp3b1lWVU4xVkVGWVZHVjRjbWhsZEdKUFpUTjZaM05yU2tKUkVnc3dWRmRKU1hOaGMxaFBRUm9UNnFqZHVRRU5DZ3N3VkZkSlNYTmhjMWhQUVNBQzABSh4IABgAIABQnbWT47-C9AJYA3gAogEAqgECEACwAQCCAQIIBA%3D%3D"));
            Assert.That(liveChat.YtInitialData.AllChatContinuation, Is.EqualTo("0ofMyAOBARpYQ2lrcUp3b1lWVU4xVkVGWVZHVjRjbWhsZEdKUFpUTjZaM05yU2tKUkVnc3dWRmRKU1hOaGMxaFBRUm9UNnFqZHVRRU5DZ3N3VkZkSlNYTmhjMWhQUVNBQzABSh4IABgAIABQnbWT47-C9AJYA3gAogEAqgECEACwAQCCAQIIAQ%3D%3D"));
        }
        /// <summary>
        /// DelegatedSessionIdとIdTokenが無い場合があった
        /// </summary>
        [Test]
        public void ParseLiveChatTest3()
        {
            var s = Tools.GetSampleData("LiveChat3.txt");
            var liveChat = LiveChat.Parse(new LiveChatHtml(s));
            Assert.That(liveChat.YtCfg.DelegatedSessionId, Is.Null);
            Assert.That(liveChat.YtCfg.IdToken, Is.Null);
            Assert.That(liveChat.YtCfg.InnertubeApiKey, Is.EqualTo("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"));
            Assert.That(liveChat.YtCfg.InnertubeContext, Does.StartWith("{"));
            Assert.That(liveChat.YtCfg.XsrfToken, Does.StartWith("QUFF"));
            Assert.That(liveChat.YtCfg.IsLoggedIn, Is.False);
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpoint, Is.Null);
            Assert.That(liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix, Is.Null);
            Assert.That(liveChat.YtInitialData.JouiChatContinuation, Is.EqualTo("0ofMyAODARpYQ2lrcUp3b1lWVU5zWDJkRGVXSlBTbEpKWjA5WWR6WlJZalJ4U25wUkVndFpabVZ2TXpoU2JHNURheG9UNnFqZHVRRU5DZ3RaWm1Wdk16aFNiRzVEYXlBQzABSiAIABgAIABQ2Kfw0I-p9AJYA3gAogEAqgEEEAAaALABAIIBAggE"));
            Assert.That(liveChat.YtInitialData.AllChatContinuation, Is.EqualTo("0ofMyAODARpYQ2lrcUp3b1lWVU5zWDJkRGVXSlBTbEpKWjA5WWR6WlJZalJ4U25wUkVndFpabVZ2TXpoU2JHNURheG9UNnFqZHVRRU5DZ3RaWm1Wdk16aFNiRzVEYXlBQzABSiAIABgAIABQ2Kfw0I-p9AJYA3gAogEAqgEEEAAaALABAIIBAggB"));
        }
    }
}