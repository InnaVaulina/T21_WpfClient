using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp1.Data;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Text.Json;
using WpfApp1.AVM.Roles;
using System.Windows;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WpfApp1.Data.UserModel;

namespace WpfApp1.Client
{
    public class AddressBookClient
    {
        IndexViewData _viewLetterPage;
        string baseAddress = "https://localhost:7298";


        public AddressBookClient(IndexViewData viewData)
        {
            _viewLetterPage = viewData;
        }

        User user;
        public User User { set { user = value; } }
            

        public async Task<List<Note>> GetNoteList(string letter, int page)
        {

            _viewLetterPage.ChooseLetter(letter);
            _viewLetterPage.Move(page);

            List<Note> notelist = new List<Note>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Home/GetByTheLetter?letter={_viewLetterPage.Letter}&page={_viewLetterPage.Page}";
                    HttpResponseMessage result = await client.GetAsync(url);
                    if (((int)result.StatusCode) == 200) 
                    {
                        Stream stream = await result.Content.ReadAsStreamAsync();
                        notelist = await JsonSerializer.DeserializeAsync<List<Note>>(stream, options);
                    }

                    if (notelist.Count == 0 && _viewLetterPage.Page > 0)
                    {
                        _viewLetterPage.ChooseLetter(_viewLetterPage.Letter);
                        _viewLetterPage.Move(_viewLetterPage.Page - 1);
                        url = $"{baseAddress}/api/Home/GetByTheLetter?letter={_viewLetterPage.Letter}&page={_viewLetterPage.Page}";
                        result = await client.GetAsync(url);
                        if (((int)result.StatusCode) == 200)
                        {
                            Stream stream = await result.Content.ReadAsStreamAsync();
                            notelist = await JsonSerializer.DeserializeAsync<List<Note>>(stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //return NotFound(ex.Message);
                }

            return notelist;

        }


        public async Task<List<Note>> GetNoteListByClue(string clue, int page)
        {
            _viewLetterPage.ChooseClue(clue);
            _viewLetterPage.Move(page);

            List<Note> notelist = new List<Note>();

            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Home/GetByTheClue?clue={_viewLetterPage.Clue}&page={_viewLetterPage.Page}";
                    HttpResponseMessage result = await client.GetAsync(url);


                    if (((int)result.StatusCode) == 200)
                    {
                        Stream stream = await result.Content.ReadAsStreamAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        notelist = await JsonSerializer.DeserializeAsync<List<Note>>(stream, options);
                    }

                }
                catch (Exception ex)
                {
                    //return NotFound(ex.Message);
                }

            return notelist;

        }


        public async Task<HttpResponseMessage> AddNote(Note note)
        {      
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Home/AddNote";
                    JsonContent content = JsonContent.Create(note);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    HttpResponseMessage result = await client.PostAsync(url, content);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }
            return null;
        }



        public async Task<HttpResponseMessage> ChangeNote(int id, Note note)
        {
            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Single/ChangeNote?id={id}";
                    JsonContent content = JsonContent.Create(note);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);   
                    HttpResponseMessage result = await client.PutAsync(url, content);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }
            return null;
        }


        public async Task<HttpResponseMessage> DeleteNote(int id)
        {

            using (var client = new HttpClient())
                try
                {
                    var url = $"{baseAddress}/api/Single/DeleteNote?id={id}";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    HttpResponseMessage result = await client.DeleteAsync(url);
                    return result;
                }
                catch (Exception ex)
                {
                    
                }
            return null;
        }



    }
}
