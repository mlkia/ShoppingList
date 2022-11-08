using ShoppingList.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views
{
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
            BindingContext = new TheList();
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

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var list = (TheList)BindingContext;
            list.Date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(list.Title))
            {
                await App.DatabaseCon.SaveListAsync(list);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var list = (TheList)BindingContext;
            await App.DatabaseCon.DeleteListAsync(list);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}