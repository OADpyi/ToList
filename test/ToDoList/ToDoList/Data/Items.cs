using System;
using System.Collections.Generic;

namespace ToDoList.Data
{
    public partial class Items
    {
        public int ItemId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime TimeOfExpiration { get; set; }
        public string Content { get; set; }
        public string Mark { get; set; }
        public int UserId { get; set; }
        public bool State { get; set; }
        public int ItemOfInventoryId { get; set; }
        public bool IsOverdue { get; set; }

        public virtual Inventorys ItemOfInventory { get; set; }
        public virtual Users User { get; set; }
    }
}
