using NUnit.Framework;
using Tests.Common.TranslationTessInner;

namespace WebSsh.Tests
{
    public class TestTranslations : TestTranslationBase
    {
        public TestTranslations()
            : base("WebSsh.ResourceManager", "Source")
        {

        }

        [Test]
        public void Test()
        {
            RunTest();
        }
    }
}
