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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class ItemsViewPage : ContentPage
    {
        public int loadNoteId { get; set; }
        public string ItemId
        {
            set
            {
                /*LoadArtikel(value);
                LoadNote(value);
                GetNoteId(value);*/
            }
        }

        public ItemsViewPage()
        {
            InitializeComponent();
        }
    }
}