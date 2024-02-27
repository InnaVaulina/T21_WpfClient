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
using WpfApp1.AVM;
using WpfApp1.AVM.Account;
using WpfApp1.AVM.Roles;
using WpfApp1.Data.UserModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        VMRoleAdmin admin;
        public AdminWindow(VMRoleAdmin admin)
        {
            InitializeComponent();
            this.admin = admin;
            DataContext = admin;
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userList.SelectedItems != null)
            {
                
                admin.AuthorizedUser.UserInWork = userList.SelectedItem as UserVMForAdmin;

            }
        }
    }
}
