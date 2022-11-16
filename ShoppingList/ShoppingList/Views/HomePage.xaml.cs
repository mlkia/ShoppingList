using System;
using ShoppingList.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingList.ViewModels;

namespace ShoppingList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // Retrieve all the Lists from the database, and set them as the
        // data source for the CollectionView.
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.DatabaseCon.GetListsAsync();
        }

        // << Navigate to the Add New List Page, without passing any data >>.
        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewListPage));
        }

        // Navigate to the Items View page, passing the Id as a query parameter.
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                TheList list = (TheList)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(ItemsViewPage)}?{nameof(ItemsViewPage.ListId)}={list.Id.ToString()}");
            }
        }
    }
}