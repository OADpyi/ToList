using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;


namespace ToDo.Models
{
    public class ModelChangeViewModel
    {
        
            public UserViewModel ChangeUser(Users user)
            {
                UserViewModel userModel = new UserViewModel
                {
                    UserName = user.UserName,
                    Id = user.Id

                };
                return userModel;
            }
            public List<UserViewModel> ChangeUsers(List<Users> users)
            {
                List<UserViewModel> _users = new List<UserViewModel>();
                foreach (var tempUser in users)
                {
                    _users.Add(ChangeUser(tempUser));
                }
                return _users;
            }
            public ItemViewModels ChangeItem(Items item)
            {
                ItemViewModels itemModel = new ItemViewModels
                {
                    Content = item.Content,
                    ItemId = item.ItemId,
                    UserId = item.UserId,
                    State = item.State,
                    CreateTime = item.CreateTime,
                    IsOverdue = item.IsOverdue,
                    ItemOfInventoryId = item.ItemOfInventoryId,
                    Mark = item.Mark,
                    TimeOfExpiration = item.TimeOfExpiration,

                };
                return itemModel;
            }
            public List<ItemViewModels> ChangeItem(List<Items> items)
            {
                List<ItemViewModels> _items = new List<ItemViewModels>();
                foreach (var tempItem in items)
                {
                    _items.Add(ChangeItem(tempItem));
                }
                return _items;
            }
            public InventoryViewModel ChangeInventory(Inventorys inventory)
            {
                InventoryViewModel inventoryModel = new InventoryViewModel
                {
                    CreateTime = inventory.CreateTime,
                    InventoryId = inventory.InventoryId,
                    InventoryName = inventory.InventoryName,
                    UserId = inventory.UserId,
                };
                return inventoryModel;
            }
            public List<InventoryViewModel> ChangeInventorys(List<Inventorys> inventorys)
            {
                List<InventoryViewModel> _inventorys = new List<InventoryViewModel>();
                foreach (var tempInventory in inventorys)
                {
                    _inventorys.Add(ChangeInventory(tempInventory));
                }
                return _inventorys;
            }
            public string ChangeTimeToString(DateTime time)
            {
                string timeString;
                timeString = time.Year.ToString();
                if (time.Month >= 10)
                {
                    timeString = timeString + "-" + time.Month.ToString();
                }
                else
                    timeString = timeString + "-" + '0' + time.Month.ToString();
                if (time.Day >= 10)
                {
                    timeString = timeString + "-" + time.Day.ToString();
                }
                else
                    timeString = timeString + "-" + '0' + time.Day.ToString();
                if (time.Hour >= 10)
                {
                    timeString = timeString + 'T' + time.Hour.ToString();
                }
                else
                    timeString = timeString + 'T' + '0' + time.Hour.ToString();
                if (time.Minute >= 10)
                {
                    timeString = timeString + ":" + time.Minute.ToString();
                }
                else
                    timeString = timeString + ":" + '0' + time.Minute.ToString();
                if (time.Second >= 10)
                {
                    timeString = timeString + ":" + time.Second.ToString();
                }
                else if (time.Second.ToString() == null)
                {
                    timeString = timeString + ":" + "00" + time.Second.ToString();
                }
                else
                    timeString = timeString + ":" + '0' + time.Second.ToString();

                return timeString;
            }

    }
}
