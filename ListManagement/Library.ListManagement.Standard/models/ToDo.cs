using Library.ListManagement.Standard.DTO;
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
        public ToDo()
        {

        }
        public ToDo(ToDo t)
        {
            Deadline = t.Deadline;
            IsCompleted = t.IsCompleted;
            Priority = t.Priority;
            Name = t.Name;
            Description = t.Description;

            Id = t.Id;
        }
        public ToDo(ToDoDTO dto)
        {
            Deadline = dto.Deadline;
            IsCompleted = dto.IsCompleted;
            Priority = dto.Priority;
            Name = dto.Name;
            Description = dto.Description;

            Id = dto.Id;
        }

        public override string ToString()
        {
            return $"{Name} {Description} Completed: {IsCompleted} Priority: {Priority}";
        }
    }
}
