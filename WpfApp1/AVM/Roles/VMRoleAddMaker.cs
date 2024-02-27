using System.Data;
using System.Windows;
using WpfApp1.AVM.Roles.Selector;
using WpfApp1.Command;
using WpfApp1.Data.UserModel;

namespace WpfApp1.AVM.Roles
{
    public class VMRoleAddMaker : VMRole
    {

        public VMRoleAddMaker(MAuthorizedUser user, MyAVM myAVM): 
            base(myAVM) 
        {
            aUser = user;
            Function = new ShowNote(this);

            addNote = new WCommand(o =>
            {
                if (aUser.NewNote.FamilyName != "" && aUser.NewNote.Name != "" && aUser.NewNote.Tel != "")
                    aUser.AddNote();
                else
                    MessageBox.Show("Не заполнены днные");

            });


            addNoteTemplateShow = new WCommand(o => { Function = new AddNote(this); });
            showNoteTemplateShow = new WCommand(o => { Function = new ShowNote(this); });
        }

        public WCommand Logout { get { return myAVM.Logout; } }

        WCommand addNote;
        public WCommand AddNote { get { return addNote; } }

        WCommand addNoteTemplateShow;
        public WCommand AddNoteTemplateShow { get { return addNoteTemplateShow; } }

        WCommand showNoteTemplateShow;
        public WCommand ShowNoteTemplateShow { get { return showNoteTemplateShow; } }

    }
}