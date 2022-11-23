using ShoppingList.ViewModels;
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
            Routing.RegisterRoute(nameof(ItemsViewPage), typeof(ItemsViewPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));


        }
        
    }
}
