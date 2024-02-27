using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Client;
using WpfApp2.Data;

namespace WpfApp2.AVM.Functions.BookFunctions
{
    public delegate void ShowNoteHandler(Note note);
    public delegate void ChangeNoteHandler();
    public class AddressBookFunction : INotifyPropertyChanged
    {

        public AddressBookFunction(AddressBookClient queryMaker) 
        {
            this.queryMaker = queryMaker;
        }

        protected AddressBookClient queryMaker;


        HttpResponseMessage result;
        virtual public HttpResponseMessage Result 
        { get { return result; } set { result = value; } }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
