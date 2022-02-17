using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using ams_finstek_dotnet.Data.Entities;

namespace ams_finstek_dotnet.Data
{
    public class AmsRepository : IAmsRepository
    {
        private readonly AmsContext _ctx;
        public AmsRepository(AmsContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<User> GetAllUsers(int id = 0, string name = "", string active = "", string email = "", string location = "")
        {
            Trace.WriteLine("Name=" + name);
            string id_string = "";
            if (id == 0)
            {
                id_string = "";
            }
            else
            {
                id_string = id.ToString();
            }

            if (name == null)
            {
                name = "";
            }

            if (active == null)
            {
                active = "";
            }
            if (email == null)
            {
                email = "";
            }
            if (location == null)
            {
                location = "";
            }

            if (id > 0)
            {
                return _ctx.Users
                     .Where(p => p.Id.ToString() == id_string && p.Name.Contains(name) && p.Email.Contains(email) && p.Location.Contains(location) && p.isActive.ToString().Contains(active))
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            else
            {
                return _ctx.Users
                     .Where(p => p.Name.Contains(name) && p.Email.Contains(email) && p.Location.Contains(location) && p.isActive.ToString().Contains(active))
                    .OrderBy(p => p.Id)
                    .ToList();
            }

        }

        public List<dynamic> GetUserAccesses(int id = 0)
        {
            var query = from a in _ctx.Accesses
                        join u in _ctx.Users on a.UserId equals u.Id
                        join s in _ctx.Services on a.ServiceId equals s.Id
                        where u.Id == id && a.HasAccess.ToString() == "true"
                        select new { Id = a.Id, UserServiceLogin = a.UserServiceLogin, ServiceName = s.Name };

            var result = new List<dynamic>(query.ToList());

            return result;

        }



        public IEnumerable<Service> GetAllServices()
        {
            return _ctx.Services
                .OrderBy(p => p.Name)
                .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void EditUser(int id,User user)
        {
                var user_to_edit = _ctx.Users.SingleOrDefault(u => u.Id == id);
                _ctx.Users.Attach(user_to_edit);

                //setting vars
                if (user.Email == null) user.Email = user_to_edit.Email;
                if (user.Location == null) user.Location = user_to_edit.Location;
                if (user.Name == null) user.Name = user_to_edit.Name;


                //changing user info
                user_to_edit.Email = user.Email;
                user_to_edit.Name = user.Name;
                user_to_edit.Location = user.Location;
                user_to_edit.isActive = user.isActive;
            

        }

        public void DeleteUser(int id)
        {
            User user = new User() {Id = id};
            _ctx.Users.Attach(user);
            _ctx.Remove(user);
        }

        

        public User GetUserById(int id)
        {
            var users = _ctx.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            if (users != null)
            {
                return users;

            }
            else
            {
                return null;
            }
            
        }
    }
}
