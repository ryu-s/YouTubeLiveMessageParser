using NUnit.Framework;
using ryu_s.YouTubeLive.Message.Continuation;

namespace YouTubeLiveSitePluginTests
{
    public class ContinuationTests
    {
        [Test]
        public void ParseTimedContinuationTest()
        {
            var s = @"{""timedContinuationData"":{""timeoutMs"":5195,""continuation"":""abc""}}";
            var c = ContinuationFactory.ParseContinuation(s);
            if (c is not TimedContinuationData timed)
            {
                Assert.Fail();
                return;
            }
            Assert.That(timed.TimeoutMs, Is.EqualTo(5195));
            Assert.That(timed.Continaution, Is.EqualTo("abc"));
        }
        [Test]
        public void ParseReloadContinuationTest()
        {
            var s = @"{""reloadContinuationData"":{""continuation"":""abc""}}";
            var c = ContinuationFactory.ParseContinuation(s);
            if (c is not ReloadContinuationData reload)
            {
                Assert.Fail();
                return;
            }
            Assert.That(reload.Continaution, Is.EqualTo("abc"));
        }
    }
}