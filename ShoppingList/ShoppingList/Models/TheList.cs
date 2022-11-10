using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Models
{
    public class TheList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Item> Item { get; set; }
    }
}
