using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WpfApp1.Client;

namespace WpfApp1.Data.UserModel
{
    public class MUser: INotifyPropertyChanged
    {
        protected AddressBookClient queryMaker;

        public MUser()
        {
            indexViewData = new IndexViewData();
            letter = "all";
            clue = "";
            queryMaker = new AddressBookClient(indexViewData);
            noteList = new ObservableCollection<Note>();
            FirstFillNoteListByLetter();
            note = new Note();
        }


        Note note;
        public Note Note
        {
            get { return note; }
            set { note = value; OnPropertyChanged("Note"); }
        }

        ObservableCollection<Note> noteList;
        public ObservableCollection<Note> NoteList
        {
            get { return noteList; }
            set { noteList = value; OnPropertyChanged("NoteList"); }
        }


        IndexViewData indexViewData;
        public IndexViewData IndexViewData { get { return indexViewData; } }


        string letter;
        public string NewLetter { get { return letter; } set { letter = value; } }

        string clue;
        public string NewClue { get { return clue; } set { clue = value; } }




        public void FirstFillNoteListByLetter() 
        {
            var list = Task.Run(() => queryMaker.GetNoteList(this.letter, 0).GetAwaiter().GetResult());
            NoteList = new ObservableCollection<Note>(list.Result);
        }


        public void FirstFillNoteListByClue()
        {
            var list = Task.Run(() => queryMaker.GetNoteListByClue(this.clue, 0).GetAwaiter().GetResult());
            NoteList = new ObservableCollection<Note>(list.Result);
        }

        public void FillNoteListRight()
        {
            if(indexViewData.Way == ChoosingManner.letter) 
                if(noteList.Count == 8) 
                {
                    var list = Task.Run(() => queryMaker.GetNoteList(indexViewData.Letter, indexViewData.Page + 1).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }

            if (indexViewData.Way == ChoosingManner.clue)
                if (noteList.Count == 8)
                {
                    var list = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page + 1).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }
        }

        public void FillNoteListLeft()
        {
            if (indexViewData.Way == ChoosingManner.letter)
                if (indexViewData.Page > 0) 
                {
                    var list = Task.Run(() => queryMaker.GetNoteList(indexViewData.Letter, indexViewData.Page - 1).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }
            if (indexViewData.Way == ChoosingManner.clue)
                if (indexViewData.Page > 0)
                {
                    var list = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page - 1).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }
        }


        public void FillNoteList()
        {
            if (indexViewData.Way == ChoosingManner.letter)
                {
                    var list = Task.Run(() => queryMaker.GetNoteList(indexViewData.Letter, indexViewData.Page).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }

            if (indexViewData.Way == ChoosingManner.clue)
                {
                    var list = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page).GetAwaiter().GetResult());
                    NoteList = new ObservableCollection<Note>(list.Result);
                }
                    
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    
}
