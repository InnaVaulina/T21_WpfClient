using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.AVM.Functions.BookFunctions;
using WpfApp2.AVM.Functions.AdminFunctions;
using WpfApp2.Command;
using WpfApp2.Data;

namespace WpfApp2.AVM.Role
{
    public class MRAnonym : MRole
    {
        LogWindow logWindow;
        RegWindow regWindow;
        public MRAnonym() : base(null)
        {
            regUser = new FSRegisterUser(this.queryMaker1);
            regUser.Notify += this.CloseRegWindow;

            logUser = new FSLogInUser(this.queryMaker1);
            logUser.Notify += this.LogNewUser;
            logUser.Notify += this.CloseLogWindow;

            bookList = new FBookSelectList(this.queryMaker2);

            showNote = new FBookShowNote();

            addressBookFunction = showNote;


            openRegWindow = new WCommand(o =>
            {
                regWindow = new RegWindow();
                regWindow.DataContext = RegUser;
                regWindow.Show();
            });

            openLogWindow = new WCommand(o =>
            {
                logWindow = new LogWindow();
                logWindow.DataContext = LogUser;
                logWindow.Show();
            });

        }

        void CloseLogWindow(User user) { logWindow.Close(); }
        void CloseRegWindow() { logWindow.Close(); }


        FSLogInUser logUser;
        public FSLogInUser LogUser { get { return logUser; } }

        FSRegisterUser regUser;
        public FSRegisterUser RegUser { get { return regUser; } }


        WCommand openRegWindow;
        public WCommand OpenRegWindow { get { return openRegWindow; } }

        WCommand openLogWindow;
        public WCommand OpenLogWindow { get { return openLogWindow; } }


    }
}
