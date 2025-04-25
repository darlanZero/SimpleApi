using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetAllAsync();
        Task<CommentModel?> GetByIdAsync(int id);
        Task<CommentModel> CreateAsync(CommentModel commentModel);
        Task<CommentModel?> UpdateAsync(int id, CommentModel commentModel);
        Task<CommentModel?> DeleteAsync(int id);
    }
}