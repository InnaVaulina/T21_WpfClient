using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.AVM.Role;
using WpfApp2.Data.Account;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        MRAdmin admin;
        public AdminWindow(MRAdmin admin)
        {
            InitializeComponent();
            this.admin = admin;
            DataContext = admin;
            admin.UserList.ExecuteFillUsersList();
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userList.SelectedItems != null)
            {               
                admin.ShowUserInWork.UserInWork = userList.SelectedItem as UserVMForAdmin;
            }
        }
    }
}
