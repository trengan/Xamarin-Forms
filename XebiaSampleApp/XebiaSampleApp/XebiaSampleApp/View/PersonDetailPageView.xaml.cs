using System;
using System.Collections.Generic;
using XebiaSampleApp.Resx;
using Xamarin.Forms;
using XebiaSampleApp.ViewModel;


namespace XebiaSampleApp.View
{
    public partial class PersonDetailPageView : ContentPage
    {
        public PersonDetailPageView()
        {
            InitializeComponent();

            MessagingCenter.Instance.Subscribe<PersonDetailPageViewModel, string>(this, MessageKeys.VALIDATION, HandleValidation);
        }

        private void HandleValidation(PersonDetailPageViewModel personDetailPageViewModel, string error)
        {
            DisplayAlert(AppResource.ValidationTitle, AppResource.ValidationMessage, AppResource.Ok);
        }
    }
}
