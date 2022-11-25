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

namespace ShoppingList.ViewModels
{
    [QueryProperty(nameof(ListId), nameof(ListId))]
    [QueryProperty(nameof(ItemID), nameof(ItemID))]
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
        public AsyncCommand<Item> DeleteItemCommand { get; }
        public AsyncCommand<Item> SaveEditItemCommand { get; }
        public AsyncCommand<Item> EditItemCommand { get; }
        public AsyncCommand CnacelEditItemCommand { get; }


        //<summary>
        //Get and set the items of a list.
        //</summary>
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


        //<summary>
        //Get and set the item to edit.
        //</summary>
        private Item _theItem;
        public Item TheItem
        {
            get { return _theItem; }
            set
            {
                _theItem = value;
                OnPropertyChanged();
            }
        }


        //<summary>
        //Get and set the title of the list to show it in the ItemsViewPage.
        //</summary>
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


        //<summary>
        //Set List Id to GetItems and GetCurrentListId.
        //</summary>
        public string ListId
        {
            set
            {
                GetItems(value);
                GetCurrentListId(value);
            }
        }


        //<summary>
        //Set item Id to GetItem.
        //</summary>
        public string ItemID
        {
            set
            {
                GetItem(value);
            }
        }


        //<summary>
        //CurrentListId and CurrentListId properties will get and set the current list Id and use it in Refresh.
        //</summary>
        private string CurrentListId { get; set; }
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
             DeleteItemCommand = new AsyncCommand<Item>(DeleteItem);
             EditItemCommand = new AsyncCommand<Item>(EditItem);
             SaveEditItemCommand = new AsyncCommand<Item>(SaveEditItem);
             CnacelEditItemCommand = new AsyncCommand(CanceEditlItem);
            
        }

        //<summary>
        //Get all items of  the list from the database and set them to the ItemsList property.
        //</summary>
        async void GetItems(string Id)
        {
            try
            {
                int listId = Convert.ToInt32(Id);
                // Retrieve the items and set it as the BindingContext of the ItemsViewPage.
                List<Item> items = await App.DatabaseCon.GetItemsOfListAsync(listId);
                TheList list = await App.DatabaseCon.GetListAsync(listId);
                ItemsList = items;
                //ListTitle 👇 will get the title of list to show this as a title to The ItemsViewPage. 
                ListTitle = list.Title;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }


        //<summary>
        //Get the item as object and set it to theItem property.
        //</summary>
        async void GetItem(string id)
        {
            try
            {
                int itemId = Convert.ToInt32(id);
                // Retrieve the item and set it as the BindingContext of the EditItemPage.
                Item items = await App.DatabaseCon.GetItemAsync(itemId);
                TheItem = items;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }

        //<summary>
        //Delete the item.
        //</summary>
        async Task DeleteItem(Item item)
        {
            await App.DatabaseCon.DeleteItemAsync(item);

            await Refresh();
        }

        //<summary>
        //Save the last changes in the item.
        //</summary>
        async Task SaveEditItem(Item item)
        {
            if (!string.IsNullOrWhiteSpace(item.Text))
            {
                await App.DatabaseCon.SaveItemlAsync(item);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        

        //<summary>
        //Cancel the edit and vavigate backwards.
        //</summary>
        async Task CanceEditlItem()
        {
            await Shell.Current.GoToAsync("..");
        }

        
        //<summary>
        //Navigate to Edit Item page and edit the item.
        //</summary>
        async Task EditItem(Item item)
        {
            //The ItemID property takes the item id and will be used to get the item from the database
            //when navigate to EditItemPage view .
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}?{nameof(ItemID)}={item.ItemId.ToString()}");
        }


        //<summary>
        //Refresh will get all items with current changes.
        //</summary>
        async Task Refresh()
        {
            IsBusy = true;

            //Whene use Refresh command, the LoadItems will get the current changes in the list.
            GetItems(CurrentListId);
            await Task.Delay(500);

            IsBusy = false;
        }
    }
}
