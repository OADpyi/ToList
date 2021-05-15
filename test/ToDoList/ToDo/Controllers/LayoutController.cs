using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Service;
using ToDo.Controllers;

namespace ToDoList.Controllers
{
    public class LayoutController : Controller
    {
        InventoryService _inventoryService = new InventoryService();
        ItemService _itemService = new ItemService();
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return UserController._userView.UserName;
        }
        /// <summary>
        /// 获取侧栏事项数
        /// </summary>
        /// <returns></returns>
        public int[] GetCount()
        {
            var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
            var finishItemList = _itemService.GetAllFinishIteams(UserController._userView.Id);
            var overdueItemList = _itemService.GetAllOverdueIteams(UserController._userView.Id);
            var notFinishItemList = _itemService.GetAllNotFinishIteams(UserController._userView.Id);
            int[] count = { inventoryList.Count, finishItemList.Count, overdueItemList.Count, notFinishItemList.Count };
            return count;
        }
    }
}
