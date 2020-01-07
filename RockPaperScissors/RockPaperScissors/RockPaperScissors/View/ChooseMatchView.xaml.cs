using RockPaperScissors.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockPaperScissors.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChooseMatchView : ContentPage
	{
		public ChooseMatchView()
		{
			InitializeComponent ();

            //this.BindingContext = new ChooseMatchViewModel();
		}
	}
}