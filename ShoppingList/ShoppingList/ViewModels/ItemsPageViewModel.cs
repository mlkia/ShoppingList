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
        bool isBusy;

        public bool IsBusy 
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Item> DeleteCommand { get; }


        private List<Item> _itemsList = new List<Item>();

        public List<Item> ItemsList
        {
            get { return _itemsList; }
            set
            {
                _itemsList = value;
                OnPropertyChanged();
            }
        }

        

        private string _listTitle;
        public string ListTitle
        {
            get { return _listTitle; }
            set
            {
                _listTitle = value;
                OnPropertyChanged();
            }
        }

        public string ListId
        {
            set
            {
                LoadItems(value);
                GetCurrentListId(value);
            }
        }

        //<summary>
        //CurrentListId and CurrentListId properties will get and save the current list Id.
        //</summary>

        public string CurrentListId { get; set; }

        void GetCurrentListId(string itemId)
        {
            try
            {
                CurrentListId = itemId;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load list Id.");
            }
        }

        public ItemsPageViewModel()
        {
             RefreshCommand = new AsyncCommand(Refresh);
             DeleteCommand = new AsyncCommand<Item>(Delete);
        }

        //<summary>
        //Get all items of  the list from the database and set them to the ItemsList property.
        //</summary>
        async void LoadItems(string Id)
        {
            try
            {
                int listId = Convert.ToInt32(Id);
                // Retrieve the items and set it as the BindingContext of the page.
                List<Item> items = await App.DatabaseCon.GetItemsOfListAsync(listId);
                TheList list = await App.DatabaseCon.GetListAsync(listId);
                ItemsList = items;
                ListTitle = list.Title;



            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }
        
        async Task Delete(Item item)
        {
            await App.DatabaseCon.DeleteItemAsync(item);

            await Refresh();
        }


        //<summary>
        //Refresh will get all items with current changes.
        //</summary>
        async Task Refresh()
        {
            IsBusy = true;

            LoadItems(CurrentListId);
            await Task.Delay(500);

            IsBusy = false;
        }
    }
}
