using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XebiaSampleApp.View;
using XebiaSampleApp.ViewModel;

namespace XebiaSampleApp.Service
{
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel => throw new NotImplementedException();

        ViewModelBase INavigationService.PreviousPageViewModel => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            return NavigateToAsync<PersonsPageViewModel>();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }   

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (Application.Current.MainPage != null && Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PopAsync();
            }
        }



        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
                
            if (page is PersonsPageView)
            {
                Application.Current.MainPage = new CustomNavigationPage(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationPage;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
            }

        
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

       

       
    }
}
