using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XebiaSampleApp.Service;

namespace XebiaSampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Locator.RegisterDependencies();
        }

        protected async override void OnStart()
        {
            await InitNavigation();
            base.OnStart();
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
