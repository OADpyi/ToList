using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class ModifyInventoryViewModel
    {
        public string InventoryName { get; set; }

        public DateTime CreateTime { get; set; }

        public int InventoryId { get; set; }

        public int UserId { get; set; }

        public int InventoryItemsCount { get; set; }
    }
}
