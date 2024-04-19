using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using weblog.CoreLayer.DTOs.Comments;
using weblog.CoreLayer.Services.Comments;
using weblog.CoreLayer.Services.Posts;
using weblog.CoreLayer.Utilities;

namespace Myblog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int PostId { get; set; }
        public PostDto Post { get; set; }
        public List<CommendDto> Comments { get; set; }
        public List<PostDto> RelatedPost { get; set; }

        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            if (Post == null)
            {
                return NotFound();
            }

            RelatedPost = _postService.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);
            Comments = _commentService.GetPostComment(Post.PostId);

            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Post", new { slug });
            }

            if (ModelState.IsValid)
            {
                Post = _postService.GetPostBySlug(slug);
                RelatedPost = _postService.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);
                Comments = _commentService.GetPostComment(Post.PostId);
                return Page();
            }
            _commentService.CreateComment(new CreateCommentDto()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.GetUserId(),

            });
            return RedirectToPage("Post", new { slug });
        }
    }
}
