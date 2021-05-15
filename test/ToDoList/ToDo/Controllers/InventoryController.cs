using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Service;
using ToDo.Models;
using ToDoList.Data;

namespace ToDo.Controllers
{
    public class InventoryController : Controller
    {
        InventoryService _inventoryService = new InventoryService();
        private static ModifyInventoryViewModel _inventory = new ModifyInventoryViewModel();
        /// <summary>
        /// 查找清单操作
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInventory()
        {
            var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
            var viewInventory = SaveInViewModels(inventoryList);
            return View("GetInventory", viewInventory);
        }
        /// <summary>
        /// 清单页面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult ViewInventory()
        {
            return View("ViewInventory");
        }

        /// <summary>
        /// 点击创建清单的页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewCreateInventory()
        {
            var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
            var viewInventory = SaveInViewModels(inventoryList);
            return View("ViewCreateInventory", viewInventory);
        }


        /// <summary>
        /// 创建清单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="inventoryName"></param>
        /// <returns></returns>
        public IActionResult CreateInventory(string inventoryName)
        {
            if (inventoryName != null)
            {
                var result = _inventoryService.CreateInventory(UserController._userView.Id, inventoryName);
                if (result)
                {
                    var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
                    var viewInventory = SaveInViewModels(inventoryList);
                    return View("ViewAfterCreateInventory", viewInventory);
                }
                else
                {
                    var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
                    var viewInventory = SaveInViewModels(inventoryList);
                    return View("ViewInventoryNameRepeat", viewInventory);
                }

            }
            else
            {
                var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
                var viewInventory = SaveInViewModels(inventoryList);
                return View("ViewAfterCreateInventory", viewInventory);
            }
        }

        /// <summary>
        /// 分装清单视图模型
        /// </summary>
        /// <param name="inventoryList"></param>
        /// <returns></returns>
        public List<InventoryViewModel> SaveInViewModels(List<Inventorys> inventoryList)
        {
            List<InventoryViewModel> viewInventoryList = new List<InventoryViewModel>();
            foreach (var inventory in inventoryList)
            {
                var Inventory = new InventoryViewModel()
                {
                    InventoryName = inventory.InventoryName,
                    CreateTime = inventory.CreateTime,
                    InventoryId = inventory.InventoryId,
                    UserId = inventory.UserId
                };
                viewInventoryList.Add(Inventory);
            }
            return viewInventoryList;
        }

        /// <summary>
        /// 删除清单操作
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IActionResult RemoveInventory(int inventoryId)
        {
            _inventoryService.RemoveInventory(inventoryId);
            var inventoryList = _inventoryService.ViewAllInventory(UserController._userView.Id);
            var viewInventory = SaveInViewModels(inventoryList);

            return View("ViewAfterCreateInventory", viewInventory);
        }

        /// <summary>
        /// 修改清单名称页面
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult ViewModifyInventoryName(int inventoryId)
        {
            _inventory.InventoryId = inventoryId;
            var findInventory = FindAInventory(inventoryId);
            return View("ViewModifyInventoryName", findInventory);
        }
        /// <summary>
        /// 修改清单名称操作
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="newInventoryName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult GetModifyInventoryName(int inventoryId, string newInventoryName)
        {
            if (newInventoryName == null)
            {
                var inventory = _inventoryService.FindInventory(inventoryId);
                newInventoryName = inventory.InventoryName;
            }
            _inventoryService.ModifyInventoryName(inventoryId, newInventoryName);
            return RedirectToAction("ViewAfterModifyInventoryName");
        }
        /// <summary>
        /// 展示修改清单名字后的页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewAfterModifyInventoryName()
        {
            var findInventory = FindAInventory(_inventory.InventoryId);
            return View("ViewModifyInventoryName", findInventory);
        }
        /// <summary>
        /// 寻找指定清单操作
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public ModifyInventoryViewModel FindAInventory(int inventoryId)
        {
            var result = _inventoryService.FindInventory(_inventory.InventoryId);
            var itemsnumber = _inventoryService.ReturnInventoryItemsCount(_inventory.InventoryId);
            ModifyInventoryViewModel findInventory = new ModifyInventoryViewModel()
            {
                CreateTime = result.CreateTime,
                InventoryId = result.InventoryId,
                InventoryName = result.InventoryName,
                InventoryItemsCount = itemsnumber
            };
            return findInventory;
        }
    }
}


