
using System.Windows;
using System.Windows.Controls;
using WpfApp1.AVM;
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyAVM myAVM;

        


        RadioButton selectedRadioButton;

        public MainWindow()
        {
            InitializeComponent();

            myAVM = new MyAVM();
            DataContext = myAVM;
            
        }

        private void noteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(noteList.SelectedItems!=null) 
            {
                myAVM.UserInRole.User.Note = noteList.SelectedItem as Note;
                
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            selectedRadioButton = pressed;
            myAVM.UserInRole.User.NewLetter = pressed.Content.ToString();
        }

        private void ChoseAllNotes_Click(object sender, RoutedEventArgs e)
        {
            if(selectedRadioButton != null)
                selectedRadioButton.IsChecked = false;
            clueText.Text = "";
        }

        private void chooseLetter_Click(object sender, RoutedEventArgs e)
        {
            clueText.Text = "";
        }

    }
}
