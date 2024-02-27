
using System.Windows.Controls;
using System.Windows;

namespace WpfApp2.Selectors
{
    public class TemplateSelectorForNoteView : DataTemplateSelector
    {
        public DataTemplate ShowNoteView { get; set; }
        public DataTemplate AddNoteView { get; set; }
        public DataTemplate DeleteNoteView { get; set; }
        public DataTemplate ChangeNoteView { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "WpfApp2.AVM.Functions.BookFunctions.FBookShowNote": 
                        return ShowNoteView;
                    case "WpfApp2.AVM.Functions.BookFunctions.FBookAddNote": 
                        return AddNoteView;
                    case "WpfApp2.AVM.Functions.BookFunctions.FBookDeleteNote": 
                        return DeleteNoteView;
                    case "WpfApp2.AVM.Functions.BookFunctions.FBookChangeNote": 
                        return ChangeNoteView;
                }
            }
            return ShowNoteView;
        }
    }
}
