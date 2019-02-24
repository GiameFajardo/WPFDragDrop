using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragDrop.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        private Person _personModel;

        public Person PersonModel
        {
            get { return _personModel; }
            set { SetProperty(ref _personModel, value);}
        }

        public DelegateCommand<string> AddNewCommand { get; set; }
        public ObservableCollection<Person> _list1;
        public ObservableCollection<Person> List1
        {
            get { return _list1; }
            set
            {
                _list1 = value;
            }
        }
        public ObservableCollection<Person> _list2;
        public ObservableCollection<Person> List2
        {
            get { return _list2; }
            set
            {
                _list2 = value;
            }
        }
        public MainWindowViewModel()
        {
            PersonModel = new Person { Name = "New" };
            AddNewCommand = new DelegateCommand<string>(OnAddNew);
            List1 = new ObservableCollection<Person>();
            List1.Add(new Person { Name = "Giame" });
        }

        private void OnAddNew(string Name)
        {
            List1.Add(new Person { Name = Name });
        }
    }

    public class Person
    {

        public string Name { get; set; }
    }
}
