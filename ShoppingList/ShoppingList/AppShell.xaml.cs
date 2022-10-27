using ShoppingList.Views;
using Xamarin.Forms;

namespace ShoppingList
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewListPage), typeof(NewListPage));

        }
        
    }
}
