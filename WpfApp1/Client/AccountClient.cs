using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json; 
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp1.AVM.Account;
using WpfApp1.Data.UserModel;

namespace WpfApp1.Client
{
    public class AccountClient
    {
        string baseAddress = "https://localhost:7298";
        

        public AccountClient() 
        { 
           
        }

        User user;
        public User User { set { user = value; } }


        public async Task<User> Login(EntryVM model) 
        {
            User user = null;
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/Login";
                    JsonContent content = JsonContent.Create(model);
                    HttpResponseMessage result = await client.PostAsync(url, content);
                    if (((int)result.StatusCode) == 200)
                    {
                        Stream stream = await result.Content.ReadAsStreamAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        user = await JsonSerializer.DeserializeAsync<User>(stream, options);
                    }
                }
                catch (Exception ex)
                {
                   
                }
            return user;
        }

        public async Task<List<UserVMForAdmin>> PullAdminList()
        {

            List<UserVMForAdmin> userList = new List<UserVMForAdmin>();
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Account/GetUserList";

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

                    HttpResponseMessage result = await client.GetAsync(url);
                    if (((int)result.StatusCode) == 200)
                    {
                        Stream stream = await result.Content.ReadAsStreamAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        userList = await JsonSerializer.DeserializeAsync<List<UserVMForAdmin>>(stream, options);
                    }

                }
                catch (Exception ex)
                {
                    
                }

            return userList;
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
