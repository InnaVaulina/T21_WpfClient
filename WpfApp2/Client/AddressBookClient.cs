using System.Collections.Generic;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp2.Data;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace WpfApp2.Client
{
    public class AddressBookClient
    {
        IndexViewData _viewLetterPage;
        string baseAddress = ConfigurationManager.AppSettings["ApiUrl"];


        public AddressBookClient(IndexViewData viewData, User user)
        {
            _viewLetterPage = viewData;
            this.user = user;
        }

        User user;
        //public User User { set { user = value; } }
            
        public IndexViewData Index { get { return _viewLetterPage; } }
 
        public async Task<HttpResponseMessage> GetNoteListByLetter(string letter, int page)
        {
            _viewLetterPage.ChooseLetter(letter);
            _viewLetterPage.Move(page);
            using (var client = new HttpClient())
                try
                {
                    if(user!=null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var url = $"{baseAddress}/api/Home/GetByTheLetter?letter={_viewLetterPage.Letter}&page={_viewLetterPage.Page}";
                    HttpResponseMessage result = await client.GetAsync(url);
                    return result;
                }
                catch (Exception ex)
                {

                }
            return null;
        }


        public async Task<HttpResponseMessage> GetNoteListByClue(string clue, int page)
        {
            _viewLetterPage.ChooseClue(clue);
            _viewLetterPage.Move(page);
            List<Note> notelist = new List<Note>();
            using (var client = new HttpClient())
                try
                {
                    if (user != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var url = $"{baseAddress}/api/Home/GetByTheClue?clue={_viewLetterPage.Clue}&page={_viewLetterPage.Page}";
                    HttpResponseMessage result = await client.GetAsync(url);
                    return result;
                }
                catch (Exception ex)
                {
                   
                }
            return null;
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
