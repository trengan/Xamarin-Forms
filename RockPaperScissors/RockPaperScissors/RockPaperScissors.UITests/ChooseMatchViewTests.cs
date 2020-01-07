using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using RockPaperScissors;

namespace RockPaperScissors.UITests
{
    [TestFixture(Platform.Android)]
   // [TestFixture(Platform.iOS)]
    public class ChooseMatchViewTests
    {
        IApp app;
        Platform platform;

        public ChooseMatchViewTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches_ShouldNavigatedToChooseMatchView()
        {
            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.CHOOSEMATCHVIEW)));
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TapHumanVsComputerButton_ShouldNavigatedToPlayMathView()
        {
            app.Tap(c => c.Marked(TestIdentifier.HUMANVSCOMPUTER));
           
            AppResult[] results = app.WaitForElement(c => c.Marked(TestIdentifier.PLAYMATCHVIEW), timeout: TimeSpan.FromSeconds(90));
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TapComputerVsComputerButton_ShouldNavigatedToPlayMathView()
        {
            app.Tap(c => c.Marked(TestIdentifier.COMPUTERVSCOMPUTER));
           
            AppResult[] results = app.WaitForElement(c => c.Marked(TestIdentifier.PLAYMATCHVIEW), timeout: TimeSpan.FromSeconds(90));
            Assert.IsTrue(results.Any());
        }
    }
}

