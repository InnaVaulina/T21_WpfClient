using System.Windows.Controls;
using System.Windows;

namespace WpfApp1.AVM.Roles.Selector
{
    public class TemplateSelectorForMenu : DataTemplateSelector
    {
        public DataTemplate RoleAnonymousMenu { get; set; }
        public DataTemplate RoleAddMakerMenu { get; set; }
        public DataTemplate RoleAdminMenu { get; set; }



        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "WpfApp1.AVM.Roles.VMRoleAnonymous": return RoleAnonymousMenu;
                    case "WpfApp1.AVM.Roles.VMRoleAddMaker": return RoleAddMakerMenu;
                    case "WpfApp1.AVM.Roles.VMRoleAdmin": return RoleAdminMenu;
                }

            }
            return RoleAnonymousMenu;
        }
    }



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
                    case "WpfApp1.AVM.Roles.Selector.ShowNote": return ShowNoteView;
                    case "WpfApp1.AVM.Roles.Selector.AddNote": return AddNoteView;
                    case "WpfApp1.AVM.Roles.Selector.DeleteNote": return DeleteNoteView;
                    case "WpfApp1.AVM.Roles.Selector.ChangeNote": return ChangeNoteView;
                }
            }
            return ShowNoteView;
        }
    }

    public class TemplateSelectorForAdminView : DataTemplateSelector
    {
        public DataTemplate ShowUserAddView { get; set; }
        public DataTemplate ShowUserDeleteView { get; set; }
        public DataTemplate ShowUserChangeRoleView { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "WpfApp1.AVM.Roles.Selector.AddUser": return ShowUserAddView;
                    case "WpfApp1.AVM.Roles.Selector.DeleteUser": return ShowUserDeleteView;
                    case "WpfApp1.AVM.Roles.Selector.ChangeUserRole": return ShowUserChangeRoleView;
                    
                }
            }
            return ShowUserDeleteView;
        }

    }
}
