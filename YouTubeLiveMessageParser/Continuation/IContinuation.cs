using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ryu_s.YouTubeLive.Message.Continuation
{
    public interface IContinuation { }

    public class ContinuationFactory
    {
        public static IContinuation ParseContinuation(string json)
        {
            dynamic? obj = JsonConvert.DeserializeObject(json);
            if (obj == null) throw new ArgumentException();
            return ParseContinuation(obj);
        }
        public static IContinuation ParseContinuation(dynamic json)
        {
            if (json.ContainsKey("timedContinuationData"))
            {
                var timed = json.timedContinuationData;
                var timeoutMs = (int)timed.timeoutMs;
                var continuation = (string)timed.continuation;
                return new TimedContinuationData(timeoutMs, continuation);
            }
            else if (json.ContainsKey("invalidationContinuationData"))
            {
                var invalid = json.invalidationContinuationData;
                var timeoutMs = (int)invalid.timeoutMs;
                var continuation = (string)invalid.continuation;
                return new InvalidationContinuationData(timeoutMs, continuation);
            }
            else if (json.ContainsKey("reloadContinuationData"))
            {
                var reload = json.reloadContinuationData;
                var continuation = (string)reload.continuation;
                return new ReloadContinuationData(continuation);
            }
            else
            {
                return new UnknownContinuationData((string)json.ToString(Formatting.None));
            }
            throw new NotImplementedException();
        }
    }
}
