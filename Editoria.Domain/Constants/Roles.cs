using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Domain.Constants
{
    public abstract class Roles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
        public const string Moderator = nameof(Moderator);
    }
}
