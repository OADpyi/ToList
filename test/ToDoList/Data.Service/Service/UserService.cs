using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Data;

namespace Data.Service
{
    public class UserService
    {
        private ToDoListContext _database = new ToDoListContext();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string userName, string password)
        {
            var User = _database.Users.ToList();
            bool judge = false;
            foreach (var item in User)
            {
                if (item.UserName == userName)
                {
                    judge = true;
                    break;
                }
                else
                    judge = false;
            }
            if (judge)
            {
                var user = _database.Users.SingleOrDefault(props => props.UserName.Equals(userName));
                if (user.Password.Equals(password))
                {
                    return user.Id;
                }
                else
                    return 0;
            }
            else
                return -1;
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="repassword"></param>
        /// <returns></returns>
        public int Register(string userName, string password, string repassword)
        {
            var userList = _database.Users;
            var judge = false;
            if (userName == null || password == null || repassword == null)
                return -2;
            else
            {
                foreach (var item in userList)
                {
                    if (item.UserName != userName)
                        judge = true;
                    else
                        judge = false;
                }
                if (judge)
                {
                    if (password.Equals(repassword))
                    {
                        var user = new Users()
                        {
                            EMail = "zvvxcv",
                            UserName = userName,
                            Password = password,
                            Birthday = DateTime.Now,
                            Sex="男"
                        };
                        _database.Users.Add(user);
                        _database.SaveChanges();
                        return 1;
                    }
                    else
                        return 0;
                }
                else
                    return -1;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void Quit()
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// 切换账号
        /// </summary>
        public int ChangeAccount(string userName)
        {
            var user = _database.Users.Find(userName);
            return user.Id;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public void ModifyUserPassWord(int userId, string password)
        {
            var user = _database.Users.SingleOrDefault(u => u.Id == userId);
            user.Password = password;
            _database.SaveChanges();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public void ModifyUserInformation(int userId, string userName, string email)
        {
            var user = _database.Users.SingleOrDefault(u => u.Id == userId);
            user.UserName = userName;
            user.EMail = email;
            _database.SaveChanges();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users FindUser(int userId)
        {
            return _database.Users.SingleOrDefault(user => user.Id == userId);
        }

        

    }
}
