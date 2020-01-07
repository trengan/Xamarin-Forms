using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.ViewModel
{
    public class ViewModelBase : BindableObject
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
