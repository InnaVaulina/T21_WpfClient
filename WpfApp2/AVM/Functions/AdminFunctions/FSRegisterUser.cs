using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Data.Account;
using WpfApp2.Client;
using WpfApp2.Command;
using System.Net.Http;
using System.Windows;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSRegisterUser: SpeсialFunction
    {

        public FSRegisterUser(AccountClient queryMaker): base(queryMaker) 
        {
            register = new RegisterVM();

            regUser = new WCommand(o =>
            {
                if (Register.LoginProp != "" && Register.Password != "" && Register.ConfirmPassword != "")
                    ExecuteRegistrUser();
            });
        }

        public event ChangeUserHandler Notify;

        RegisterVM register;
        public RegisterVM Register
        {
            get { return register; }
            set { register = value; }
        }

        public override HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    Register.Clear();
                    Notify?.Invoke();
                    MessageBox.Show($"Запрос выполнен.");
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }

        public void ExecuteRegistrUser()
        {
            Result = Task.Run(() => queryMaker.Register(register).GetAwaiter().GetResult()).Result;
        }

        WCommand regUser;
        public WCommand RegUser { get { return regUser; } }
    }
}
