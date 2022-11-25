using MvvmHelpers.Commands;
using ShoppingList.Models;
using ShoppingList.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms;
using MvvmHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace ShoppingList.ViewModels
{
    [QueryProperty(nameof(Test), nameof(Test))]
    [QueryProperty(nameof(ListId), nameof(ListId))]

    public class ListViewModel : ObservableObject
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
        public AsyncCommand<TheList> DeleteListCommand { get; }
        public AsyncCommand<TheList> SaveEditListCommand { get; }
        public AsyncCommand SaveNewListCommand { get; }
        public AsyncCommand<TheList> EditListCommand { get; }
        public AsyncCommand CnacelEditListCommand { get; }

        //<summary>
        //Get and set the all lists.
        //</summary>
        private List<TheList> _allLists;
    public List<TheList> AllLists
        {
            get { return _allLists; }
            set
            {
                _allLists = value;
                OnPropertyChanged();
            }
        }


       



        //<summary>
        //Get and set the list to edit.
        //</summary>
        private TheList _currentList;
        public TheList CurrentList
        {
            get { return _currentList; }
            set
            {
                _currentList = value;
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
                GetList(value);
            }
        }

        public string Test
        {
            set
            {
                GetTest(value);
            }
        }

        public ListViewModel()
        {
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteListCommand = new AsyncCommand<TheList>(DeleteList);
            EditListCommand = new AsyncCommand<TheList>(EditList);
            SaveEditListCommand = new AsyncCommand<TheList>(SaveEditList);
            SaveNewListCommand = new AsyncCommand(SaveNewList);
            CnacelEditListCommand = new AsyncCommand(CancelEditList);
            GetAllLists();

            var NeweList = new TheList()
            {
                Title = Title,
                Date = Date
            };
        }


        string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public DateTime Date { get; set; }


        async void GetAllLists()
        {
            try
            {
                //Retrieve the items and set it as the BindingContext of the ItemsViewPage.
                List<TheList> allListe = await App.DatabaseCon.GetListsAsync();
                AllLists = allListe;
            }

            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }



        //<summary>
        //Get the current list and set it in CurrentList prperty to use it in EditListPage.xaml 
        //</summary>
        async void GetList(string Id)
        {
            try
            {
                int listId = Convert.ToInt32(Id);
                //Retrieve the items and set it as the BindingContext of the ItemsViewPage.
                TheList list = await App.DatabaseCon.GetListAsync(listId);
                CurrentList = list;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }


        async void GetTest(string text)
        {
            try
            {
               if (text != null)
                {
                    await Refresh();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load item.");
            }
        }


        async Task SaveNewList()
        {
            var newList = new TheList();
            newList.Title = Title;
            newList.Date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(newList.Title))
            {
                await App.DatabaseCon.SaveListAsync(newList);
            }

            // Navigate backwards
            //await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync($"..?{nameof(Test)}={newList.Title}");
        }
        
        

        //<summary>
        //Save the last changes in the item.
        //</summary>
        async Task SaveEditList(TheList list)
        {
            if (!string.IsNullOrWhiteSpace(list.Title))
            {
                await App.DatabaseCon.SaveListAsync(list);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync($"..?{nameof(Test)}={list.Title}");
        }


        //<summary>
        //Cancel the edit and vavigate backwards.
        //</summary>
        async Task CancelEditList()
        {
            await Shell.Current.GoToAsync("..");
        }


        //<summary>
        //Navigate to Edit List page and edit the list name.
        //</summary>
        async Task EditList(TheList list)
            
        {
            //The ItemID property takes the item id and will be used to get the item from the database
            //when navigate to EditItemPage view .
            await Shell.Current.GoToAsync($"{nameof(EditListPage)}?{nameof(ListId)}={list.Id.ToString()}");
        }

        //<summary>
        //Delete the list and all items that are in the list.
        //</summary>
        async Task DeleteList(TheList list)
        {
            await App.DatabaseCon.DeleteListAsync(list);

            await App.DatabaseCon.DeleteItemsOfListAsync(list.Id);

            await Refresh();
        }

        
        
        //<summary>
        //Refresh will get all items with current changes.
        //</summary>
        async Task Refresh()
        {
            IsBusy = true;
            //Whene use Refresh command, the LoadItems will get the current changes in the list.
            await Task.Delay(500);

            GetAllLists();
            
            IsBusy = false;
        }
    }
}
