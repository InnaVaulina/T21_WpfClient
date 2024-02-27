using WpfApp2.AVM.Functions.AdminFunctions;
using WpfApp2.AVM.Functions.BookFunctions;
using WpfApp2.Data;
using WpfApp2.Command;

namespace WpfApp2.AVM.Role
{
    public class MRAdmin: MRole
    {
        AdminWindow adminWindow;
        public MRAdmin(User user) : base(user)
        {
            logOut = new FSLogOutUser(user);
            logOut.Notify += this.LogOutUser;

            userList = new FSSelecUserstList(queryMaker1);

            regUser = new FSRegisterUser(queryMaker1);
            regUser.Notify += userList.ExecuteFillUsersList;

            deleteUser = new FSDeleteUser(queryMaker1);
            deleteUser.Notify += userList.ExecuteFillUsersList;

            changeUserRole = new FSChangeUserRole(queryMaker1);
            changeUserRole.Notify += userList.ExecuteFillUsersList;

            showUserInWork = new FSShowUser();
            showUserInWork.Notify += deleteUser.FillUserInWork;
            showUserInWork.Notify += changeUserRole.FillUserInWork;

            specialFunction = deleteUser;

            regUserTemplateShow = new WCommand(o => { SpecialFunction = regUser; });
            deleteUserTemplateShow = new WCommand(o => { SpecialFunction = deleteUser; });
            changeUserRoleTemplateShow = new WCommand(o => { SpecialFunction = changeUserRole; });

            pullUsersList = new WCommand(o => { userList.ExecuteFillUsersList(); });

            openAdminWindow = new WCommand(o =>
            {
                adminWindow = new AdminWindow(this);
                adminWindow.Show();
            });

            executeLogOut = new WCommand(o =>
            {
                logOut.User = null;
            });


            bookList = new FBookSelectList(this.queryMaker2);

            addNote = new FBookAddNote(this.queryMaker2);
            addNote.Notify += bookList.FillNoteList;

            deleteNote = new FBookDeleteNote(queryMaker2);
            deleteNote.Notify += bookList.FillNoteList;

            changeNote = new FBookChangeNote(queryMaker2);
            changeNote.Notify += bookList.FillNoteList;

            showNote = new FBookShowNote();
            showNote.Notify += deleteNote.FillNote;
            showNote.Notify += changeNote.FillNote;

            addressBookFunction = showNote;

            addNoteTemplateShow = new WCommand(o => { AddressBookFunction = addNote; });
            showNoteTemplateShow = new WCommand(o => { AddressBookFunction = showNote; });
            changeNoteTemplateShow = new WCommand(o => { AddressBookFunction = changeNote; });
            deleteNoteTemplateShow = new WCommand(o => { AddressBookFunction = deleteNote; });
        }


        FSLogOutUser logOut;

        WCommand executeLogOut;
        public WCommand ExecuteLogOut 
        { 
            get 
            { return executeLogOut; } 
        }

        WCommand openAdminWindow;
        public WCommand OpenAdminWindow { get { return openAdminWindow; } }


        FSSelecUserstList userList;
        public FSSelecUserstList UserList { get { return userList; } }

        SpeсialFunction specialFunction;

        public SpeсialFunction SpecialFunction
        {
            get { return specialFunction; }
            set { specialFunction = value; OnPropertyChanged("SpecialFunction"); }
        }

        FSRegisterUser regUser;       
        FSDeleteUser deleteUser;
        FSChangeUserRole changeUserRole;

        FSShowUser showUserInWork;
        public FSShowUser ShowUserInWork { get { return showUserInWork; } }

        WCommand pullUsersList;
        public WCommand PullUsersList { get { return pullUsersList; } }

        WCommand regUserTemplateShow;
        public WCommand RegUserTemplateShow { get { return regUserTemplateShow; } }

        WCommand deleteUserTemplateShow;
        public WCommand DeleteUserTemplateShow { get { return deleteUserTemplateShow; } }

        WCommand changeUserRoleTemplateShow;
        public WCommand ChangeUserRoleTemplateShow { get { return changeUserRoleTemplateShow; } }


              
        FBookAddNote addNote;       
        FBookDeleteNote deleteNote;       
        FBookChangeNote changeNote;


        WCommand addNoteTemplateShow;
        public WCommand AddNoteTemplateShow { get { return addNoteTemplateShow; } }

        WCommand showNoteTemplateShow;
        public WCommand ShowNoteTemplateShow { get { return showNoteTemplateShow; } }

        WCommand changeNoteTemplateShow;
        public WCommand ChangeNoteTemplateShow { get { return changeNoteTemplateShow; } }

        WCommand deleteNoteTemplateShow;
        public WCommand DeleteNoteTemplateShow { get { return deleteNoteTemplateShow; } }

       
    }
}
