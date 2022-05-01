using ListManagement.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class AppointmentDTO: ItemDTO, INotifyPropertyChanged
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<string> Attendees { get; set; }

        public AppointmentDTO(Item i) : base(i)
        {
            var a = i as Appointment;
            if(a != null)
            {
                Start = a.Start;
                End = a.End;

                Attendees = a.Attendees;
            }
        }

        public AppointmentDTO()
        {

        }
        public AppointmentDTO(Appointment app)
        {
            Name = app.Name;
            Description = app.Description;
            Attendees = app.Attendees;
            StartDate = app.Start;
            EndDate = app.End;
            Priority = app.Priority;
        }
        public AppointmentDTO(AppointmentDTO app)
        {
            Name = app.Name;
            Description = app.Description;
            Attendees = app.Attendees;
            StartDate = app.Start;
            EndDate = app.End;
            Priority = app.Priority;
        }
        public override string ToString()
        {
            return $"{Name} {Description} From {Start} to {End}. Priority: {Priority}";
        }
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
        public TimeSpan StartTime
        {
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
        public TimeSpan EndTime
        {
            get { return endTime; }
            set
            {
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
