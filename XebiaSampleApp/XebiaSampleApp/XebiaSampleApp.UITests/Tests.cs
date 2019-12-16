using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using XebiaSampleApp;
namespace XebiaSampleApp.UITests
{
    [TestFixture(Platform.Android)]
   // [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void  PersonList_AppLaunches_ShouldShowPersonPage()
        {
            AppResult[] results  = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1)));
            //AppResult[] results = app.WaitForElement(c => c.Id(TestIdentifier.PERSON_PAGE1));
            Assert.IsTrue(results.Any());

        }

        [Test]
        public void PersonList_PressingAddButton_ShouldNavigateToDetailPage()
        {
            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));
           
            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));
          
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void PersonDetail_AddPersonWithOutData_ShouldShowValidationMessage()
        {
            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));

            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE2_DONEBUTTON));

            results = app.WaitForElement(c => c.Marked(("Validation")));

            Assert.IsTrue(results.Any());

        }

        [Test]
        public void PersonDetail_AddPersonWithData_ShouldNotShowValidationMessage()
        {
            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));

            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_FIRSTNAME), "Xebia");

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_LASTNAME), "Software");

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE2_DONEBUTTON));

            results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1)));

            Assert.IsTrue(results.Any());

        }

        [Test]
        public void PersonList_AddPersonWithData_ShouldListedAddedDatainList()
        {
            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));

            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_FIRSTNAME), "Xebia");

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_LASTNAME), "Software");

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE2_DONEBUTTON));

            Assert.IsTrue(app.Query(x => x.Marked("ListView").Child()).Length > 0);


        }

        [Test]
        public void PersonList_UpdatePersonWithData_ShouldListedUpdateDatainList()
        {
            string sAddFirstName = "Trimble";
            string sUpdateFirstName = "Xebia";

            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));

            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_FIRSTNAME), sAddFirstName);

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_LASTNAME), "Software");

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE2_DONEBUTTON));

            Assert.IsTrue(app.Query(x => x.Marked("ListView").Child()).Length > 0);

            //app.Repl();

            var addFirstName =  app.Query(x => x.Id(TestIdentifier.PERSON_PAGE1_LISTVIEW).Descendant().Id(TestIdentifier.PERSON_PAGE1_FIRSTNAME)).FirstOrDefault().Text;
 
           //  var addFirstName = app.Query(x => x.Marked("ListView").Child().Descendant("label").Id(TestIdentifier.PERSON_PAGE1_FIRSTNAME)).FirstOrDefault().Text;

            Assert.IsTrue(sAddFirstName == addFirstName);

            app.Tap(c => c.Id(TestIdentifier.PERSON_PAGE1_FIRSTNAME).Index(0));

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_FIRSTNAME), sUpdateFirstName);

            var  updatedFirstName = app.Query(x => x.Id(TestIdentifier.PERSON_PAGE1_LISTVIEW).Descendant().Id(TestIdentifier.PERSON_PAGE1_FIRSTNAME)).FirstOrDefault().Text;

            Assert.IsTrue(sUpdateFirstName == updatedFirstName);
        }

        public void PersonList_DeletePersonData_ShouldNotDisplayDeletedDatainListView()
        {
            string sAddFirstName = "Trimble";
           

            AppResult[] results = app.Query(c => c.Marked((TestIdentifier.PERSON_PAGE1_ADDBUTTON)));
            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_ADDBUTTON));

            results = app.WaitForElement(c => c.Marked((TestIdentifier.PERSON_PAGE2)));

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_FIRSTNAME), sAddFirstName);

            app.EnterText(c => c.Marked(TestIdentifier.PERSON_PAGE2_LASTNAME), "Software");

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE2_DONEBUTTON));

            Assert.IsTrue(app.Query(x => x.Marked("ListView").Child()).Length > 0);

            app.TouchAndHold(app.Query(x => x.Id("ListView")).ToString());

            app.Tap(c => c.Marked(TestIdentifier.PERSON_PAGE1_DELETEBUTTON));

            Assert.IsTrue(app.Query(x => x.Marked("ListView").Child()).Length ==0);


        }
    }
}
 