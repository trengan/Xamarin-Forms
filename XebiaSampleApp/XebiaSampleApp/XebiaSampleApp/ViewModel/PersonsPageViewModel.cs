using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XebiaSampleApp.Model;
using XebiaSampleApp.Service;
using System.Linq;

namespace XebiaSampleApp.ViewModel
{
    public class PersonsPageViewModel:ViewModelBase
    {
        IPersonService _personService;
        INavigationService _navigationService;
        public PersonsPageViewModel(IPersonService personService, INavigationService navigationService)
        {
            _personService = personService;
            _navigationService = navigationService;
            MessagingCenter.Instance.Subscribe<PersonDetailPageViewModel,string>(this, MessageKeys.PERSON_ADDED, HandlePersonAdded);
        }

        public PersonsPageViewModel()
        {
            
        }

        public ICommand DeletePersonCommand => new Command(async (object sender) =>
        {
            var person = sender as Person;
            if (person != null)
            {
               var result =   await _personService.DeletePersonAsync(person.Id);
                if(result)
                {
                    var personToBeDeleted = Persons.FirstOrDefault(i => i.Id == person.Id);
                    if (personToBeDeleted != null)
                    {
                        Persons.Remove(personToBeDeleted);
                    }
                }
            }
        });

        public ICommand UpdatePersonCommand => new Command(async (object sender) => {
            var person = sender as Person;
            if (person != null)
            {
                await _navigationService.NavigateToAsync<PersonDetailPageViewModel>(person.Id);
            }
        });

        public ICommand AddPersonCommand => new Command(async () => {

            await _navigationService.NavigateToAsync<PersonDetailPageViewModel>();
        });

        ObservableCollection<Person> _persons = new ObservableCollection<Person>();
        public ObservableCollection<Person> Persons
        {
            get
            {
                
                return _persons;
            }
            set
            {
                _persons = value;


                _shouldShowInfo = !_persons.Any();


                OnPropertyChanged();
                OnPropertyChanged(nameof(ShouldShowInfo));
            }
        }

        bool _shouldShowInfo;
        public bool ShouldShowInfo
        {
            get
            {
                return _shouldShowInfo;
            }
            set
            {
                _shouldShowInfo = value;
            }
        }

        Person _selectedPerson;
        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                if (_selectedPerson.Id > 0)
                {
                    UpdatePersonCommand.Execute(_selectedPerson);
                }
                else
                {
                    return;
                }
                SelectedPerson = new Person();
                OnPropertyChanged();
            }
        }


        public override async Task InitializeAsync(object navigationData)
        {
            Persons = await _personService.GetPersonsAsync();

            await base.InitializeAsync(navigationData);
        }


        private async void HandlePersonAdded(PersonDetailPageViewModel sender, string error)
        {
            Persons = await _personService.GetPersonsAsync();
        }



    }
}
