using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
namespace API.ListManagement.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return FakeDatabase.Appointments.Select(t => new AppointmentDTO(t));
        }
        public AppointmentDTO AddOrUpdate(AppointmentDTO app)
        {
            if (app.Id <= 0)
            {
                //CREATE
                app.Id = ItemService.Current.NextId;
                FakeDatabase.Appointments.Add(new Appointment(app));
            }
            else
            {
                //UPDATE
                var itemToUpdate = FakeDatabase.Appointments.FirstOrDefault(i => i.Id == app.Id);
                if (itemToUpdate != null)
                {
                    var index = FakeDatabase.Appointments.IndexOf(itemToUpdate);
                    FakeDatabase.Appointments.Remove(itemToUpdate);
                    FakeDatabase.Appointments.Insert(index, new Appointment(app));
                }
                else
                {
                    //CREATE -- Fall-Back
                    FakeDatabase.Appointments.Add(new Appointment(app));
                }
            }

            return app;
        }

        public AppointmentDTO Delete(int id)
        {
            var todoToDelete = FakeDatabase.Appointments.FirstOrDefault(i => i.Id == id);
            if (todoToDelete != null)
            {
                FakeDatabase.Appointments.Remove(todoToDelete);
            }

            return new AppointmentDTO(todoToDelete);
        }
    }
}
