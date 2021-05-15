using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;

namespace ToDo.Models
{
    public class ItemsListViewModel
    {
        public List<ItemViewModels> noFinished { get; set; }
        public List<ItemViewModels> hasFinished { get; set; }
        public List<ItemViewModels> hasOverdue { get; set; }
        public List<InventoryViewModel> allInventorys { get; set; }
        public int InventoryId { get; set; }
    }
}
