
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockPaperScissors.View
{
    public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage()
        {
            InitializeComponent();
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}