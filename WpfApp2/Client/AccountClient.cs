using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json; 
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp2.Data;
using WpfApp2.Data.Account;

namespace WpfApp2.Client
{
    public class AccountClient
    {
        string baseAddress = ConfigurationManager.AppSettings["ApiUrl"];
        

        public AccountClient(User user) 
        {
            this.user = user;
        }

        User user;
        //public User User { set { user = value; } }


        public async Task<HttpResponseMessage> Login(EntryVM model) 
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/Login";
                    JsonContent content = JsonContent.Create(model);
                    HttpResponseMessage result = await client.PostAsync(url, content);
                    return result;                   
                }
                catch (Exception ex)
                {
                   
                }
            return null;
        }

        public async Task<HttpResponseMessage> PullAdminList()
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/GetUserList";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    HttpResponseMessage result = await client.GetAsync(url);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }

            return null;
        }




        public async Task<HttpResponseMessage> DeleteUser(UserVMForAdmin usermodel)
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/DeleteUser?id={usermodel.Id}";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    HttpResponseMessage result = await client.DeleteAsync(url);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }

            return null;

        }



        public async Task<HttpResponseMessage> Register(RegisterVM model)
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/Register";
                    JsonContent content = JsonContent.Create(model);
                    HttpResponseMessage result = await client.PostAsync(url, content);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }

            return null;
        }



        public async Task<HttpResponseMessage> ChangeUserRole(UserVMForAdmin usermodel)
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/ChangeUserRole?id={usermodel.Id}";
                    JsonContent content = JsonContent.Create(new { Role = usermodel.UserRole });
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    HttpResponseMessage result = await client.PutAsync(url, content);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }

            return null;

        }

    }
}
