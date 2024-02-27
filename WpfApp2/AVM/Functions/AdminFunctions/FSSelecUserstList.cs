using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfApp2.Client;
using WpfApp2.Data.Account;
using WpfApp2.Command;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSSelecUserstList: SpeсialFunction
    {
        public FSSelecUserstList(AccountClient queryMaker) : base(queryMaker) 
        {
            pullUsersList = new WCommand(o => { ExecuteFillUsersList(); });
        }

        ObservableCollection<UserVMForAdmin> userInWorkList;
        public ObservableCollection<UserVMForAdmin> UserInWorkList
        {
            get { return userInWorkList; }
            set { userInWorkList = value; OnPropertyChanged("UserInWorkList"); }
        }

        public override HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    UserInWorkList = DeserializeResponse();
                    
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }

        public void ExecuteFillUsersList()
        {
            Result = Task.Run(() => queryMaker.PullAdminList().GetAwaiter().GetResult()).Result;
            
        }


        public ObservableCollection<UserVMForAdmin> DeserializeResponse()
        {
            return Task.Run(async () =>
            {
                Stream stream = await Result.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var list = await JsonSerializer.DeserializeAsync<List<UserVMForAdmin>>(stream, options);
                return new ObservableCollection<UserVMForAdmin>(list);
            }).Result;
        }

        WCommand pullUsersList;
        public WCommand PullUsersList { get { return pullUsersList; } }
    }
}
