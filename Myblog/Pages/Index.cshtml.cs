using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using weblog.CoreLayer.Services.Posts;

namespace Myblog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPostService _postService;

        public IndexModel(ILogger<IndexModel> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postService.GetPopularPosts());
        }
    }
}
