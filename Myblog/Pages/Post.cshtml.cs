using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using weblog.CoreLayer.Services.Posts;

namespace Myblog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;

        public PostModel(IPostService postService)
        {
            _postService = postService;
        }

        public PostDto Post { get; set; }
        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
