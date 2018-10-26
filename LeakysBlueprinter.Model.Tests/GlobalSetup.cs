using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace LeakysBlueprinter.Model.Tests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            // Setting culture to en-US is critical, ensuring that expected decimal point is dot, not comma
            SetCultureForDefaultAndCurrentThread("en-US");
        }

        protected void SetCultureForDefaultAndCurrentThread(string culture)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(culture);

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
