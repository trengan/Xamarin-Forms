using RockPaperScissors.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockPaperScissors.ViewModel
{
    public class ChooseMatchViewModel : ViewModelBase
    {

        INavigationService _navigationService;
        public ChooseMatchViewModel(INavigationService navigationService )
        {
            _navigationService = navigationService;

        }

        public override Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Send(this, MessageKeys.CHOOSEMATCHLANDEDKEY);

            return base.InitializeAsync(navigationData);
        }

        public ICommand HumanVSComputerCommand => new Command(() =>
        {
            _navigationService.NavigateToAsync<PlayMatchViewModel>(MatchType.HumanVSComputer);
        });

        public ICommand ComputerVSComputerCommand => new Command(() =>
        {
            _navigationService.NavigateToAsync<PlayMatchViewModel>(MatchType.ComputerVSComputer);

        });

    }
}