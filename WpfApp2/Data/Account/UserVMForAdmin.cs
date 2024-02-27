using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Data.Account
{
    public class UserVMForAdmin : INotifyPropertyChanged
    {
        string id;
        string name;
        string role;
        public string Id { get { return id; } set { id = value; } }
        public string UserName { get { return name; } set { name = value; } }
        public string UserRole { get { return role; } set { role = value; OnPropertyChanged("UserRole"); } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
