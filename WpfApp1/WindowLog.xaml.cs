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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для WindowLog.xaml
    /// </summary>
    public partial class WindowLog : Window
    {
        MyAVM myAVM;
        public WindowLog(MyAVM myAVM)
        {
            InitializeComponent();
            this.myAVM = myAVM;
            DataContext = myAVM;
        }

    }
}
