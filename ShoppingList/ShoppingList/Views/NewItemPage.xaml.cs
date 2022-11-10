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
    [QueryProperty(nameof(ListId), nameof(ListId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public int listId { get; set; }
        public string ListId
        {
            set
            {
                GetListId(value);
            }
        }

        public NewItemPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Item.
            BindingContext = new Item();
        }

        void GetListId(string Id)
        {
            try
            {
                listId = Convert.ToInt32(Id);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Item.");
            }
        }

        private void OnEntryTextChanged(object sender, EventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                btnSave.IsEnabled = true;
            }
            else { btnSave.IsEnabled = false; }
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            item.ListId = listId;
            if (!string.IsNullOrWhiteSpace(item.Text))
            {
                await App.DatabaseCon.SaveItemlAsync(item);
            }
            
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            await App.DatabaseCon.DeleteItemAsync(item);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}