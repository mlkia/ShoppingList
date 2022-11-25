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

        [ForeignKey(typeof(TheList))]
        public int FK_ListId { get; set; } = 0;
        public string Text { get; set; }
        public bool Done { get; set; }

        public int ListId { get; set; }

        [ManyToOne("FK_ListId", "Id")]
        public TheList TheList { get; set; }
    }
}
