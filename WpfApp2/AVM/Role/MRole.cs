using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Client;
using WpfApp2.Data;
using WpfApp2.AVM.Functions.AdminFunctions;
using WpfApp2.AVM.Functions.BookFunctions;

namespace WpfApp2.AVM.Role
{
    
    public class MRole: INotifyPropertyChanged
    {
        protected AccountClient queryMaker1;
        protected AddressBookClient queryMaker2;

        public event LogInUserHandler Notify_UserIn;
        public event LogOutUserHandler Notify_UserOut;
        public MRole(User user) 
        {
            queryMaker1 = new AccountClient(user);
            queryMaker2 = new AddressBookClient(new IndexViewData(), user);
        }

        protected FBookSelectList bookList;
        public FBookSelectList BookList { get { return bookList; } }

        protected AddressBookFunction addressBookFunction;
        public AddressBookFunction AddressBookFunction
        {
            get { return addressBookFunction; }
            set { addressBookFunction = value; OnPropertyChanged("AddressBookFunction"); }
        }

        protected FBookShowNote showNote;
        public FBookShowNote ShowNote { get { return showNote; } }

        public void LogNewUser(User user) { Notify_UserIn?.Invoke(user); }
        public void LogOutUser() { Notify_UserOut?.Invoke(); }


        public event PropertyChangedEventHandler PropertyChanged;       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
