using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp2.Data
{
    public class Note: INotifyPropertyChanged
    {
        int id;
        public int Id 
        { 
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
            
        }

        string familyName;
        public string FamilyName
        {
            get { return familyName; }
            set { familyName = value; OnPropertyChanged("FamilyName"); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        string patronymicName;
        public string PatronymicName
        {
            get { return patronymicName; }
            set { patronymicName = value; OnPropertyChanged("PatronymicName"); }
        }

        string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; OnPropertyChanged("Tel"); }
        }

        string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }

        string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }


        public Note()
        {
            Id = 0;
            FamilyName = "";
            Name = "";
            PatronymicName = "";
            Tel = "";
            Address = "";
            Description = "";
        }

        public void Clear() 
        {
            Id = 0;
            FamilyName = "";
            Name = "";
            PatronymicName = "";
            Tel = "";
            Address = "";
            Description = "";
        }

        public void Copy(Note note) 
        {
            Id = note.Id;
            FamilyName = note.FamilyName;
            Name = note.Name;
            PatronymicName = note.PatronymicName;
            Tel = note.Tel;
            Address = note.Address;
            Description = note.Description;
        }
       



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
