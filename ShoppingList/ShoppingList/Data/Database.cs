using ShoppingList.Models;
using SQLite;
using System;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;
        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TheList>().Wait();
            database.CreateTableAsync<Item>().Wait();
        }

        //<< -------------------The List------------------- >>
        
        //<< Get all Lists >>.
        public Task<List<TheList>> GetNotesAsync()
        {
            return database.Table<TheList>().ToListAsync();
        }

        //<< Get a specific List >>.
        public Task<TheList> GetListAsync(int id)
        {
            return database.Table<TheList>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        //<< Save and update List >>.
        public Task<int> SaveListAsync(TheList list)
        {
            if (list.Id != 0)
            {
                // Update an existing list.
                return database.UpdateAsync(list);
            }
            else
            {
                // Save a new list.
                return database.InsertAsync(list);
            }
        }

        //<< Delete List >>.
        public Task<int> DeleteListAsync(TheList list)
        {
            return database.DeleteAsync(list);
        }

        //<< -------------------Item------------------- >>

        //<< Get all Items >>.
        public Task<List<Item>> GetItemAsync()
        {
            return database.Table<Item>().ToListAsync();
        }
        
        
        // << Get a specific Item >>.
        public Task<Item> GetItemAsync(int id)
        {
            return database.Table<Item>()
                            .Where(i => i.ItemlId == id)
                            .FirstOrDefaultAsync();
        }

        // << Get Items for a specific List >> **************.
        public Task<List<Item>> GetItemsOfListAsync(int id)
        {
            return database.Table<Item>()
                            .Where(i => i.ListId == id).ToListAsync();
        }

        
        //<< Save and update Item >>.
        public Task<int> SaveItemlAsync(Item item)
        {
            if (item.ItemlId != 0)
            {
                // Update an existing Item.
                return database.UpdateAsync(item);
            }
            else
            {
                // Save a new Item.
                return database.InsertAsync(item);
            }
        }

        // << Delete Item >>.
        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }
    }
}
