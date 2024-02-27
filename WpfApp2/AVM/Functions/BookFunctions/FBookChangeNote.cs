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
    public class FBookChangeNote: AddressBookFunction
    {
        public FBookChangeNote(AddressBookClient queryMaker) : base(queryMaker) 
        {            
            newNote = new Note();

            changeNote = new WCommand(o =>
            {
                ExecuteChangeNote();
            });

            copyNoteToNewNote = new WCommand(o => { NewNote.Copy(Note); });
        }

        public event ChangeNoteHandler Notify;

        Note note;
        public Note Note
        {
            get { return note; }
            set { note = value; OnPropertyChanged("Note"); }
        }

        Note newNote;
        public Note NewNote
        {
            get { return newNote; }
            set { newNote = value; OnPropertyChanged("NewNote"); }
        }

        override public HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    NewNote.Clear();
                    Notify?.Invoke();
                    
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }

        public void ExecuteChangeNote()
        {
            Result = Task.Run(() => queryMaker.ChangeNote(Note.Id, NewNote).GetAwaiter().GetResult()).Result;

        }

        public void FillNote(Note note) { Note = note; }

        WCommand changeNote;
        public WCommand ChangeNote { get { return changeNote; } }

        WCommand copyNoteToNewNote;
        public WCommand CopyNoteToNewNote { get { return copyNoteToNewNote; } }
    }
}
