using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Common.Interfaces.Identity
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
