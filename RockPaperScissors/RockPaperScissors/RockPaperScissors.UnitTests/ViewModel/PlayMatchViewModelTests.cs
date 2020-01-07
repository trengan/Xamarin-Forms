
using Autofac;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;
using RockPaperScissors.Service;
using RockPaperScissors.ViewModel;
using Xamarin.Forms;

namespace RockPaperScissors.UnitTests
{
    public class PlayMatchViewModelTests
    {
        private AutoFake _fake;
        private ILifetimeScope _scope;
        bool _iSAppLandedOn;
        public PlayMatchViewModelTests()
        {
            Container.RegisterDependencies();
            _fake = new AutoFake();
            _scope = Container.ForTest_SetContainer(_fake.Container);

            MessagingCenter.Subscribe<PlayMatchViewModel>(this, MessageKeys.PLAYMATCHLANDEDKEY, (sender) =>
            {
                _iSAppLandedOn = true;
            });
        }

        [Test]
        public void AppLanded_HumanVSComputerMode_ShouldNavigatedToPlayMatchViewModel()
        {
            var playMatchViewModel = A.Fake<PlayMatchViewModel>();

            A.CallTo(() => playMatchViewModel.InitializeAsync(MatchType.HumanVSComputer)).Invokes(() => MessagingCenter.Send(playMatchViewModel, MessageKeys.PLAYMATCHLANDEDKEY));
            playMatchViewModel.InitializeAsync(MatchType.HumanVSComputer);
            Assert.IsTrue(_iSAppLandedOn);
        }
        [Test]
        public void ExecuteSelectChoiceCommand_HumanVSComputerMode_ShouldAssignedForPlayer1()
        {
            var playMatchViewModel = A.Fake<PlayMatchViewModel>();
            playMatchViewModel.Game.Play(MatchType.HumanVSComputer);

            playMatchViewModel.SelectChoiceCommand.Execute((int)Choice.Rock);

            Assert.IsTrue(playMatchViewModel.Game.Match.GetChoiceFromPalyer1() == Choice.Rock);
        }

        [Test]
        public void VerifyResult_HumanVSComputerMode_ShouldDraw()
        {
            var playMatchViewModel = A.Fake<PlayMatchViewModel>();
            playMatchViewModel.Game.Play(MatchType.HumanVSComputer);
            playMatchViewModel.Game.Match.SetChoiceForPlayer1(Choice.Rock);
            playMatchViewModel.Game.Match.SetChoiceForPlayer2(Choice.Rock);
            playMatchViewModel.Game.GetResult();
            Assert.IsTrue(playMatchViewModel.Game.Match.IsTie);
        }

        [Test]
        public void VerifyResult_HumanVSComputerMode_ShouldPlayer1Win()
        {
            var playMatchViewModel = A.Fake<PlayMatchViewModel>();
            playMatchViewModel.Game.Play(MatchType.HumanVSComputer);
            playMatchViewModel.Game.Match.SetChoiceForPlayer1(Choice.Rock);
            playMatchViewModel.Game.Match.SetChoiceForPlayer2(Choice.Scissors);
            playMatchViewModel.Game.GetResult();
            Assert.IsTrue(playMatchViewModel.Game.Match.Winner.Name =="You");
        }
        [Test]
        public void VerifyResult_HumanVSComputerMode_ShouldPlayer2Win()
        {
            var playMatchViewModel = A.Fake<PlayMatchViewModel>();
            playMatchViewModel.Game.Play(MatchType.HumanVSComputer);
            playMatchViewModel.Game.Match.SetChoiceForPlayer1(Choice.Paper);
            playMatchViewModel.Game.Match.SetChoiceForPlayer2(Choice.Scissors);
            playMatchViewModel.Game.GetResult();
            Assert.IsTrue(playMatchViewModel.Game.Match.Winner.Name != "You");
        }
    }
}
