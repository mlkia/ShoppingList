using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }

        public string Text { get; set; }
        public bool Done { get; set; }

        [ForeignKey(typeof(TheList))]
        public int ListId { get; set; }

        [ManyToOne]
        public TheList TheList { get; set; }
    }
}
