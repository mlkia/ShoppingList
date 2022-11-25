using ShoppingList.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingList.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace ShoppingList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NewListPage : ContentPage
    {

        public string ItemId
        {
            set
            {
                LoadList(value);
            }
        }

        public NewListPage()
        {
            InitializeComponent();
            // Set the BindingContext of the page to a new List.
        }

        // Retrieve the list and set it as the BindingContext of the page.
        async void LoadList(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                TheList list = await App.DatabaseCon.GetListAsync(id);
                BindingContext = list;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
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

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync("..");
        }
    }
}