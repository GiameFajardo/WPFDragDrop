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
        public MainWindowViewModel()
        {
            List1 = new ObservableCollection<Person>();
            List1.Add(new Person { Name = "Giame" });
        }
        public ObservableCollection<Person> _list1;
        public ObservableCollection<Person> List1 { get { return _list1; }
            set
            {
                _list1 = value;
            }
        }
    }

    public class Person
    {

        public string Name { get; set; }
    }
}
