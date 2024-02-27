using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.AVM.Account;
using WpfApp1.Client;

namespace WpfApp1.Data.UserModel
{
    public class MAuthorizedUser: MUser
    {
        User user;

        AccountClient queryMaker2;


        public MAuthorizedUser(User user):base() 
        {
            this.user = user;
            this.queryMaker.User = user;
            queryMaker2 = new AccountClient();
            queryMaker2.User = user;
            newNote = new Note();
            register = new RegisterVM();
        }

        Note newNote;
        public Note NewNote
        {
            get { return newNote; }
            set { newNote = value; OnPropertyChanged("NewNote"); }
        }

        RegisterVM register;
        public RegisterVM Register 
        { 
            get { return register; }
            set { register = value; }
        }


        UserVMForAdmin userInWork;
        public UserVMForAdmin UserInWork
        {
            get { return userInWork; }
            set { userInWork = value; OnPropertyChanged("UserInWork"); }
        }

        ObservableCollection<UserVMForAdmin> userInWorkList;
        public ObservableCollection<UserVMForAdmin> UserInWorkList 
        {  
            get { return userInWorkList; }
            set { userInWorkList = value; OnPropertyChanged("UserInWorkList"); }
        }


        HttpResponseMessage result;
        HttpResponseMessage Result 
        { 
            set 
            { 
                result = value; 
                if((int)result.StatusCode == 200 || (int)result.StatusCode == 201)
                {
                    NewNote.Clear();
                    FillNoteList();
                    MessageBox.Show("Запрос выполнен");
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {result.StatusCode}");
            } 
        }

        HttpResponseMessage adminresult;
        HttpResponseMessage AdminResult
        {
            set
            {
                adminresult = value;
                if ((int)adminresult.StatusCode == 200 || (int)adminresult.StatusCode == 201)
                {
                    FillUsersList();
                    MessageBox.Show("Запрос выполнен");
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {adminresult.StatusCode}");
            }
        }

        public void AddNote()
        {
            Result = Task.Run(() => queryMaker.AddNote(NewNote).GetAwaiter().GetResult()).Result;             
        }

        public void RemoveNote() 
        {
            Result = Task.Run(() => queryMaker.DeleteNote(Note.Id).GetAwaiter().GetResult()).Result;

        }
        
        public void ChangeNote() 
        {
            Result = Task.Run(() => queryMaker.ChangeNote(Note.Id,NewNote).GetAwaiter().GetResult()).Result;
            
        }

        public void FillUsersList()
        {
            var list = Task.Run(() => queryMaker2.PullAdminList().GetAwaiter().GetResult());
            UserInWorkList = new ObservableCollection<UserVMForAdmin>(list.Result);
        }

        public void DeleteUser() 
        {
            AdminResult = Task.Run(() => queryMaker2.DeleteUser(userInWork).GetAwaiter().GetResult()).Result;
        }


        public void AddUser() 
        {
            AdminResult = Task.Run(() => queryMaker2.Register(register).GetAwaiter().GetResult()).Result;
        }


        public void ChangeUserRole() 
        {
            AdminResult = Task.Run(() => queryMaker2.ChangeUserRole(userInWork).GetAwaiter().GetResult()).Result;
        }
    }
}
