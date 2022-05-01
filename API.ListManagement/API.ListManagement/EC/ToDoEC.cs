using Api.ToDoApplication.Persistence;
using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;

namespace API.ListManagement.EC
{
    public class ToDoEC
    {
        public IEnumerable<ToDoDTO> Get()
        {
            return Filebase.Current.ToDos.Select(t => new ToDoDTO(t));
        }

        public ToDoDTO AddOrUpdate(ToDoDTO todo)
        {
            if (todo.Id <= 0)
            {
                //CREATE
                todo.Id = ItemService.Current.NextId;
                //Filebase.Current.ToDos.Add(new ToDo(todo));
                Filebase.Current.AddOrUpdate(new ToDo(todo));
            }
            else
            {
                //UPDATE
                var itemToUpdate = Filebase.Current.ToDos.FirstOrDefault(i => i.Id == todo.Id);
                if (itemToUpdate != null)
                {
                    var index = Filebase.Current.ToDos.IndexOf(itemToUpdate);
                    Filebase.Current.ToDos.Remove(itemToUpdate);
                    Filebase.Current.ToDos.Insert(index, new ToDo(todo));
                }
                else
                {
                    //CREATE -- Fall-Back
                    Filebase.Current.ToDos.Add(new ToDo(todo));
                    Filebase.Current.ToDos.Add(new ToDo(todo));
                }
            }

            return todo;
        }

        public ToDoDTO Delete(int Id)
        {
            var todoToDelete = Filebase.Current.ToDos.FirstOrDefault(i => i.Id == Id);
            if (todoToDelete != null)
            {
                Filebase.Current.DeleteTodo(Id);


                return new ToDoDTO(todoToDelete);
            }
            else
            {
                return new ToDoDTO();
            }
            
        }
    }
}
