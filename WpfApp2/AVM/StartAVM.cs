using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp2.AVM.Role;
using WpfApp2.Data;

namespace WpfApp2.AVM
{
    public class StartAVM: INotifyPropertyChanged
    {

        public StartAVM() 
        {
            SetAnonym();
        }

        MRole role;
        public MRole Role 
        { 
            get { return role; }
            set {  role = value; OnPropertyChanged("Role"); }
        }


        string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        public void SetAnonym()
        {
            Role = new MRAnonym();
            Role.Notify_UserIn += SetUser;
            Message = "Вы вошли как анонимный пользователь. Вы можете просмтривать записную книжку.";
        }

        public void SetUser(User user) 
        {
            switch (user.UserRole) 
            {
                case "admin":
                    Role = new MRAdmin(user);
                    Role.Notify_UserOut += SetAnonym;
                    Message = $"{user.UserName} в системе. У вас доступ администратора.";
                    break;
                case "user":
                    Role = new MRUser1(user);
                    Role.Notify_UserOut += SetAnonym;
                    Message = $"{user.UserName} в системе. Вы можете просмтривать записную книжку и добавлять записи.";
                    break;
                default:
                    SetAnonym();
                    break;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
