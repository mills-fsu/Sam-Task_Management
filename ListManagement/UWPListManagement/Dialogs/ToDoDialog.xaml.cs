using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {
        private MainViewModel _mvm;
        private ObservableCollection<Item> _toDoCollection;
        public ToDoDialog()
        {
            this.InitializeComponent();
            //_toDoCollection = ItemService.Current.Items;

            DataContext = new ToDoDTO(new ToDo());
        }

        public ToDoDialog(MainViewModel mvm)
        {
            this.InitializeComponent();
            _mvm = mvm;

            if (mvm != null && _mvm.SelectedItem != null)
            {
                var replacement = new ToDo();
                replacement.Id = _mvm.SelectedItem.Id;
                DataContext = new ToDoDTO(replacement);//mvm.SelectedItem;
            }
            else
            {
                DataContext = new ToDoDTO(new ToDo());
            }
            //_toDoCollection = ItemService.Current.Items;
            //DataContext = item;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = new ItemViewModel(DataContext as ToDoDTO);

            _mvm.Add(item);

            //var item = DataContext as ToDo;
            //if(_toDoCollection.Any(i => i.Id == item.Id))
            //{
            //    var itemToUpdate = _toDoCollection.FirstOrDefault(i => i.Id == item.Id);
            //    var index = _toDoCollection.IndexOf(itemToUpdate);
            //    _toDoCollection.RemoveAt(index);
            //    _toDoCollection.Insert(index, item);
            //} else
            //{
            //    ItemService.Current.Add(DataContext as ToDo);
            //}

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        /*private void HandleCheckTrue(object sender, RoutedEventArgs e)
        {
            var item = DataContext as ToDo;
            CheckBox cb = sender as CheckBox;
           /if (cb.Name == "True") item.IsCompleted = true;
            else ;
        }

        private void HandleCheckFalse(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name == "False") ;
            else ;
        }*/

    }
}
