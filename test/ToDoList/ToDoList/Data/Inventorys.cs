using System;
using System.Collections.Generic;

namespace ToDoList.Data
{
    public partial class Inventorys
    {
        public Inventorys()
        {
            Items = new HashSet<Items>();
        }

        public int InventoryId { get; set; }
        public string InventoryName { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Items> Items { get; set; }
        public virtual Users User { get; set; }
    }
}
