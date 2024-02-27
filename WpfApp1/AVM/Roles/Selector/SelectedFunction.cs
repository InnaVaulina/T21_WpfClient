
namespace WpfApp1.AVM.Roles.Selector
{


    public class SelectedFunction
    {
        VMRole role;
        public VMRole Role { get {return role;} }

        public SelectedFunction(VMRole role)
        {
            this.role = role;
        }


    }

    public class ShowNote : SelectedFunction
    {
        public ShowNote(VMRole role) : base(role) { }

    }


    public class AddNote : SelectedFunction
    {
        public AddNote(VMRole role) : base(role){ }
    }

    public class DeleteNote : SelectedFunction
    {
        public DeleteNote(VMRole role) : base(role) { }
    }

    public class ChangeNote : SelectedFunction
    {
        public ChangeNote (VMRole role) : base(role) { }
    }

    public class AddUser : SelectedFunction
    {
        public AddUser(VMRole role) : base(role) { }
    }

    public class DeleteUser : SelectedFunction
    {
        public DeleteUser(VMRole role) : base(role) { }
    }

    public class ChangeUserRole : SelectedFunction
    {
        public ChangeUserRole(VMRole role) : base(role) { }
    }

}
