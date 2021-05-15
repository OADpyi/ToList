using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class ItemViewModels
    {
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public int ItemOfInventoryId { get; set; }
        public bool State { get; set; }
        public bool IsOverdue { get; set; }
        public string Mark { get; set; }
        public DateTime TimeOfExpiration { get; set; }
        public string TimeToString { get; set; }
        public bool IsReturnSearch { get; set; }
        public string ChangeBeforeContent { get; set; }
        public bool IsReturnHomePage { get; set; }
    }
}
