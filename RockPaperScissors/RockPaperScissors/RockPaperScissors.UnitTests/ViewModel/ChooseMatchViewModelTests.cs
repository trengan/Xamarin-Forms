using Autofac;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;
using RockPaperScissors.Service;
using RockPaperScissors.ViewModel;
using Xamarin.Forms;

namespace RockPaperScissors.UnitTests
{

    public class ChooseMatchViewModelTests
    {
        bool _iSAppLandedOn;
        bool _isNavigatedToPlayMatch;

        private AutoFake _fake;
        private ILifetimeScope _scope;
        public ChooseMatchViewModelTests()
        {
            Container.RegisterDependencies();

            _fake = new AutoFake();
            _scope = Container.ForTest_SetContainer(_fake.Container);

            MessagingCenter.Subscribe<ChooseMatchViewModel>(this, MessageKeys.CHOOSEMATCHLANDEDKEY, (sender) =>
            {
                 _iSAppLandedOn = true;

            });

            MessagingCenter.Subscribe<PlayMatchViewModel>(this, MessageKeys.PLAYMATCHLANDEDKEY, (sender) =>
            {
                _isNavigatedToPlayMatch = true;

            });

        }

        [Test]
        public void AppLaunched_ShouldBeOnChooseMatchViewModel()
        {
            var chooseMatchViewModel = _scope.Resolve<ChooseMatchViewModel>();

            chooseMatchViewModel.InitializeAsync(null);

            Assert.IsTrue(_iSAppLandedOn);
        }

            [Test]
        public void ExecuteHumanVSComputerCommand_ShouldNavigateToPlayMatchViewModel()
        {
            var mockNavigationService = _scope.Resolve<INavigationService>();
            var chooseMatchViewModel = _scope.Resolve<ChooseMatchViewModel>();
            var playMatchViewModel = _scope.Resolve<PlayMatchViewModel>();
            A.CallTo(() => mockNavigationService.NavigateToAsync<PlayMatchViewModel>(MatchType.HumanVSComputer)).
                Invokes(() => MessagingCenter.Send(playMatchViewModel, MessageKeys.PLAYMATCHLANDEDKEY));

            chooseMatchViewModel.HumanVSComputerCommand.Execute(null);

            Assert.IsTrue(_isNavigatedToPlayMatch);
        }
         
        [Test]
        public void ExecuteComputerVSComputerCommand_ShouldNavigateToPlayMatchViewModel()
        {
            var mockNavigationService = _scope.Resolve<INavigationService>();
            var chooseMatchViewModel = _scope.Resolve<ChooseMatchViewModel>();
            var playMatchViewModel = _scope.Resolve<PlayMatchViewModel>();
            A.CallTo(() => mockNavigationService.NavigateToAsync<PlayMatchViewModel>(MatchType.ComputerVSComputer)).Invokes(() => MessagingCenter.Send(playMatchViewModel, MessageKeys.PLAYMATCHLANDEDKEY));

            chooseMatchViewModel.ComputerVSComputerCommand.Execute(null);

            Assert.IsTrue(_isNavigatedToPlayMatch);
        }

    }
}
