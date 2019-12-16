using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XebiaSampleApp.Model;
using XebiaSampleApp.Service;

namespace XebiaSampleApp.ViewModel
{
    public class PersonDetailPageViewModel:ViewModelBase
    {
        IPersonService _personService;
        INavigationService _navigationService;
        public PersonDetailPageViewModel(IPersonService personService, INavigationService navigationService)
        {
            _personService = personService;
            _navigationService = navigationService;
        }

    
        PersonDetail _personDetail;
        public PersonDetail PersonDetail
        {
            get
            {

                return _personDetail;
            }
            set
            {
                _personDetail = value;
                OnPropertyChanged();
            }
        }


        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null) // For edit
            {
                int persondId = (int)navigationData;
                PersonDetail = await _personService.GetPersonDetailsAsync(persondId) as PersonDetail;
                await base.InitializeAsync(navigationData);
            }
            else // For save 
            {
                PersonDetail = new PersonDetail();
            }
        }


        public ICommand UpdatePersonCommand => new Command(async () => {

            if (!string.IsNullOrEmpty(PersonDetail.FirstName))
            {
                PersonDetail = await _personService.SavePersonAsync(PersonDetail) as PersonDetail;
                // Should pass the expection message through the last parameter of below method
                MessagingCenter.Instance.Send(this, MessageKeys.PERSON_ADDED, string.Empty);
                await _navigationService.NavigateBackAsync();
            }
            else
            {
                MessagingCenter.Instance.Send(this, MessageKeys.VALIDATION, string.Empty);
            }
        });
    }
}
