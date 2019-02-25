using DragDrop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDrop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public Point startPoint { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            vm = (MainWindowViewModel)this.DataContext;
        }

        private void lvDrag_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Store the mouse position
            startPoint = e.GetPosition(null);
        }
        private void lvDrag_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Person person = (Person)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", person);
                    System.Windows.DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);

                }
            }
        }
        private void lvDrag_MouseMove(object sender, MouseEventArgs e)
        {
            //// Get the current mouse position
            //Point mousePos = e.GetPosition(null);
            //Vector diff = startPoint - mousePos;

            //if (e.LeftButton == MouseButtonState.Pressed &&
            //    Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            //    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            //{
            //    // Get the dragged ListViewItem
            //    ListView listView = sender as ListView;
            //    ListViewItem listViewItem =
            //        FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
            //    if (listViewItem != null)
            //    {
            //        // Find the data behind the ListViewItem
            //        Person person = (Person)listView.ItemContainerGenerator.
            //            ItemFromContainer(listViewItem);

            //        // Initialize the drag & drop operation
            //        DataObject dragData = new DataObject("myFormat", person);
            //        System.Windows.DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);

            //    }
            //}

        }
        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }


        private void lvDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            if (!e.Data.GetDataPresent("DeleteFormat"))
            {
                vm.DeleteVisibility = Visibility.Collapsed;
                ((ListView)sender).BorderThickness = new Thickness(10);
                ((ListView)sender).Padding = new Thickness(0);
            }
            ((ListView)sender).BorderBrush = new SolidColorBrush(Colors.MistyRose);
            //((ListView)sender).BorderThickness = new Thickness(10);
            //((ListView)sender).Padding = new Thickness(0);
        }
        private void lvDrop_Drop(object sender, DragEventArgs e)
        {

            var vm = (MainWindowViewModel)this.DataContext;
            if (e.Data.GetDataPresent("myFormat"))
            {
                Person person = e.Data.GetData("myFormat") as Person;
                //ListView listView = sender as ListView;
                //listView.Items.Add(person);
                vm.CopyToList2Command.Execute(person);
                ((ListView)sender).BorderThickness = new Thickness(0);
                ((ListView)sender).Padding = new Thickness(10);
            }
            if (e.Data.GetDataPresent("DeleteFormat"))
            {

                ((ListView)sender).BorderThickness = new Thickness(0);
                ((ListView)sender).Padding = new Thickness(10);
                vm.DeleteVisibility = Visibility.Collapsed;
            }
        }
        private void lvDrop_DragLeave(object sender, DragEventArgs e)
        {
            ((ListView)sender).BorderBrush = new SolidColorBrush(Colors.LightBlue);

            //((ListView)sender).BorderThickness = new Thickness(0);
            //((ListView)sender).Padding = new Thickness(10);
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent("DeleteFormat"))
            {
                Person person = e.Data.GetData("DeleteFormat") as Person;
                vm.RemoveFromList2Command.Execute(person);
                vm.DeleteVisibility = Visibility.Collapsed;
                
            }

        }

        private void lvDrop_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(null);
        }

        private void lvDrop_MouseMove(object sender, MouseEventArgs e)
        {
            //vm.DeleteVisibility = Visibility.Visible;
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Person person = (Person)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("DeleteFormat", person);
                    System.Windows.DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);

                }
            }
        }

        private void lvDrop_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lvDrop_DragOver(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent("DeleteFormat"))
            {
                vm.DeleteVisibility = Visibility.Visible;
            }
        }

        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
          

            fp_Move_Control(sender, e);
        }
        private void fp_Move_Control(object sender, MouseEventArgs e)
        {
            //----------------< fp_Move_Control() >---------------- 
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (true/*Mouse.OverrideCursor == Cursors.Hand*/) //nur bewegen, wenn Cursor auf Bewegung steht 
                {
                    ListViewItem objTextbox = (ListViewItem)sender;
                    if (objTextbox != null)
                    {
                        //----< Move Control >---- 
                        Point mousePoint = e.GetPosition(this);

                        //< Vertical > 
                        int posY = (int)mousePoint.Y;
                        int actHeight = (int)Application.Current.MainWindow.Height;
                        int margin_Bottom = actHeight - (posY + (int)objTextbox.ActualHeight + (int)SystemParameters.CaptionHeight + (int)SystemParameters.BorderWidth + 4);
                        //</ Vertical > 

                        //< Horizontal > 
                        int posX = (int)mousePoint.X;
                        int actWidth = (int)Application.Current.MainWindow.Width;
                        int margin_Right = actWidth - (posX + (int)objTextbox.ActualWidth + (int)SystemParameters.BorderWidth);
                        //</ Horizontal > 

                        //< Objekt bewegen > 
                        objTextbox.Margin = new Thickness(posX, posY, margin_Right, margin_Bottom);
                        //</ Objekt bewegen > 

                       // ctlStatus.Text = "Top=" + posY + " margin_bottom=" + margin_Bottom + " WinHeigth=" + actHeight + Environment.NewLine + " Left=" + posX + " margin_Right=" + margin_Right + "WinWidth=" + actWidth;

                        //< Cursor anpassen > 
                        Mouse.OverrideCursor = Cursors.Hand;
                        //</ Cursor anpassen > 
                        //----</ Move Control >---- 
                    }
                }
            }
            else
            {
                //< Cursor reset > 
                Mouse.OverrideCursor = Cursors.Arrow;
                //</ Cursor reset > 
            }
            //----------------< fp_Move_Control() >---------------- 

        }
    }
}
