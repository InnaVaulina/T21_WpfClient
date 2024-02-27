using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Data
{

    public enum ChoosingManner { letter, clue };
    public class IndexViewData : INotifyPropertyChanged
    {
        string letter;
        public string Letter { get { return letter; } }

        string clue;
        public string Clue { get { return clue; } }

        ChoosingManner way;
        public ChoosingManner Way { get { return way; } }

        int page;
        public int Page 
        { 
            get { return page; } 
            set { page = value; OnPropertyChanged("Page"); } 
        }

        private readonly string[] letters = {"А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К",
                "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Э", "Ю", "Я"};

        public IndexViewData()
        {
            letter = "all";
            way = ChoosingManner.letter;
            page = 0;
        }

        public void ChooseLetter(string newLetter)
        {
            Page = 0;
            way = ChoosingManner.letter;
            switch (newLetter)
            {
                case "all":
                    this.letter = newLetter;
                    break;
                default:
                    this.letter = "all";
                    foreach (string item in letters)
                    {
                        if (string.Compare(item, newLetter) == 0)
                        {
                            this.letter = newLetter;
                        }
                    }
                    break;
            }
        }

        public void ChooseClue(string newclue) 
        {
            Page = 0;
            way = ChoosingManner.clue;
            clue = newclue;
        }


        public void Move(int trandPage)
        {
            if (trandPage >= 0) Page = trandPage;
            else Page = 0;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
