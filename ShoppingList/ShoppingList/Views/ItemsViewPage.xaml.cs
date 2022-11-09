using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ListId), nameof(ListId))]
    public partial class ItemsViewPage : ContentPage
    {
        public int LoadListId { get; set; }
        public string ListId
        {
            set
            {
                LoadItems(value);
                LoadList(value);
                GetListId(value);
            }
        }

        public ItemsViewPage()
        {
            InitializeComponent();
        }

        void GetListId(string itemId)
        {

            try
            {
                LoadListId = Convert.ToInt32(itemId);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load list Id.");
            }
        }

        async void LoadItems(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                List<Item> items = await App.DatabaseCon.GetItemsOfListAsync(id);
                collectionView.ItemsSource = items;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }

        async void LoadList(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                TheList list = await App.DatabaseCon.GetListAsync(id);
                BindingContext = list;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemPage.ListId)}={LoadListId.ToString()}");
        }
    }
}