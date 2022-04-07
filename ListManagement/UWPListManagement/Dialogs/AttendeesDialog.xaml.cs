using ListManagement.models;
using ListManagement.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPListManagement.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AttendeesDialog : Page, INotifyPropertyChanged
    {
        public Appointment selectedAppointment { get; set; }
        public ObservableCollection<string> Attendees { get; set; }

        public AttendeesDialog()
        {
            //Attendees = new ObservableCollection<String>(selectedAppointment.Attendees);
            this.InitializeComponent();
            DataContext = this;
        }
        public AttendeesDialog(Item item)
        {
            selectedAppointment = (item as Appointment);
            
            this.InitializeComponent();
        }
        public string SelectedItem
        {
            get; set;
        }
        private async void AttendeeAdd(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAttendee(selectedAppointment);
            await dialog.ShowAsync();
            Attendees = new ObservableCollection<String>(selectedAppointment.Attendees);
            NotifyPropertyChanged("Attendees");
        }
        private async void AttendeeDelete(object sender, RoutedEventArgs e)
        {
            
            if (SelectedItem != null)
            {
                var index = ItemService.Current.Items.IndexOf(selectedAppointment);
                (ItemService.Current.Items.ElementAt(index) as Appointment).Attendees.Remove(SelectedItem);
                Attendees = new ObservableCollection<String>(selectedAppointment.Attendees);
                NotifyPropertyChanged("Attendees");
            }
            else
            {
                //haven't selected an item, force them via redirect
                var invalid = new InvalidEdit("Attendee");
                await invalid.ShowAsync();
            }
        }
        private void AttendeeReturn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedAppointment = (Appointment)e.Parameter;
            Attendees = new ObservableCollection<String>(selectedAppointment.Attendees);
            NotifyPropertyChanged("Attendees");
            base.OnNavigatedTo(e);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
