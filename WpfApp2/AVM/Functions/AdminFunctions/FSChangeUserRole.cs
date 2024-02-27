﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Command;
using WpfApp2.Client;
using WpfApp2.Data.Account;
using System.Net.Http;
using System.Windows;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSChangeUserRole : SpeсialFunction
    {
        public FSChangeUserRole(AccountClient queryMaker) : base(queryMaker)
        {
            changeUserRole = new WCommand(o =>
            {
                if (UserInWork.Id != "") ExecuteChangeUserRole();
            });
        }

        public event ChangeUserHandler Notify;

        UserVMForAdmin userInWork;
        public UserVMForAdmin UserInWork
        {
            get { return userInWork; }
            set { userInWork = value; OnPropertyChanged("UserInWork"); }
        }

        public override HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    Notify?.Invoke();
                    
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }

        public void ExecuteChangeUserRole()
        {
            Result = Task.Run(() => queryMaker.ChangeUserRole(userInWork).GetAwaiter().GetResult()).Result;
        }

        public void FillUserInWork(UserVMForAdmin user)
        { UserInWork = user; }

        WCommand changeUserRole;
        public WCommand ChangeUserRole { get { return changeUserRole; } }
    }
}
