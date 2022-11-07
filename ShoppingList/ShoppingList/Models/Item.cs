using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Models
{
    internal class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemlId { get; set; }

        public string Text { get; set; }
        public bool Done { get; set; }

        [ForeignKey(typeof(List))]
        public int NoteId { get; set; }

        [ManyToOne]
        public List List { get; set; }
    }
}
