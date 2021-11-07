using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using ryu_s.YouTubeLive.Message.Action;
using ryu_s.YouTubeLive.Message.Continuation;

namespace ryu_s.YouTubeLive.Message
{
    public class YtInitialData
    {
        public IContinuation? Continuation { get; }
        public List<IAction> Actions { get; }
        public string? MessageSendButtonServiceEndpoint { get; }
        public string? MessageSendButtonServiceEndpointClientIdPrefix { get; }
        public string JouiChatContinuation { get; }
        public string AllChatContinuation { get; }
        public YtInitialData(IContinuation? continuation, List<IAction> actions, string? endpoint, string? clientIdPrefix, string jouiChatContinuation, string allChatContinuation)
        {
            Continuation = continuation;
            Actions = actions;
            MessageSendButtonServiceEndpoint = endpoint;
            MessageSendButtonServiceEndpointClientIdPrefix = clientIdPrefix;
            JouiChatContinuation = jouiChatContinuation;
            AllChatContinuation = allChatContinuation;
        }
        public static YtInitialData Parse(string raw)
        {
            dynamic? obj = JsonConvert.DeserializeObject(raw);
            if (obj == null)
            {
                throw new ArgumentException("");
            }
            if (!obj.contents.ContainsKey("liveChatRenderer"))
            {
                //このライブ ストリームではチャットは無効です。
                return new YtInitialData(null, new List<IAction>(), null, null, "", "");
            }
            var continuation = ContinuationFactory.ParseContinuation(obj.contents.liveChatRenderer.continuations[0]);
            var actions = new List<IAction>();
            foreach (var a in obj.contents.liveChatRenderer.actions)
            {
                var action = ActionFactory.Parse(a);
                actions.Add(action);
            }
            var endpoint = ExtractSendButtonServiceEndpoint(raw);
            string? clientIdPrefix;
            if (endpoint != null)
            {
                dynamic? d = JsonConvert.DeserializeObject(endpoint)!;
                clientIdPrefix = (string)d.sendLiveChatMessageEndpoint.clientIdPrefix;
            }
            else
            {
                clientIdPrefix = null;
            }
            var allChatContinuation = (string)obj.contents.liveChatRenderer.header.liveChatHeaderRenderer.viewSelector.sortFilterSubMenuRenderer.subMenuItems[1].continuation.reloadContinuationData.continuation;
            var jouiChatContinuation = (string)obj.contents.liveChatRenderer.header.liveChatHeaderRenderer.viewSelector.sortFilterSubMenuRenderer.subMenuItems[0].continuation.reloadContinuationData.continuation;
            return new YtInitialData(continuation, actions, endpoint, clientIdPrefix,jouiChatContinuation,allChatContinuation);
        }
        public static string? ExtractSendButtonServiceEndpoint(string ytInitialData)
        {
            var arr = new[]
            {
                "contents",
                "liveChatRenderer",
                //"continuationContents",
                //"liveChatContinuation",
                "actionPanel",
                "liveChatMessageInputRenderer",
                "sendButton",
                "buttonRenderer",
                "serviceEndpoint",
            };
            var data = (Newtonsoft.Json.Linq.JObject?)JsonConvert.DeserializeObject(ytInitialData);
            if (data == null)
            {
                return null;
            }
            var temp = data[arr[0]];
            if (temp == null)
            {
                return null;
            }
            for (int i = 1; i < arr.Length; i++)
            {
                var s = arr[i];
                temp = temp[s];
                if (temp == null)
                {
                    return null;
                }
            }
            return temp.ToString();
        }
    }
}
