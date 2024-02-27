using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WpfApp2.Selectors
{
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
                    case "WpfApp2.AVM.Functions.AdminFunctions.FSRegisterUser": 
                        return ShowUserAddView;
                    case "WpfApp2.AVM.Functions.AdminFunctions.FSDeleteUser": 
                        return ShowUserDeleteView;
                    case "WpfApp2.AVM.Functions.AdminFunctions.FSChangeUserRole": 
                        return ShowUserChangeRoleView;

                }
            }
            return ShowUserDeleteView;
        }

    }

}
