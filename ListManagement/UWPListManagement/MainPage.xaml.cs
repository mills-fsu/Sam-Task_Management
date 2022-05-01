using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using UWPListManagement.Dialogs;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\UWPSaveData.json";
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel(persistencePath);

           /* if (File.Exists(persistencePath))
            {
                DataContext = Load(persistencePath);
            }
            else
            {
                DataContext = new MainViewModel(persistencePath);
            }*/

        }

        private async void AddClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ChoiceDialog(DataContext as MainViewModel);
            await dialog.ShowAsync();
        }

        private async void EditClick(object sender, RoutedEventArgs e)
        {
            var item = ((DataContext as MainViewModel).SelectedItem);
            if (item != null)
            {
                if (!item.IsTodo){
                    //is an appointment
                    var dialog = new AppointmentDialog((DataContext as MainViewModel));
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new ToDoDialog((DataContext as MainViewModel));
                    await dialog.ShowAsync();
                }
            }
            else
            {
                //haven't selected an item, force them via redirect
                var invalid = new InvalidEdit();
                await invalid.ShowAsync();
            }
            
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var item = ((DataContext as MainViewModel).SelectedItem);
            if (item.IsTodo)
            {
                (DataContext as MainViewModel).Delete(item.Id);
            }
            else
            {
                (DataContext as MainViewModel).DeleteApp(item.Id);
            }
            //var deleteItem = (DataContext as MainViewModel).SelectedItem;
            //var items = ((DataContext as MainViewModel).Items);
            //if (deleteItem != null)
            //{
            //    var index = items.IndexOf(deleteItem);
            //    items.RemoveAt(index);
            //}
            //else
            //{
            //    //haven't selected an item, force them via redirect
            //    var invalid = new InvalidEdit();
            //    await invalid.ShowAsync();
            //}

        }
        private async void AttendeeClick(object sender, RoutedEventArgs e)
        {
            var item = ((DataContext as MainViewModel).SelectedItem);
            if (item != null)
            {
                if (new Appointment(item.BoundAppointment) != null)
                {
                    //is an appointment
                    //var dialog = new AttendeesDialog((DataContext as MainViewModel).SelectedItem);
                    this.Frame.Navigate(typeof(AttendeesDialog), (DataContext as MainViewModel));
                }
                else
                {
                    var invalid = new InvalidEdit("Attendees");
                    await invalid.ShowAsync();
                }
            }
            else
            {
                //haven't selected an item, force them via redirect
                var invalid = new InvalidEdit();
                await invalid.ShowAsync();
            }

        }
        private void SortClick(object sender, RoutedEventArgs e)
        {
            var items = ((DataContext as MainViewModel).Items);
            var list_items = ((DataContext as MainViewModel).Items).OrderBy(item => item.Priority).ToList();
            items.Clear();
            foreach(var i in list_items)
            {
                items.Add(i);
            }
        }
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            //var Query = ((DataContext as MainViewModel).Query);
            //if (Query != null && Query != "" ){
            //    (DataContext as MainViewModel).GetFilteredItems(Query);
            //    SearchResults.Visibility = Visibility.Visible;
            //    AllResults.Visibility = Visibility.Collapsed;
            //}
            //else
            //{

            //    SearchResults.Visibility = Visibility.Collapsed;
            //    AllResults.Visibility = Visibility.Visible;
            //}
        }
        private void IncompleteClick(object sender, RoutedEventArgs e)
        {
            //if (IncompleteResults.Visibility == Visibility.Collapsed)
            //{
            //    (DataContext as MainViewModel).GetIncompleteItems();
            //    IncompleteResults.Visibility = Visibility.Visible;
            //    AllResults.Visibility = Visibility.Collapsed;
            //    SearchResults.Visibility = Visibility.Collapsed;
            //}
            //else
            //{

            //    IncompleteResults.Visibility = Visibility.Collapsed;
            //    AllResults.Visibility = Visibility.Visible;
            //    SearchResults.Visibility = Visibility.Visible;
            //}
        }
        private void LoadClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Load(persistencePath);
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Save();
        }
    }
}
