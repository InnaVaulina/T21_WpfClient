
using WpfApp1.AVM.Roles.Selector;
using WpfApp1.Command;

namespace WpfApp1.AVM.Roles
{
    public class VMRoleAnonymous: VMRole
    {
        
        public VMRoleAnonymous(MyAVM myAVM) : base(myAVM) 
        {
            Function = new ShowNote(this);
        }

        public WCommand OpenRegWindow { get { return base.myAVM.OpenRegWindow; } }

        public WCommand OpenLogWindow { get { return base.myAVM.OpenLogWindow; } }

        public WCommand LogUser { get { return base.myAVM.LogUser; } }
    }
}
