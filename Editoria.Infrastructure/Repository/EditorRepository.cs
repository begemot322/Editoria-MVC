using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Domain.Entities;
using Editoria.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Infrastructure.Repository
{
    public class EditorRepository : Repository<Editor>, IEditorRepository
    {
        public EditorRepository(ApplicationDbContext db) : base(db) { }
    }
}
