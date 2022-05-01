using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _appointmentRoot;
        private string _todoRoot;
        private static Filebase? _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "C:\\sam_temp";
            _appointmentRoot = $"{_root}\\Appointments";
            _todoRoot = $"{_root}\\ToDos";

            if(!Directory.Exists(_root))
            {
                Directory.CreateDirectory(_root);
                if(!Directory.Exists(_appointmentRoot))
                {
                    Directory.CreateDirectory(_appointmentRoot);
                }

                if(!Directory.Exists(_todoRoot))
                {
                    Directory.CreateDirectory(_todoRoot);
                }
            }


        }

        public Item AddOrUpdate(Item item)
        {

            //go to the right place
            string path;
            if (item is ToDo)
            {
                path = $"{_todoRoot}\\{item.Id}.json";
            } else if(item is Appointment)
            {
                path = $"{_appointmentRoot}\\{item.Id}.json";
            } else
            {
                throw new Exception("Polymorphic binding failed!!!!");
            }

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }

        public List<ToDo> ToDos
        {
            get
            {
                var root = new DirectoryInfo(_todoRoot);
                var _todos = new List<ToDo>();
                try
                {
                    foreach (var todoFile in root.GetFiles())
                    {
                        var todo = JsonConvert.DeserializeObject<ToDoDTO>(File.ReadAllText(todoFile.FullName));
                        _todos.Add(new ToDo(todo));
                    }
                }
                catch
                {

                }
                return _todos;
            }
        }


        public ToDo GetById(int id)
        {
            //return ToDos?.FirstOrDefault(t => t.Id == id) ?? new ToDo();

            var fileName = $"{_todoRoot}\\{id}.json";
            var dto = JsonConvert.DeserializeObject<ToDoDTO>(File.ReadAllText(fileName)) ?? null;
            return new ToDo(dto);
        }

        public List<Appointment> Appointments
        {
            get
            {
                var root = new DirectoryInfo(_appointmentRoot);
                var _apps = new List<Appointment>();
                foreach (var appFile in root.GetFiles())
                {
                    var dto = JsonConvert.DeserializeObject<AppointmentDTO>(File.ReadAllText(appFile.FullName));
                    var app = new Appointment(dto);
                    _apps.Add(app);
                }
                return _apps;
            }
        }

        public bool DeleteTodo(int Id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.

            var path = $"{_todoRoot}\\{Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }


            return true;
        }
        public bool DeleteApp(int Id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.

            var path = $"{_appointmentRoot}\\{Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }


            return true;
        }
    }

}
