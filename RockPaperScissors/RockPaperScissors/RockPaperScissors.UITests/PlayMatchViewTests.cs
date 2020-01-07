using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RockPaperScissors.UITests
{
    [TestFixture(Platform.Android)]
    public class PlayMatchViewTests
    {
        IApp app;
        Platform platform;

        public PlayMatchViewTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TapChangeMode_HumanVsComputerMode_ShouldNavigateBackToChooseMatchView()
        {
            app.Tap(c => c.Marked(TestIdentifier.HUMANVSCOMPUTER));
            app.Tap(c => c.Marked(TestIdentifier.SELECTCHOICE));
            app.Tap(c => c.Marked(TestIdentifier.CHANGEMODE));
            AppResult[] results = app.WaitForElement(c => c.Marked(TestIdentifier.CHOOSEMATCHVIEW), timeout: TimeSpan.FromSeconds(90));
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TapChangeMode_ComputerVsComputerMode_ShouldNavigateBackToChooseMatchView()
        {
            app.Tap(c => c.Marked(TestIdentifier.COMPUTERVSCOMPUTER));
            app.WaitForElement(x => x.Marked(TestIdentifier.CHANGEMODE));
            app.Tap(c => c.Marked(TestIdentifier.CHANGEMODE));
            AppResult[] results = app.WaitForElement(c => c.Marked(TestIdentifier.CHOOSEMATCHVIEW), timeout: TimeSpan.FromSeconds(90));
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void VerifyResult_HumanVsComputerMode_ShouldShowResultLabel()
        {
            app.Tap(c => c.Marked(TestIdentifier.HUMANVSCOMPUTER));
            app.Tap(c => c.Marked(TestIdentifier.SELECTCHOICE));
            Assert.IsNotEmpty(app.Query(c => c.Marked(TestIdentifier.RESULT)).FirstOrDefault().Text);
        }
        [Test]
        public void VerifyResult_ComputerVsComputerMode_ShouldShowResultLabel()
        {
            app.Tap(c => c.Marked(TestIdentifier.COMPUTERVSCOMPUTER));
            Assert.IsNotEmpty(app.WaitForElement(c => c.Marked(TestIdentifier.RESULT), timeout: TimeSpan.FromSeconds(90)).FirstOrDefault().Text);
        }
    }
}

