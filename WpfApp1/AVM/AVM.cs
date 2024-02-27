using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.AVM.Account;
using WpfApp1.AVM.Roles;
using WpfApp1.Client;
using WpfApp1.Command;
using WpfApp1.Data.UserModel;

namespace WpfApp1.AVM
{
    public class MyAVM: INotifyPropertyChanged
    {       
        AccountClient queryMaker;
        WindowLog logWindow;
        RegWindow regWindow;
 

        public MyAVM() 
        {
            userInRole = new VMRoleAnonymous(this);
            Message = "Вы вошли как анонимный пользователь. Вы можете просмтривать записную книжку.";
            entry = new EntryVM();
            register = new RegisterVM();
            queryMaker = new AccountClient();


            openLogWindow = new WCommand(o =>
            {
                logWindow = new WindowLog(this);
                logWindow.Show();
            });

            logUser = new WCommand(o =>
            {
                if (entry.LoginProp != "" || entry.PassWord != "")
                {
                    var response = Task.Run(() => queryMaker.Login(entry).GetAwaiter().GetResult());
                    if (response.Result != null)
                    {
                        User user = response.Result;
                        if (user.UserRole == "user") 
                        {
                            UserInRole = new VMRoleAddMaker(new MAuthorizedUser(user), this);
                            Message = $"{user.UserName} в системе. Вы можете просмтривать записную книжку и добавлять записи.";
                        }
                            
                        if (user.UserRole == "admin") 
                        {
                            UserInRole = new VMRoleAdmin(new MAuthorizedUser(user), this);
                            Message = $"{user.UserName} в системе. У вас доступ администратора.";
                        }
                            
                        logWindow.Close();
                        MessageBox.Show("Вход выполнен");
                    }
                    else
                        MessageBox.Show("Не удалось выполнить вход");
                }
                else
                    MessageBox.Show("не заполнены данные");
            });


            logout = new WCommand(o => 
            {
                UserInRole = new VMRoleAnonymous(this);
                Message = "Вы вошли как анонимный пользователь. Вы можете просмтривать записную книжку.";
            });

            openRegWindow = new WCommand(o => 
            {
                regWindow = new RegWindow(this);
                regWindow.Show();
            });

            registerNewUser = new WCommand(o => 
            {
                if(register.LoginProp !="" || register.Password !="" || register.ConfirmPassword != "") 
                {
                    var response = Task.Run(() => queryMaker.Register(register).GetAwaiter().GetResult());
                    if (response.Result != null)
                    {
                        if ((int)response.Result.StatusCode == 200 || (int)response.Result.StatusCode == 201)
                        {
                            regWindow.Close();
                            register.LoginProp = "";
                            register.Password = "";
                            register.ConfirmPassword = "";
                            MessageBox.Show("Запрос выполнен");
                        }
                        else
                            MessageBox.Show($"Запрос не выполнен. Причина: {response.Result.StatusCode}");
                    }
                    else
                        MessageBox.Show("Не удалось выполнить регистрацию");
                }
                else
                    MessageBox.Show("не заполнены данные");
            });

        }

        VMRole userInRole;
        public VMRole UserInRole 
        { 
            get { return userInRole; } 
            set { userInRole = value; OnPropertyChanged("UserInRole"); } 
        }

        EntryVM entry;
        public EntryVM Entry { get { return entry; } }

        string message;
        public string Message 
        { 
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        RegisterVM register;
        public RegisterVM Register
        {
            get { return register; }
            set { register = value; }
        }


        WCommand openRegWindow;
        public WCommand OpenRegWindow { get { return openRegWindow; } }


        WCommand openLogWindow;
        public WCommand OpenLogWindow { get { return openLogWindow; } }

        WCommand logUser;             
        public WCommand LogUser { get { return logUser; } }

        WCommand logout;
        public WCommand Logout { get { return logout; } }

        WCommand registerNewUser;
        public WCommand RegisterNewUser { get { return registerNewUser; } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
