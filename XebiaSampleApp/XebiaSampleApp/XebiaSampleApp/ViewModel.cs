using System;

using Xamarin.Forms;

namespace XebiaSampleApp
{
    public class ViewModel : ContentPage
    {
        public ViewModel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

