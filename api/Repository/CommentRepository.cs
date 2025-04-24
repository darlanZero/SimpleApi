using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<List<CommentModel>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentModel?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}