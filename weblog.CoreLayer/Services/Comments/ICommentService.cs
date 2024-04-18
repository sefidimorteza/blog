using Microsoft.EntityFrameworkCore;
using weblog.CoreLayer.DTOs.Comments;
using weblog.CoreLayer.Utilities;
using weblog.DataLayer.Context;
using weblog.DataLayer.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace weblog.CoreLayer.Services.Comments;public interface ICommentService
{
    OperationResult CreateComment(CreateCommentDto command);
    List<CommendDto> GetPostComment(int Id);
}
public class CommentService : ICommentService{
    private readonly BlogContext _Context;

    public CommentService(BlogContext context)
    {
        _Context = context;
    }


    public OperationResult CreateComment(CreateCommentDto command)
    {
        var comment = new PostComment()
        {
            UserId = command.UserId,
            PostId = command.PostId,
            Text = command.Text,
        };
        _Context.PostComments.Add(comment);
        _Context.SaveChanges();
        return OperationResult.Success();
    }

    public List<CommendDto> GetPostComment(int Id)
    {
        return _Context.PostComments
            .Include(user => user.User)
            .Where(comment => comment.PostId == Id)
            .Select(comments => new CommendDto()
            {
                Id = comments.Id,
                UserFullName = comments.User.FullName,
                PostId = comments.PostId,
                Text = comments.Text,
                CreationDateTime = comments.CreationDate
            }).ToList();
    }
}