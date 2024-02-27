using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Client;
using WpfApp2.Data;
using WpfApp2.Command;

namespace WpfApp2.AVM.Functions.BookFunctions
{
    public class FBookSelectList: AddressBookFunction
    {
        public FBookSelectList(AddressBookClient queryMaker) : base(queryMaker)
        {
            NewLetter = "all";
            FirstFillNoteListByLetter();
            indexViewData = queryMaker.Index;


            fillNotelistByAllNotes = new WCommand(o =>
            {
                NewLetter = "all";
                FirstFillNoteListByLetter();
            });

            fillNotelistByLetter = new WCommand(o =>
            {
                if (NewLetter != "all") FirstFillNoteListByLetter();
                else MessageBox.Show("Не выбрана буква!");
            });

            fillNotelistByClue = new WCommand(o =>
            {
                if (NewClue != "") FirstFillNoteListByClue();
            });

            fillNotelistRightr = new WCommand(o => { FillNoteListRight(); });

            fillNotelistLeft = new WCommand(o => { FillNoteListLeft(); });
        }

        ObservableCollection<Note> noteList;
        public ObservableCollection<Note> NoteList
        {
            get { return noteList; }
            set { noteList = value; OnPropertyChanged("NoteList"); }
        }

        IndexViewData indexViewData;
        public IndexViewData Index { get { return indexViewData; } }


        string letter;
        public string NewLetter { get { return letter; } set { letter = value; } }

        string clue;
        public string NewClue { get { return clue; } set { clue = value; } }

        override public HttpResponseMessage Result
        {
            set
            {
                base.Result = value;
                if ((int)Result.StatusCode == 200 || (int)Result.StatusCode == 201)
                {
                    NoteList = DeserializeResponse();
                    if (NoteList.Count == 0 && indexViewData.Page > 0)
                    {
                        indexViewData.Move(indexViewData.Page - 1);
                        FillNoteList();
                    }
                   
                }
                else
                    MessageBox.Show($"Запрос не выполнен. Причина: {Result.StatusCode}");
            }
        }


        public void FirstFillNoteListByLetter()
        {
            Result = Task.Run(() => queryMaker.GetNoteListByLetter(this.letter, 0).GetAwaiter().GetResult()).Result;
            
        }


        public void FirstFillNoteListByClue()
        {
            Result = Task.Run(() => queryMaker.GetNoteListByClue(this.clue, 0).GetAwaiter().GetResult()).Result;
        }

        public void FillNoteList()
        {
            if (indexViewData.Way == ChoosingManner.letter)
            {
                Result = Task.Run(() => queryMaker.GetNoteListByLetter(indexViewData.Letter, indexViewData.Page).GetAwaiter().GetResult()).Result;
            }

            if (indexViewData.Way == ChoosingManner.clue)
            {
                Result = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page).GetAwaiter().GetResult()).Result;
            }
        }

        public void FillNoteListRight()
        {
            if (indexViewData.Way == ChoosingManner.letter)
                if (noteList.Count == 8)
                {
                    Result = Task.Run(() => queryMaker.GetNoteListByLetter(indexViewData.Letter, indexViewData.Page + 1).GetAwaiter().GetResult()).Result;
                }

            if (indexViewData.Way == ChoosingManner.clue)
                if (noteList.Count == 8)
                {
                    Result = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page + 1).GetAwaiter().GetResult()).Result;
                }
        }

        public void FillNoteListLeft()
        {
            if (indexViewData.Way == ChoosingManner.letter)
                if (indexViewData.Page > 0)
                {
                    Result = Task.Run(() => queryMaker.GetNoteListByLetter(indexViewData.Letter, indexViewData.Page - 1).GetAwaiter().GetResult()).Result;
                }
            if (indexViewData.Way == ChoosingManner.clue)
                if (indexViewData.Page > 0)
                {
                    Result = Task.Run(() => queryMaker.GetNoteListByClue(indexViewData.Clue, indexViewData.Page - 1).GetAwaiter().GetResult()).Result;
                }
        }


        public ObservableCollection<Note> DeserializeResponse()
        {
            return Task.Run(async () =>
                    {
                        Stream stream = await Result.Content.ReadAsStreamAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var list = await JsonSerializer.DeserializeAsync<List<Note>>(stream, options);
                        return new ObservableCollection<Note>(list);
                    }).Result;
        }


        WCommand fillNotelistByAllNotes;     // все записи. сначала           
        public WCommand FillNotelistByAllNotes { get { return fillNotelistByAllNotes; } }


        WCommand fillNotelistByLetter;     // все записи по букве. сначала           
        public WCommand FillNotelistByLetter { get { return fillNotelistByLetter; } }

        WCommand fillNotelistByClue;     // все записи по ключу. сначала           
        public WCommand FillNotelistByClue { get { return fillNotelistByClue; } }


        WCommand fillNotelistRightr;
        public WCommand FillNotelistRight { get { return fillNotelistRightr; } }


        WCommand fillNotelistLeft;
        public WCommand FillNotelistLeft { get { return fillNotelistLeft; } }
    }
}
