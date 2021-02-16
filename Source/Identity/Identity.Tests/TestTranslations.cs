using NUnit.Framework;
using Tests.Common.TranslationTessInner;

namespace Identity.Tests
{
    public class TestTranslations : TestTranslationBase
    {
        public TestTranslations()
            : base("Identity.ResourceManager", "Identity")
        {

        }

        [Test]
        public void Test()
        {
            RunTest();
        }
    }
}
