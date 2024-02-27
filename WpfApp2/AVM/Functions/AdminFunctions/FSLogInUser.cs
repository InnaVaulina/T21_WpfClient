using System.Threading.Tasks;
using WpfApp2.Data.Account;
using WpfApp2.Data;
using WpfApp2.Client;
using WpfApp2.Command;
using System.Net.Http;
using System.Windows;
using System;
using System.IO;
using System.Text.Json;


namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSLogInUser : SpeсialFunction
    {
        public FSLogInUser(AccountClient queryMaker) : base(queryMaker)
        {
            user = null;
            entry = new EntryVM();

            logUser = new WCommand(o =>
            {
                if (entry.LoginProp != "" || entry.PassWord != "")
                    ExecuteLoginUser();
 
            });

        }

        public event LogInUserHandler Notify;

        EntryVM entry;
        public EntryVM Entry { get { return entry; } }

        User user;

        User User 
        { 
            get { return user; }
            set 
            { 
                user = value;
                Notify?.Invoke(user);
            }
        }


        public override HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    User = DeserializeResponse();
                    MessageBox.Show($"Запрос выполнен.");
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }
        public void ExecuteLoginUser()
        {
            Result = Task.Run(() => queryMaker.Login(entry).GetAwaiter().GetResult()).Result;
        }

        public User DeserializeResponse() 
        {
            return Task.Run(async () => 
            {
                Stream stream = await Result.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return await JsonSerializer.DeserializeAsync<User>(stream, options);
            }).Result;
        }

        WCommand logUser;
        public WCommand LogUser { get { return logUser; } }


    }
}
