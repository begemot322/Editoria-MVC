using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface INewspaperRepository
    {
        IEnumerable<Newspaper> GetAllNewspapers();
        Newspaper GetNewspaperById(int newspaperId);
        void AddNewspaper(Newspaper newspaper);
        void UpdateNewspaper(Newspaper newspaper);
        void DeleteNewspaper(int newspaperId);
        List<SelectListItem> GetEditorsList();
    }
}
