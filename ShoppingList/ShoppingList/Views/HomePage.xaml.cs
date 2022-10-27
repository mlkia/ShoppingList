﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NewListPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(NewListPage));
        }
    }
}