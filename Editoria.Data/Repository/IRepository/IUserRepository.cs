using Editoria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByEmail(string email);
        User GetById(Guid userId);
        void Update(User user);
    }
}
