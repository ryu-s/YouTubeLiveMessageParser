using ryu_s.YouTubeLive.Message.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ryu_s.YouTubeLive.Message.Continuation;

namespace ryu_s.YouTubeLive.Message
{
    public class GetLiveChat
    {
        public IContinuation? Continuation { get; }
        public List<IAction> Actions { get; }
        public GetLiveChat(IContinuation? continuation, List<IAction> actions)
        {
            Continuation = continuation;
            Actions = actions;
        }
        private static GetLiveChat Parse(dynamic d)
        {
            IContinuation? continuation;
            if (d.ContainsKey("continuationContents"))
            {
                continuation = ContinuationFactory.ParseContinuation(d.continuationContents.liveChatContinuation.continuations[0]);
            }
            else
            {
                continuation = null;
            }

            var actions = new List<IAction>();
            if (d.ContainsKey("continuationContents"))
            {
                if (d.continuationContents.liveChatContinuation.ContainsKey("actions"))
                {
                    foreach (var a in d.continuationContents.liveChatContinuation.actions)
                    {
                        var action = ActionFactory.Parse(a);
                        actions.Add(action);
                    }
                }
            }

            return new GetLiveChat(continuation, actions);
        }
        public static GetLiveChat Parse(string raw)
        {
            dynamic? d = JsonConvert.DeserializeObject(raw);
            if (d == null)
            {
                throw new ArgumentException();
            }
            try
            {
                return Parse(d);
            }
            catch (Exception ex)
            {
                throw new GetLiveChatParseException(ex, raw);
            }
        }
    }
}
