using ListManagement.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class ToDo: Item
    {
        public DateTime Deadline { get; set; }
        public bool isCompleted { get; set; }
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} {Description} Completed: {IsCompleted} Priority: {Priority}";
        }
    }
}
