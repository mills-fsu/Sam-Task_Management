using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog
    {
        private ObservableCollection<Item> _toDoCollection;
        public AppointmentDialog()
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = new Appointment();
        }
        public AppointmentDialog(Item item)
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = item;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = DataContext as Appointment;
            //item.Start = Date1;
            //item.End = Date2;
            if (_toDoCollection.Any(i => i.Id == item.Id))
            {
                var itemToUpdate = _toDoCollection.FirstOrDefault(i => i.Id == item.Id);
                var index = _toDoCollection.IndexOf(itemToUpdate);
                _toDoCollection.RemoveAt(index);
                _toDoCollection.Insert(index, item);
            }
            else
            {
                ItemService.Current.Add(DataContext as Appointment);
            }
            //NotifyPropertyChanged("StartDate");
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        /* private DateTimeOffset startDate;
        public DateTimeOffset StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                Date1 = startDate.Date;
                NotifyPropertyChanged("StartDate");
            }
        }
        private DateTimeOffset endDate;
        public DateTimeOffset EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                Date2 = endDate.Date;
                NotifyPropertyChanged("EndDate");
            }
        }

        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } */

    }
}
