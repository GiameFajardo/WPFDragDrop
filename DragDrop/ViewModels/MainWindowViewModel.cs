using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragDrop.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        private Visibility _deleteVisibility = Visibility.Collapsed;

        public Visibility DeleteVisibility 
        {
            get { return _deleteVisibility; }
            set { SetProperty(ref _deleteVisibility, value); }
        }

        private Person _personModel;

        public Person PersonModel
        {
            get { return _personModel; }
            set { SetProperty(ref _personModel, value);}
        }

        public DelegateCommand<string> AddNewCommand { get; set; }
        public DelegateCommand<Person> CopyToList2Command { get; set; }
        public DelegateCommand<Person> RemoveFromList2Command { get; set; }
        public DelegateCommand<Person> RemoveFromList2TCommand { get; set; }
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

            PersonModel = new Person();
            AddNewCommand = new DelegateCommand<string>(OnAddNew);
            CopyToList2Command = new DelegateCommand<Person>(OnCopyToList2Command);
            RemoveFromList2Command = new DelegateCommand<Person>(OnRemoveFromList2);
            RemoveFromList2TCommand = new DelegateCommand<Person>(OnRemoveFromList2T);
            List1 = new ObservableCollection<Person>();
            List2 = new ObservableCollection<Person>();
            List1.Add(new Person { Name = "Giame" });
        }

        private void OnCopyToList2Command(Person person)
        {
            List2.Add(person);
        }

        private void OnRemoveFromList2(Person person)
        {
            List2.Remove(person);
        }

        private void OnRemoveFromList2T(Person person)
        {
            List2.Remove(person);
        }

        private void OnAddNew(string Name)
        {
            List1.Add(new Person { Name = Name });
            PersonModel = new Person();
        }
    }

    public class Person
    {

        public string Name { get; set; }
    }
}
