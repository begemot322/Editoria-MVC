using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Domain.Constants
{
    public abstract class Policies
    {
        public const string AdminPolicy = nameof(AdminPolicy);
        public const string ModeratorPolicy = nameof(ModeratorPolicy);
        public const string UserPolicy = nameof(UserPolicy);
        public const string GuestPolicy = nameof(GuestPolicy);
    }
}
