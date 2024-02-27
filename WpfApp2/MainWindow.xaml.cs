
using System.Windows;
using System.Windows.Controls;
using WpfApp2.AVM;
using WpfApp2.Data;


namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartAVM start;
        RadioButton selectedRadioButton;
        public MainWindow()
        {
            InitializeComponent();
            start = new StartAVM();
            DataContext = start;
        }

        private void noteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (noteList.SelectedItems != null)
            {
                start.Role.ShowNote.Note = noteList.SelectedItem as Note;
            }
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            selectedRadioButton = pressed;
            start.Role.BookList.NewLetter = pressed.Content.ToString();
        }

        private void ChoseAllNotes_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRadioButton != null)
                selectedRadioButton.IsChecked = false;
            clueText.Text = "";
        }

        private void chooseLetter_Click(object sender, RoutedEventArgs e)
        {
            clueText.Text = "";
        }
    }
}
