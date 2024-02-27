using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.AVM.Account
{
    public class RegisterVM : INotifyPropertyChanged
    {
        string loginProp;
        string password;
        string confirmPassword;
        string userRole;

        public RegisterVM() 
        {
            loginProp = "";
            password = "";
            confirmPassword = "";
            userRole = "";
        }

        public string LoginProp 
        {
            get { return loginProp; }
            set { loginProp = value; } 
        }

        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    
}
