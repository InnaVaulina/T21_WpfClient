using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Client;
using WpfApp2.Command;
using WpfApp2.Data;

namespace WpfApp2.AVM.Functions.BookFunctions
{
    public class FBookDeleteNote: AddressBookFunction
    {
        public FBookDeleteNote(AddressBookClient queryMaker) : base(queryMaker) 
        {
            deleteNote = new WCommand(o =>
            {
                ExecuteDeleteNote();
            });
        }

        public event ChangeNoteHandler Notify;

        Note note;
        public Note Note
        {
            get { return note; }
            set { note = value; OnPropertyChanged("Note"); }
        }

        override public HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    Notify?.Invoke();
                    
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }

        public void ExecuteDeleteNote()
        {
            Result = Task.Run(() => queryMaker.DeleteNote(Note.Id).GetAwaiter().GetResult()).Result;

        }

        public void FillNote(Note note) { Note = note; }

        WCommand deleteNote;
        public WCommand DeleteNote { get { return deleteNote; } }
    }
}
