using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Service;
using ToDoList.Data;
using ToDo.Models;


namespace ToDo.Controllers
{
    public class ItemController : Controller
    {
        private ItemService _itemService = new ItemService();
        private InventoryService _inventoryService = new InventoryService();
        private ModelChangeViewModel _modelChange = new ModelChangeViewModel();
        UserViewModel _userView = UserController._userView;

        /// <summary>
        /// 展示未完成事项
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IActionResult ShowNoFinishItems(int inventoryId)
        {
            List<ItemViewModels> noFinished = SaveInViewModels(_itemService.GetItemsInInventory(inventoryId));
            var noFinishItems = new ItemsListViewModel()
            {
                noFinished = noFinished,
                InventoryId = inventoryId
            };
            return View("ShowNoFinishItems", noFinishItems);
        }
        /// <summary>
        /// 展示完成事项
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IActionResult ShowFinishItems(int inventoryId)
        {
            List<ItemViewModels> hasFinished = SaveInViewModels(_itemService.GetFinishedItemsInInventory(inventoryId));
            return View("ShowFinishItems", hasFinished);
        }
        /// <summary>
        /// 展示过期事项
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IActionResult ShowOverdueItems(int inventoryId)
        {
            List<ItemViewModels> hasOverDue = SaveInViewModels(_itemService.GetOverdueItemsInInventory(inventoryId));
            return View("ShowOverdueItems", hasOverDue);
        }
        //查看所有未完成事项 
        [HttpGet]
        public IActionResult ShowAllItem()
        {
            List<Items> allItems = new List<Items>();
            List<ItemViewModels> itemsView = new List<ItemViewModels>();

            allItems = _itemService.GetAllNotFinishIteams(_userView.Id);
            itemsView = SaveInViewModels(allItems);

            return View("ShowAllItem", itemsView);
        }

        //查看所有完成事项 
        [HttpGet]
        public IActionResult ShowAllFinishItem()
        {
            List<Items> allFinishItems = new List<Items>();
            List<ItemViewModels> itemsView = new List<ItemViewModels>();

            allFinishItems = _itemService.GetAllFinishIteams(_userView.Id);
            itemsView = SaveInViewModels(allFinishItems);

            return View("ShowAllFinishItem", itemsView);
        }

        //查看所有过期事项 
        [HttpGet]
        public IActionResult ShowAllOverdueItem()
        {
            List<Items> allOverdueItems = new List<Items>();
            List<ItemViewModels> itemsView = new List<ItemViewModels>();

            allOverdueItems = _itemService.GetAllOverdueIteams(_userView.Id);
            itemsView = SaveInViewModels(allOverdueItems);

            return View("ShowAllOverdueItem", itemsView);
        }

        //删除事项 （从所有未完成的事项中）
        [HttpGet]
        public IActionResult DeleteItem(int itemId)
        {
            _itemService.DeleteItem(itemId);

            return RedirectToAction("ShowAllItem");
        }

        //删除事项（从所有完成事项中）
        public IActionResult DeleteFinishItem(int itemId)
        {
            _itemService.DeleteItem(itemId);

            return RedirectToAction("ShowAllFinishItem");
        }

        //删除事项（从所有过期事项中）
        public IActionResult DeleteOverdueItem(int itemId)
        {
            _itemService.DeleteItem(itemId);

            return RedirectToAction("ShowAllOverdueItem");
        }

        //删除事项（从某一个清单的事项中）
        public JsonResult DeleteItemeInInventory(int itemId, int inventoryId)
        {
            _itemService.DeleteItem(itemId);
            return Json(inventoryId);
        }
        //改变事件状态(清单中的事项) 
        public IActionResult ChangeStatueInInventory(int itemId)
        {
            _itemService.IsCompletionItems(itemId);
            return Json("1231231");
        }

        //改变事件状态（所有未完成事项中）
        public IActionResult ChangeStatueInNotFinish(int itemId)
        {
            _itemService.IsCompletionItems(itemId);
            return RedirectToAction("ShowAllItem");
        }

        //改变事件状态（所有完成事项中）
        public IActionResult ChangeStatueInFinish(int itemId)
        {
            _itemService.IsCompletionItems(itemId);
            return RedirectToAction("ShowAllFinishItem");
        }

        //将获得的Items的值传给ItemViewModels
        public List<ItemViewModels> SaveInViewModels(List<Items> notFinishItems)
        {
            List<ItemViewModels> itemsView = new List<ItemViewModels>();
            foreach (var item in notFinishItems)
            {
                ItemViewModels itemView = new ItemViewModels()
                {
                    Content = item.Content,
                    CreateTime = item.CreateTime,
                    ItemId = item.ItemId,
                    UserId = item.UserId,
                    ItemOfInventoryId = item.ItemOfInventoryId,
                    TimeOfExpiration = item.TimeOfExpiration,
                    Mark = item.Mark,
                };
                itemsView.Add(itemView);
            }
            return itemsView;
        }

        //将获得的Inventory的值传给InventoryViewModels
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
                    UserId = inventory.UserId,
                };
                viewInventoryList.Add(Inventory);
            }
            return viewInventoryList;
        }

        public IActionResult ChangeItem(int itemId, bool isReturnSearch, string content, bool isReturnHomePage)
        {

            ItemViewModels item = _modelChange.ChangeItem(_itemService.GetItem(itemId));
            if (item.TimeOfExpiration != null)
            {
                item.TimeToString = _modelChange.ChangeTimeToString((DateTime)item.TimeOfExpiration);
            }
            item.IsReturnSearch = isReturnSearch;
            item.ChangeBeforeContent = content;
            item.IsReturnHomePage = isReturnHomePage;
            return View(item);
        }
        /// <summary>
        /// 返回进入界面
        /// </summary>
        /// <param name="content"></param>
        /// <param name="mark"></param>
        /// <param name="deadTime"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetBack(string content, string mark, DateTime deadTime, int itemId, bool isReturnSearch, string changeBeforeContent, bool isReturnHomePage)
        {
            _itemService.ChangeItem(itemId, content, deadTime, mark);
            ItemViewModels item = _modelChange.ChangeItem(_itemService.GetItem(itemId));
            if (isReturnHomePage)
            {
                return RedirectToAction("ShowHomePage", new { inventoryID = item.ItemOfInventoryId });
            }
            else
            {
                if (isReturnSearch)
                {
                    List<Items> items = _itemService.SearchItem(changeBeforeContent, _userView.Id);
                    List<ItemViewModels> _items = _modelChange.ChangeItem(items);
                    //items.Add(item);
                    return View("SearchReasult", _items);
                }
                if (item.State)
                {
                    _itemService.OverTime(_userView.Id);
                    return RedirectToAction("ShowAllFinishItem");
                }
                if (item.IsOverdue)
                {
                    _itemService.OverTime(_userView.Id);
                    return RedirectToAction("ShowAllOverdueItem");
                }
                _itemService.OverTime(_userView.Id);
                return RedirectToAction("ShowAllItem");
            }

        }
        public IActionResult GetBack(int itemId, bool isReturnSearch, string changeBeforeContent, bool isReturnHomePage)
        {
            var items = _itemService.GetItem(itemId);
            ItemViewModels item = _modelChange.ChangeItem(items);
            if (isReturnHomePage)
            {
                return RedirectToAction("ShowHomePage", new { inventoryID = items.ItemOfInventoryId });
            }
            if (isReturnSearch)
            {
                List<Items> Viewitems = _itemService.SearchItem(changeBeforeContent, _userView.Id);
                List<ItemViewModels> _items = _modelChange.ChangeItem(Viewitems);
                //items.Add(item);
                return View("SearchReasult", _items);
            }
            if (item.State)
            {
                return RedirectToAction("ShowAllFinishItem");
            }
            if (item.IsOverdue)
            {
                return RedirectToAction("ShowAllOverdueItem");
            }
            return RedirectToAction("ShowAllItem");
        }
        /// <summary>
        /// 查找事项
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IActionResult FindItem(string content)
        {
            List<Items> items = _itemService.SearchItem(content, _userView.Id);
            List<ItemViewModels> _items = _modelChange.ChangeItem(items);
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].ChangeBeforeContent = content;
            }
            if (_items.Count != 0)
            {
                return View("SearchReasult", _items);
            }
            else if (content != null)
            {
                return View(1);
            }
            else
                return View();
        }
        /// <summary>
        /// 在查找中删除事项
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteItemInSearch(int itemId)
        {
            _itemService.DeleteItem(itemId);

            return RedirectToAction("FindItem");
        }
        /// <summary>
        /// 展示添加事项的页面
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IActionResult AddItem(int inventoryId)
        {
            return View(inventoryId);
        }
        /// <summary>
        /// 添加事项操作
        /// </summary>
        /// <param name="inventoryID"></param>
        /// <param name="content"></param>
        /// <param name="mark"></param>
        /// <param name="time"></param>
        public IActionResult ToAddItem(int inventoryID, string content, string mark, DateTime time)
        {
            _itemService.AddItem(content, time, mark, _userView.Id, inventoryID);
            return RedirectToAction("ShowHomePage", new { inventoryID = inventoryID });
        }

        public IActionResult ShowHomePage(int inventoryID)
        {
            List<ItemViewModels> noFinished = SaveInViewModels(_itemService.GetItemsInInventory(inventoryID));
            List<ItemViewModels> hasFinished = SaveInViewModels(_itemService.GetFinishedItemsInInventory(inventoryID));
            List<ItemViewModels> hasOverDue = SaveInViewModels(_itemService.GetOverdueItemsInInventory(inventoryID));
            List<InventoryViewModel> allInventory = SaveInViewModels(_inventoryService.ViewAllInventory(_userView.Id));
            ItemsListViewModel itemList = new ItemsListViewModel()
            {
                noFinished = noFinished,
                hasFinished = hasFinished,
                hasOverdue = hasOverDue,
                allInventorys = allInventory,
                InventoryId = inventoryID
            };
            return View("ShowItems", itemList);
        }

        /// <summary>
        /// 过期事项提醒
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public IActionResult OverdueItemRemind(string days)
        {
            List<ItemViewModels> overdueItems = SaveInViewModels(_itemService.GetWillOverdueItems(_userView.Id, days));
            return View("ViewOverdueItemsRemind", overdueItems);
        }
    }
}