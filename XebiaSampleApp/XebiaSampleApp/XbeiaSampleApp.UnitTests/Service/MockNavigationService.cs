using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XebiaSampleApp.Service;
using XebiaSampleApp.ViewModel;

namespace XbeiaSampleApp.UnitTests.Service
{
    public class MockNavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            return Task.FromResult(false);
        }

        public Task NavigateBackAsync()
        {
            return Task.FromResult(false);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return Task.FromResult(false);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return Task.FromResult(false);
        }

        public Task RemoveBackStackAsync()
        {
            return Task.FromResult(false);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            return Task.FromResult(false);
        }
    }
}
