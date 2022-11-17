using ShoppingList.Data;
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
    public partial class ItemsViewPage : ContentPage
    {
        public int LoadListId { get; set; }
        
        public string ListId
        {
            set
            {
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

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemPage.ListId)}={LoadListId.ToString()}");
        }
        
        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var item = ((CheckBox)sender).BindingContext as Item;

            if (item == null)
                return;
            
            item.Done = e.Value;
            App.DatabaseCon.SaveItemlAsync(item);
        }
    }
}