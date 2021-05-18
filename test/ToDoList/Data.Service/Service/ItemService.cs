using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Data;

namespace Data.Service
{
    public class ItemService
    {
        private ToDoListContext _ctx = new ToDoListContext();

        /// <summary>
        /// 获取一个清单内未完成的事项
        /// </summary>
        /// <returns></returns>
        public List<Items> GetItemsInInventory(int inventoryId)
        {
            List<Items> items = _ctx.Items.Where(p => p.ItemOfInventoryId == inventoryId 
            && p.State==false && p.IsOverdue==false).ToList();
            return items;
        }

        /// <summary>
        /// 获取清单内已完成的事项
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<Items> GetFinishedItemsInInventory(int inventoryId)
        {
            List<Items> items = _ctx.Items.Where(p => p.ItemOfInventoryId == inventoryId && p.State==true).ToList();
            return items;
        }

        /// <summary>
        /// 获取一个清单内未完成且过期的事项
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<Items> GetOverdueItemsInInventory(int inventoryId)
        {
            List<Items> items = _ctx.Items.Where(p => p.ItemOfInventoryId == inventoryId
            && p.State==false && p.IsOverdue==true).ToList();
            return items;
        }

        /// <summary>
        /// 获取所有未完成的事项
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Items> GetAllNotFinishIteams(int userId)
        {
            List<Items> items = _ctx.Items.Where(p => p.UserId == userId 
            && p.State==false && p.IsOverdue==false).ToList();
            return items;
        }


        /// <summary>
        /// 获取所有完成的事项
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Items> GetAllFinishIteams(int userId)
        {
            List<Items> items = _ctx.Items.Where(p => p.UserId == userId
            && p.State==true).ToList();
            return items;
        }

        /// <summary>
        /// 获取所有未完成的事项
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Items> GetAllOverdueIteams(int userId)
        {
            List<Items> items = _ctx.Items.Where(p => p.UserId == userId
            && p.State == false && p.IsOverdue == true).ToList();
            return items;
        }

        /// <summary>
        /// 删除一个事项
        /// </summary>
        /// <param name="itemId"></param>
        public void DeleteItem(int itemId)
        {
            Items item = _ctx.Items.SingleOrDefault(p => p.ItemId == itemId);
            _ctx.Items.Remove(item);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// 将过期事项改为过期状态
        /// </summary>
        /// <param name="thing"></param>
        public void OverTime(int userId)
        {
            List<Items> items = new List<Items>();
            items = _ctx.Items.Where(item => item.UserId == userId).ToList();
            foreach (var item in items)
            {
                item.IsOverdue = item.TimeOfExpiration <= DateTime.Now;
            }
            _ctx.SaveChanges();
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="itemId"></param>
        public void IsCompletionItems(int itemId)
        {
            var completionItem = _ctx.Items.Where(item => item.ItemId == itemId).SingleOrDefault();
            completionItem.State = !completionItem.State;
            _ctx.SaveChanges();
        }

        /// <summary>
        /// 添加事项
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="inventoryName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool setItem(string userName, string inventoryName, string content)
        {
            var user = _ctx.Users.ToList().SingleOrDefault(u => u.UserName == userName);
            var inventory = user.Inventorys.ToList().SingleOrDefault(invent => invent.InventoryName == inventoryName);
            if (user != null && inventory != null)
            {
                Items item = new Items();
                item.ItemOfInventoryId = inventory.InventoryId;
                item.UserId = user.Id;
                item.CreateTime = DateTime.Now;
                item.IsOverdue = true;
                item.State = true;
                item.Content = content;
                _ctx.Items.Add(item);
                user.Items.Add(item);
                inventory.Items.Add(item);
                _ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 添加事项的备注
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="mark"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool setItemMark(int itemId, string mark, string userName)
        {
            var user = _ctx.Users.ToList().SingleOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                var item = user.Items.SingleOrDefault(i => i.ItemId == itemId);
                if (item != null)
                {
                    item.Mark = mark;
                    _ctx.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 添加事项的过期时间
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="timeOfExpiration"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool setItemTimeOfExpiration(int itemId, DateTime timeOfExpiration, string userName)
        {
            var user = _ctx.Users.ToList().SingleOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                var item = user.Items.SingleOrDefault(i => i.ItemId == itemId);
                if (item != null)
                {
                    item.TimeOfExpiration = timeOfExpiration;
                    _ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 修改事项的内容
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="content"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool setItemContent(int itemId, string content, string userName)
        {
            var user = _ctx.Users.ToList().SingleOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                var item = user.Items.SingleOrDefault(i => i.ItemId == itemId);
                if (item != null)
                {
                    item.Content = content;
                    _ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 通过id查找，修改事项内容
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool setItemContent(int itemId, string content)
        {

            var item = _ctx.Items.SingleOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                item.Content = content;
                _ctx.SaveChanges();
                return true;
            }
            return false;

        }

        /// <summary>
        /// 查找事项通过ID
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Items GetItem(int itemId)
        {
            return _ctx.Items.SingleOrDefault(i => i.ItemId == itemId);
        }
        /// <summary>
        /// 修改事项
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemDeadLine"></param>
        /// <param name="itemMark"></param>
        /// <returns></returns>
        public bool ChangeItem(int itemId, string itemContent, DateTime itemDeadLine, string itemMark)
        {
            Items item = _ctx.Items.SingleOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                item.Content = itemContent;
                item.TimeOfExpiration = itemDeadLine;
                item.Mark = itemMark;
                _ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 通过关键字查找事项
        /// </summary>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Items> SearchItem(string keyword, int userId)
        {
            var items = _ctx.Items.Where(p => p.Content.Contains(keyword) && p.UserId == userId).ToList();
            return items;
        }
        /// <summary>
        /// 添加事项
        /// </summary>
        /// <returns></returns>
        public bool AddItem(string content,DateTime timeOfExpiration, string mark,int userId,int inventoryId)
        {
            if (content != null)
            {
                DateTime createTime = DateTime.Now;
                bool isOverdue=true;
                if (createTime<= timeOfExpiration)
                    isOverdue = false;
                Items item = new Items()
                {
                    IsOverdue=isOverdue,
                    Mark=mark,
                    UserId=userId,
                    ItemOfInventoryId=inventoryId,
                    TimeOfExpiration=timeOfExpiration,
                    CreateTime=createTime,
                    State=false,
                    Content=content,
                    
                };
                _ctx.Items.Add(item);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        //获取即将过期的事项（还有1天过期）
        public List<Items> GetWillOverdueItems(int userId, string a)
        {
            List<Items> items = _ctx.Items.Where(p => p.UserId == userId).ToList();
            List<Items> willOverdueItems = new List<Items>();
            foreach (var item in items)
            {
                DateTime t1 = item.CreateTime;
                DateTime t2 = item.TimeOfExpiration;

                DateTime Convert_dt1 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", t1.Year, t1.Month, t1.Day));
                DateTime Convert_dt2 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", t2.Year, t2.Month, t2.Day));

                string Days = (Convert_dt2 - Convert_dt1).Days.ToString();
                if (Days.Equals(a))
                {
                    willOverdueItems.Add(item);
                }
            }

            return willOverdueItems;
        }

    }
}
