﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class ChoiceDialog : ContentDialog
    {
        public ChoiceDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
        }

        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
            var dialog = new AppointmentDialog();
            try { await dialog.ShowAsync(); }
            catch (Exception e){ Console.WriteLine(e); }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

    }
}
