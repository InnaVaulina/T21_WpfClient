
using System.Windows.Controls;
using System.Windows;

namespace WpfApp2.Selectors
{
    public class TemplateSelectorForMenu : DataTemplateSelector
    {
        public DataTemplate RoleAnonymousMenu { get; set; }
        public DataTemplate RoleUser1Menu { get; set; }
        public DataTemplate RoleAdminMenu { get; set; }



        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "WpfApp2.AVM.Role.MRAnonym": return RoleAnonymousMenu;
                    case "WpfApp2.AVM.Role.MRUser1": return RoleUser1Menu;
                    case "WpfApp2.AVM.Role.MRAdmin": return RoleAdminMenu;
                }

            }
            return RoleAnonymousMenu;
        }
    }
}
