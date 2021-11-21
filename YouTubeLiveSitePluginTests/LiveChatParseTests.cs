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
            var liveChat=LiveChat.Parse(s);
            Assert.AreEqual("103208314919748213421", liveChat.YtCfg.DelegatedSessionId);
            Assert.AreEqual("QUFFLUhqazJ3MlF5aWxmbkFOaFhyOUFKSG9kaE10d19zUXw=", liveChat.YtCfg.IdToken);
            Assert.AreEqual("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8", liveChat.YtCfg.InnertubeApiKey);
            Assert.IsTrue(liveChat.YtCfg.InnertubeContext.StartsWith("{"));
            Assert.AreEqual("QUFFLUhqbVRHeXp6U3F3TE8wS0RIaUNhdS1sVlo2WERTQXxBQ3Jtc0ttY1o0OV9kNlFaWkNWQ3B0YU9fQ0ZSTWZYZllweVBJXzJJZDRvc3pOdmNTeUh2azNlNGFWVzcFNWR3ZWU1dNUQ==", liveChat.YtCfg.XsrfToken);
            Assert.IsTrue(liveChat.YtCfg.IsLoggedIn);
            Assert.IsTrue(liveChat.YtInitialData.MessageSendButtonServiceEndpoint!.StartsWith("{"));
            Assert.AreEqual("CKuAp6qd8vICFQZEWAodY3oNlQ", liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix);
            Assert.AreEqual("0ofMyAN5GlhDaWtxSndvWVZVTXRhRTAyV1VwMVRsbFdRVzFWVjNobFNYSTVSbVZCRWd0TGVYcFBTV3Q1YUcxVVRSb1Q2cWpkdVFFTkNndExlWHBQU1d0NWFHMVVUU0FDMAFKFggAGAAgAFDx7Z6qnfLyAlgDeACiAQCCAQIIBA%3D%3D", liveChat.YtInitialData.JouiChatContinuation);
            Assert.AreEqual("0ofMyAN5GlhDaWtxSndvWVZVTXRhRTAyV1VwMVRsbFdRVzFWVjNobFNYSTVSbVZCRWd0TGVYcFBTV3Q1YUcxVVRSb1Q2cWpkdVFFTkNndExlWHBQU1d0NWFHMVVUU0FDMAFKFggAGAAgAFDx7Z6qnfLyAlgDeACiAQCCAQIIAQ%3D%3D", liveChat.YtInitialData.AllChatContinuation);
        }
        [Test]
        public void ParseLiveChatTest2()
        {
            var s = Tools.GetSampleData("LiveChat2.txt");
            var liveChat = LiveChat.Parse(s);
            Assert.AreEqual("103208314919748213421", liveChat.YtCfg.DelegatedSessionId);
            Assert.AreEqual("QUFFLUhqazJ3MlF5aWxmbkFOaFhyOUFKSG9kaE10d19zUXw=", liveChat.YtCfg.IdToken);
            Assert.AreEqual("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8", liveChat.YtCfg.InnertubeApiKey);
            Assert.IsTrue(liveChat.YtCfg.InnertubeContext.StartsWith("{"));
            Assert.AreEqual("QUFFLUhqbFd4ekdueXdsYTVrbWZLOWZIQVZQWFdJVVFwUXxBQ3Jtc0ttbUNNeXVPajVjbF95dUhYSWgyb2Y0YlQ2cUUwWUVwT0pOU0ZtQW5EQ3RidHBpSUVrdDdoaVZreUZEay1KeHZ5bFBrTzZvZy1LMGFpbzVZeVYtWndZenBNREhPcjJDY1poRGJLVW9XOGQ3T1hWSkdVbw==", liveChat.YtCfg.XsrfToken);
            Assert.IsTrue(liveChat.YtCfg.IsLoggedIn);
            Assert.IsTrue(liveChat.YtInitialData.MessageSendButtonServiceEndpoint!.StartsWith("{"));
            Assert.AreEqual("COSCmeO_gvQCFYJXhQodWpsFcQ", liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix);
            Assert.AreEqual("0ofMyAOBARpYQ2lrcUp3b1lWVU4xVkVGWVZHVjRjbWhsZEdKUFpUTjZaM05yU2tKUkVnc3dWRmRKU1hOaGMxaFBRUm9UNnFqZHVRRU5DZ3N3VkZkSlNYTmhjMWhQUVNBQzABSh4IABgAIABQnbWT47-C9AJYA3gAogEAqgECEACwAQCCAQIIBA%3D%3D", liveChat.YtInitialData.JouiChatContinuation);
            Assert.AreEqual("0ofMyAOBARpYQ2lrcUp3b1lWVU4xVkVGWVZHVjRjbWhsZEdKUFpUTjZaM05yU2tKUkVnc3dWRmRKU1hOaGMxaFBRUm9UNnFqZHVRRU5DZ3N3VkZkSlNYTmhjMWhQUVNBQzABSh4IABgAIABQnbWT47-C9AJYA3gAogEAqgECEACwAQCCAQIIAQ%3D%3D", liveChat.YtInitialData.AllChatContinuation);
        }
        /// <summary>
        /// DelegatedSessionIdとIdTokenが無い場合があった
        /// </summary>
        [Test]
        public void ParseLiveChatTest3()
        {
            var s = Tools.GetSampleData("LiveChat3.txt");
            var liveChat = LiveChat.Parse(s);
            Assert.IsNull(liveChat.YtCfg.DelegatedSessionId);
            Assert.IsNull( liveChat.YtCfg.IdToken);
            Assert.AreEqual("AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8", liveChat.YtCfg.InnertubeApiKey);
            Assert.IsTrue(liveChat.YtCfg.InnertubeContext.StartsWith("{"));
            Assert.IsTrue( liveChat.YtCfg.XsrfToken.StartsWith("QUFF"));
            Assert.IsFalse(liveChat.YtCfg.IsLoggedIn);
            Assert.IsNull(liveChat.YtInitialData.MessageSendButtonServiceEndpoint);
            Assert.IsNull(liveChat.YtInitialData.MessageSendButtonServiceEndpointClientIdPrefix);
            Assert.AreEqual("0ofMyAODARpYQ2lrcUp3b1lWVU5zWDJkRGVXSlBTbEpKWjA5WWR6WlJZalJ4U25wUkVndFpabVZ2TXpoU2JHNURheG9UNnFqZHVRRU5DZ3RaWm1Wdk16aFNiRzVEYXlBQzABSiAIABgAIABQ2Kfw0I-p9AJYA3gAogEAqgEEEAAaALABAIIBAggE", liveChat.YtInitialData.JouiChatContinuation);
            Assert.AreEqual("0ofMyAODARpYQ2lrcUp3b1lWVU5zWDJkRGVXSlBTbEpKWjA5WWR6WlJZalJ4U25wUkVndFpabVZ2TXpoU2JHNURheG9UNnFqZHVRRU5DZ3RaWm1Wdk16aFNiRzVEYXlBQzABSiAIABgAIABQ2Kfw0I-p9AJYA3gAogEAqgEEEAAaALABAIIBAggB", liveChat.YtInitialData.AllChatContinuation);
        }
    }
}