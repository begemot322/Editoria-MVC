using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(User user)
        {
             _db.Users.Add(user);
             _db.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }

        public User GetById(Guid userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
