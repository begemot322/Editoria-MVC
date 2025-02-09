using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task CreateTagAsync(Tag tag)
        {
            ArgumentNullException.ThrowIfNull(tag);

            await _tagRepository.AddAsync(tag);
        }

        public async Task<bool> DeleteTagAsync(int tagId)
        {
            try
            {
                var tag = await _tagRepository.GetAsync(t => t.TagId == tagId);

                if (tag != null)
                {
                    await _tagRepository.DeleteAsync(tag);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Tag with ID {tagId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _tagRepository.GetAsync(t => t.TagId == id);
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            ArgumentNullException.ThrowIfNull(tag);

            await _tagRepository.UpdateAsync(tag);
        }

    }
}
