using Library.ListManagement.Standard.utilities;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    [JsonConverter(typeof(ItemDTOConverter))]
    public class ItemDTO
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }

        }

        public string Description { get; set; }

        public int Id { get; set; }
        private int priority;
        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }

        }
        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
        public ItemDTO(){

        }
        public ItemDTO(Appointment i)
        {
            Name = i.Name;
            Description = i.Description;
            Id = i.Id;
            Priority = i.Priority;

        }

        public ItemDTO(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Id = i.Id;
            Priority = i.Priority;
        }
    }
}
