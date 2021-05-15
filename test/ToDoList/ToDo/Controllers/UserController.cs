using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Data.Service;
using ToDo.Models;
using System.Collections.Generic;
using ToDoList.Data;


namespace ToDo.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService = new UserService();
        private InventoryService _inventoryService = new InventoryService();
        private ItemService _itemService = new ItemService();
        public static UserViewModel _userView = new UserViewModel();

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Function()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                //登录后的清单界面
                return View("ViewInventory", "Inventory");
            }
            else
                return View("Login");
        }

        [HttpPost]
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult Login(string userName, string password)
        {
            if (userName == "" || userName == null || password == "" || password == null)
            {
                return View("Null");
            }
            else
            {
                int result = _userService.Login(userName, password);
                //登录成功
                if (result != 0 && result != -1)
                {
                    HttpContext.Session.SetString("user", userName);
                    SaveUserInformation(result);//保存用户信息
                    _itemService.OverTime(result);//将已过期事项改变状态
                    return RedirectToAction("ViewInventory", "Inventory");
                }
                //密码错误
                else if (result == 0)
                    return View("Wrong");
                else
                    return View("NotExit");
            }

        }


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


        public IActionResult ToRegister()
        {
            return View("Register");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IActionResult Register(string userName, string password, string repassword)
        {
            var result = _userService.Register(userName, password, repassword);
            if (result == 1)
            {
                return View("Login");
            }
            else if (result == -1)
                return View("Exit");
            else if (result == 0)
                return View("Failed");
            else
                return View("NotNull");
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            return View("Login");
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public IActionResult ModifyPassword(string newPassword, string againNewPassword, string oldPassword)
        {
            var user = _userService.FindUser(_userView.Id);
            if (oldPassword == user.Password && newPassword == againNewPassword)
            {
                _userService.ModifyUserPassWord(_userView.Id, newPassword);
                return View("UserInformationPage", _userView);
            }
            else
                return View("ErrorPopup", 1);

        }

        public IActionResult ViewErrorPopup()
        {
            return View("ErrorPopup", 2);
        }
        /// <summary>
        /// 展示修改密码界面
        /// </summary>
        /// <returns></returns>
        public IActionResult GetModifyPasswordPage()
        {
            return View("ModifyPasswordPage");
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="newUserName"></param>
        /// <returns></returns>
        public IActionResult ModifyUserInformation(string newUserName, string newEmail)
        {
            if (newUserName == null)
            {
                newUserName = _userView.UserName;
            }
            if (newEmail == null)
            {
                newEmail = _userView.Email;
            }
            _userService.ModifyUserInformation(_userView.Id, newUserName, newEmail);
            SaveUserInformation(_userView.Id);//保存用户信息
            return RedirectToAction("UserInformationPage");
        }

        //保存登陆用户的ID和信息
        public void SaveUserInformation(int userId)
        {
            var user = _userService.FindUser(userId);
            _userView.Id = user.Id;
            _userView.Sex = user.Sex;
            _userView.Email = user.EMail;
            _userView.UserName = user.UserName;
        }

        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <returns></returns>
        public IActionResult ModifyPasswordPage()
        {
            return View("ModifyPasswordPage");
        }

        /// <summary>
        /// 用户信息界面
        /// </summary>
        /// <returns></returns>
        public IActionResult UserInformationPage()
        {
            return View("UserInformationPage", _userView);
        }

    }
}