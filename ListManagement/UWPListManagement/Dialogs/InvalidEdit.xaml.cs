using System;
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
    public sealed partial class InvalidEdit : ContentDialog
    {
        public InvalidEdit()
        {
            this.InitializeComponent();
        }
        public InvalidEdit(string s)
        {
            this.InitializeComponent();
            if (s == "Attendees")
            {
                this.Title = "Invalid Item: Must be an Appointment";
            }
            if (s == "Attendee")
            {
                this.Title = "Invalid Item: Must select Attendee";
            }

        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
