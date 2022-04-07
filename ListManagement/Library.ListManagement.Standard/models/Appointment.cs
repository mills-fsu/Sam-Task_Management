using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Appointment : Item, INotifyPropertyChanged
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<string> Attendees { get; set; }

        public Appointment()
        {
            Attendees = new List<string>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} {Description} From {Start} to {End}. Priority: {Priority}";
        }

        //This business with the offsets is only a thing because of the return time of the datepicker
        //I have to use them to store bound values before converting
        //DateTime start and end are the only properties that actually matter and affect the appointment displayed

        private DateTimeOffset startDate;
        public DateTimeOffset StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                Start = startDate.Date.Add(startTime);
                NotifyPropertyChanged("StartDate");
            }
        }
        //<TimePicker Grid.Column="1" Grid.Row="1" SelectedTime="{Binding Path=StartDate, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"></TimePicker>
        //<TimePicker Grid.Column="1" Grid.Row="1" SelectedTime="{Binding Path=EndDate, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"></TimePicker>
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
                End = endDate.Date.Add(endTime);
                NotifyPropertyChanged("EndDate");
            }
        }


        private TimeSpan startTime;
        public TimeSpan StartTime {
            get { return startTime; }
            set
            {
                startTime = value;
                TimeSpan spanInDate = Start.TimeOfDay;
                Start = Start.Subtract(spanInDate);
                Start = Start.Add(startTime);
                NotifyPropertyChanged("StartTime");
                NotifyPropertyChanged("StartDate");
            }
        }

        private TimeSpan endTime;
        public TimeSpan EndTime {
            get { return endTime; }
            set {
                endTime = value;
                TimeSpan spanInDate = End.TimeOfDay;
                End = End.Subtract(spanInDate);
                End = End.Add(endTime);
                NotifyPropertyChanged("EndTime");
                NotifyPropertyChanged("EndDate");
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
