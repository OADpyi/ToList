using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Data;

namespace Data.Service
{
    public class InventoryService
    {
        ToDoListContext _database = new ToDoListContext();

        /// <summary>
        /// 创建清单
        /// 一个用户不能拥有重复的清单名字
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="inventoryName"></param>
        /// <returns></returns>
        public bool CreateInventory(int userId, string inventoryName)
        {
            var userInventory = _database.Inventorys.Where(user => user.UserId == userId).ToList();
            var inventory = userInventory.SingleOrDefault(list => list.InventoryName.Equals(inventoryName));
            if (inventory == null)
            {
                var newInventory = new Inventorys()
                {
                    InventoryName = inventoryName,
                    CreateTime = DateTime.Now,
                    UserId = userId,
                };
                _database.Inventorys.Add(newInventory);
                _database.SaveChanges();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 删除清单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public void RemoveInventory(int inventoryId)
        {
            var removeInventory = _database.Inventorys.SingleOrDefault(inventory => inventory.InventoryId == inventoryId);
            var inventoryItem = _database.Items.Where(item => item.ItemOfInventoryId == inventoryId).ToList();
            foreach (var item in inventoryItem)
            {
                _database.Items.Remove(item);
            }
            _database.Inventorys.Remove(removeInventory);
            _database.SaveChanges();
        }
        /// <summary>
        /// 修改清单名称
        /// 要先判断新清单名称是否存在，如果存在则操作失败
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="newInventoryName"></param>
        /// <returns></returns>
        public bool ModifyInventoryName(int inventoryId, string newInventoryName)
        {
            var inventory = _database.Inventorys.SingleOrDefault(list => list.InventoryName.Equals(newInventoryName));
            if (inventory == null)
            {
                var modifyInventory = _database.Inventorys.SingleOrDefault(list => list.InventoryId == inventoryId);
                modifyInventory.InventoryName = newInventoryName;
                _database.SaveChanges();
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 返回所有清单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Inventorys> ViewAllInventory(int userId)
        {
            var userInventory = _database.Inventorys.Where(inventory => inventory.UserId == userId).ToList();
            return userInventory;
        }

        /// <summary>
        /// 寻找某个清单
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public Inventorys FindInventory(int inventoryId)
        {
            var findInventory = _database.Inventorys.SingleOrDefault(inventory => inventory.InventoryId == inventoryId);
            return findInventory;
        }
        /// <summary>
        /// 计算清单内的事项个数
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public int ReturnInventoryItemsCount(int inventoryId)
        {
            var ItemList = _database.Items.Where(item => item.ItemOfInventoryId == inventoryId).ToList();
            return ItemList.Count;
        }

    }


}
