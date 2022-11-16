using MvvmHelpers;
using MvvmHelpers.Commands;
using ShoppingList.Models;
using ShoppingList.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingList.ViewModels
{
    [QueryProperty(nameof(ListId), nameof(ListId))]
    public class ItemsPageViewModel : ObservableObject 
    {
        public bool IsBusy { get; set; }
        public AsyncCommand RefreshCommand { get; }

        private List<Item> _boardNameList = new List<Item>();

        public List<Item> BoardNameList
        {
            get { return _boardNameList; }
            set
            {
                _boardNameList = value;
                OnPropertyChanged();
            }
        }

        public string ListId
        {
            set
            {
                LoadItems(value);
            }
        }
        
        public ItemsPageViewModel()
        {
             RefreshCommand = new AsyncCommand(Refresh);
        }


        async void LoadItems(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                List<Item> items = await App.DatabaseCon.GetItemsOfListAsync(id);
                BoardNameList = items;

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }

        async Task Refresh()
        {
            IsBusy = true;
            
            await Task.Delay(2000);


            IsBusy = false;
        }
    }
}
