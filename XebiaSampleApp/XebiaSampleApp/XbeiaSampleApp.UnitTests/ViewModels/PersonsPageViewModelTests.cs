using System;
using System.Collections.Generic;
using System.Text;
using XebiaSampleApp;
using XbeiaSampleApp.UnitTests.Service;
using NUnit;
using NUnit.Framework;
using XebiaSampleApp.ViewModel;
using XebiaSampleApp.Model;

namespace XbeiaSampleApp.UnitTests.ViewModels
{
    public class PersonsPageViewModelTests
    {
        public PersonsPageViewModelTests()
        {
            // Register Dependencies
            XebiaSampleApp.Locator.RegisterDependencies();
        }


        public async void  Test_InitializeAsync()
        {

            var personsPageViewModel = new PersonsPageViewModel(new MockPersonService(),new MockNavigationService());
            await personsPageViewModel.InitializeAsync(null);
            Assert.IsTrue(personsPageViewModel.Persons != null);
        }

        public void Test_DeletePersonCommand()
        {
            var personsPageViewModel = new PersonsPageViewModel();
            int beforeDeleteCommand = personsPageViewModel.Persons.Count;
            personsPageViewModel.DeletePersonCommand.Execute(new Person() { Id = 1 });
            int afterDeleteCommand = personsPageViewModel.Persons.Count;
            Assert.IsTrue(beforeDeleteCommand - 1 == afterDeleteCommand);
        }
    }
}
