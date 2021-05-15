using System;
using System.Collections.Generic;

namespace ToDoList.Data
{
    public partial class Users
    {
        public Users()
        {
            Inventorys = new HashSet<Inventorys>();
            Items = new HashSet<Items>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public string EMail { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Inventorys> Inventorys { get; set; }
        public virtual ICollection<Items> Items { get; set; }
    }
}
