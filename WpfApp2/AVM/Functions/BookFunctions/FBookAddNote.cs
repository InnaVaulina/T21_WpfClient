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
using WpfApp2.Command;
using WpfApp2.Data;

namespace WpfApp2.AVM.Functions.BookFunctions
{
    public class FBookAddNote: AddressBookFunction
    {

        public FBookAddNote(AddressBookClient queryMaker) : base(queryMaker)
        {
            newNote = new Note();

            addNote = new WCommand(o =>
            {
                if (NewNote.FamilyName != "" && NewNote.Name != "" && NewNote.Tel != "")
                    ExecuteAddNote();
                else
                    MessageBox.Show("Не заполнены днные");

            });
        }

        public event ChangeNoteHandler Notify;

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

        public void ExecuteAddNote()
        {
            Result = Task.Run(() => queryMaker.AddNote(NewNote).GetAwaiter().GetResult()).Result;
        }

        WCommand addNote;
        public WCommand AddNote { get { return addNote; } }



    }
}
