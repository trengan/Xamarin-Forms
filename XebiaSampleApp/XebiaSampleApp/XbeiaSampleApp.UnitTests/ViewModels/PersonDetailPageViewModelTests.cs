using System;
using System.Collections.Generic;
using System.Text;
using XbeiaSampleApp.UnitTests.Service;
using XebiaSampleApp.ViewModel;
using XebiaSampleApp.Model;
using NUnit.Framework;

namespace XbeiaSampleApp.UnitTests.ViewModels
{
    public class PersonDetailPageViewModelTests
    {
        public PersonDetailPageViewModelTests()
        {
            XebiaSampleApp.Locator.RegisterDependencies();
        }

        public async void Test_InitializeAsync()
        {
            var personsDetailPageViewModel = new PersonDetailPageViewModel(new MockPersonService(), new MockNavigationService());
            await personsDetailPageViewModel.InitializeAsync(new Person() { Id = 2 });
            Assert.IsTrue(personsDetailPageViewModel.PersonDetail != null);
        }
    }
}
