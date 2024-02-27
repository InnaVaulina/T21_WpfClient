using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Data;

namespace WpfApp2.AVM.Functions.BookFunctions
{
    public class FBookShowNote: AddressBookFunction
    {
        public FBookShowNote() : base(null) 
        { }

        public event ShowNoteHandler Notify;

        Note note;
        public Note Note
        {
            get { return note; }
            set 
            { 
                note = value;
                Notify?.Invoke(Note);
                OnPropertyChanged("Note"); 
            }
        }
    }
}
