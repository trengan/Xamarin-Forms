using RockPaperScissors.Service;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RockPaperScissors.ViewModel;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RockPaperScissors
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new ChooseMatchView();
        }

        protected async override void OnStart()
        {
            Container.RegisterDependencies();
            await InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Task InitNavigation()
        {
            return new NavigationService().InitializeAsync();
        }
    }
}
