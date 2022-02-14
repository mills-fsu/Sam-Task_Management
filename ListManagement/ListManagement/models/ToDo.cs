using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class ToDo
    {
        //just used auto-implemented properties for this one, will change when the class gets crazier
        //public string? Name { get; set; }
        private string? name;

        //C#-style properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool? IsCompleted { get; set; }

        public override string ToString()
        {
            return $"\tName: {Name}\n\tDescription: {Description}\n\tDeadline: {Deadline.ToShortDateString()}\n\tCompleted: {IsCompleted}\n\n";
        }
    }
}
