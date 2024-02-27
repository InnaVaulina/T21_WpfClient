
using WpfApp2.AVM.Functions.AdminFunctions;
using WpfApp2.AVM.Functions.BookFunctions;
using WpfApp2.Data;
using WpfApp2.Command;

namespace WpfApp2.AVM.Role
{
    public class MRUser1: MRole
    {
        public MRUser1(User user): base(user) 
        {
            logOut = new FSLogOutUser(user);
            logOut.Notify += this.LogOutUser;

            executeLogOut = new WCommand(o =>
            {
                logOut.User = null;
            });

            bookList = new FBookSelectList(this.queryMaker2);

            addNote = new FBookAddNote(this.queryMaker2);
            addNote.Notify += bookList.FillNoteList;

            showNote = new FBookShowNote();

            addressBookFunction = showNote;

            addNoteTemplateShow = new WCommand(o => { AddressBookFunction = addNote; });
            showNoteTemplateShow = new WCommand(o => { AddressBookFunction = showNote; });
        }



        FSLogOutUser logOut;

        WCommand executeLogOut;
        public WCommand ExecuteLogOut
        {
            get
            { return executeLogOut; }
        }


        FBookAddNote addNote;

        WCommand addNoteTemplateShow;
        public WCommand AddNoteTemplateShow { get { return addNoteTemplateShow; } }

        WCommand showNoteTemplateShow;
        public WCommand ShowNoteTemplateShow { get { return showNoteTemplateShow; } }
    }
}
