using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this CommentModel commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static CommentModel ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new CommentModel
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static CommentModel ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
        {
            return new CommentModel
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}