
using ShoppingList.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace ShoppingList
{
    public partial class App : Application
    {
        static Database database;

        // << Create the database connection as a singleton >>.
        public static Database DatabaseCon
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
