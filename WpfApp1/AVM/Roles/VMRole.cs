
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.AVM.Roles.Selector;
using WpfApp1.Command;
using WpfApp1.Data.UserModel;

namespace WpfApp1.AVM.Roles
{
    public class VMRole: INotifyPropertyChanged
    {
        protected MyAVM myAVM;

        public VMRole(MyAVM myAVM)
        {
            this.user = new MUser();
            this.myAVM = myAVM;


            fillNotelistByAllNotes = new WCommand(o =>
            {
                user.NewLetter = "all";
                user.FirstFillNoteListByLetter();
            });

            fillNotelistByLetter = new WCommand(o =>
            {
                if (user.NewLetter != "all") user.FirstFillNoteListByLetter();
                else MessageBox.Show("Не выбрана буква!");
            });

            fillNotelistByClue = new WCommand(o =>
            {
                if (user.NewClue != "") user.FirstFillNoteListByClue();
            });

            fillNotelistRightr = new WCommand(o => { user.FillNoteListRight(); });

            fillNotelistLeft = new WCommand(o => { user.FillNoteListLeft(); });

        }
  

        MUser user;
        public MUser User { get { return user; } }

        protected MAuthorizedUser aUser;
        public MAuthorizedUser AuthorizedUser { get { return aUser; } }

        SelectedFunction function;
        public SelectedFunction Function
        {
            get { return function; }
            set { function = value; OnPropertyChanged("Function"); }
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



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
