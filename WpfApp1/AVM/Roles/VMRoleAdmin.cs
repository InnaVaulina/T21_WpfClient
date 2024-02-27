using WpfApp1.AVM.Roles.Selector;
using WpfApp1.Command;
using WpfApp1.Data.UserModel;
using System.Windows;

namespace WpfApp1.AVM.Roles
{
    public class VMRoleAdmin : VMRole
    { 
        AdminWindow adminWindow;


        SelectedFunction specialFunction;
        public SelectedFunction SpecialFunction
        {
            get { return specialFunction; }
            set { specialFunction = value; OnPropertyChanged("SpecialFunction"); }
        }

        public VMRoleAdmin(MAuthorizedUser user, MyAVM myAVM): 
            base(myAVM) 
        {
            this.aUser = user;

            Function = new ShowNote(this);
            specialFunction = new DeleteUser(this);

            regUser = new WCommand(o =>
            {
                if (aUser.Register.LoginProp != "" && aUser.Register.Password != "" && aUser.Register.ConfirmPassword != "")
                    aUser.AddUser();
            });

            deleteUser = new WCommand(o => 
            {
                if (aUser.UserInWork.Id != "") aUser.DeleteUser();
            });

            changeUserRole = new WCommand(o =>
            {
                if (aUser.UserInWork.Id != "") aUser.ChangeUserRole();
            });



            addNote = new WCommand(o =>
            {
                if (aUser.NewNote.FamilyName != "" && aUser.NewNote.Name != "" && aUser.NewNote.Tel != "")
                    aUser.AddNote();
                else
                    MessageBox.Show("Не заполнены днные");

            });

            deleteNote = new WCommand(o => 
            {
                aUser.RemoveNote(); 
            });
        

            changeNote = new WCommand(o => 
            {
                aUser.ChangeNote(); 
            });

            openAdminWindow = new WCommand(o =>
            {
                adminWindow = new AdminWindow(this);
                aUser.FillUsersList();
                adminWindow.Show();
            });

            pullUsersList = new WCommand(o => { aUser.FillUsersList(); });

            regUserTemplateShow = new WCommand(o => { SpecialFunction = new AddUser(this); });
            deleteUserTemplateShow = new WCommand(o => { SpecialFunction = new DeleteUser(this); });
            changeUserRoleTemplateShow = new WCommand(o => { SpecialFunction = new ChangeUserRole(this); });

            addNoteTemplateShow = new WCommand(o => { Function = new AddNote(this); });
            showNoteTemplateShow = new WCommand(o => { Function = new ShowNote(this); });
            changeNoteTemplateShow = new WCommand(o => { Function = new ChangeNote(this); });
            deleteNoteTemplateShow = new WCommand(o => { Function = new DeleteNote(this); });

            copyNoteToNewNote = new WCommand(o => { aUser.NewNote.Copy(user.Note); });

 
        }


        public WCommand Logout { get { return myAVM.Logout; } }

        WCommand addNote;
        public WCommand AddNote { get { return addNote; } }


        WCommand deleteNote;
        public WCommand DeleteNote { get { return deleteNote; } }


        WCommand changeNote;
        public WCommand ChangeNote { get { return changeNote; } }


        WCommand addNoteTemplateShow;
        public WCommand AddNoteTemplateShow { get { return addNoteTemplateShow; } }

        WCommand showNoteTemplateShow;
        public WCommand ShowNoteTemplateShow { get { return showNoteTemplateShow; } }

        WCommand changeNoteTemplateShow;
        public WCommand ChangeNoteTemplateShow { get { return changeNoteTemplateShow; } }

        WCommand deleteNoteTemplateShow;
        public WCommand DeleteNoteTemplateShow { get { return deleteNoteTemplateShow; } }

        WCommand copyNoteToNewNote;
        public WCommand CopyNoteToNewNote { get { return copyNoteToNewNote; } }


        WCommand openAdminWindow;
        public WCommand OpenAdminWindow { get { return openAdminWindow; } }

        WCommand pullUsersList;
        public WCommand PullUsersList { get { return pullUsersList; } }

        WCommand regUser;
        public WCommand RegUser { get { return regUser; } }

        WCommand deleteUser;
        public WCommand DeleteUser { get { return deleteUser; } }

        WCommand changeUserRole;
        public WCommand ChangeUserRole { get { return changeUserRole; } }

        WCommand regUserTemplateShow;
        public WCommand RegUserTemplateShow { get { return regUserTemplateShow; } }

        WCommand deleteUserTemplateShow;
        public WCommand DeleteUserTemplateShow { get { return deleteUserTemplateShow; } }

        WCommand changeUserRoleTemplateShow;
        public WCommand ChangeUserRoleTemplateShow { get { return changeUserRoleTemplateShow; } }

    }
}
