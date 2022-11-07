using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Models
{
    internal class List
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Item> Item { get; set; }
    }
}
