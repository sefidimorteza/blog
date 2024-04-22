using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using weblog.CoreLayer.DTOs.Posts;
using weblog.CoreLayer.Services.Posts;

namespace Myblog.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPostService _postService;

        public SearchModel(IPostService postService)
        {
            _postService = postService;
        }

        public PostFilterDto Filter { get; set; }
        public void OnGet(string category = null, int pageId = 1, string q = null)
        {
            Filter = _postService.GetPostsByFilter(new PostFilterParams()
            {
                CategorySlug = category,
                PageId = pageId,
                Take = 2,
                Title = q
            });
        } 
        public IActionResult OnGetPagination(string category = null, int pageId = 1, string q = null)
        {
            var model = _postService.GetPostsByFilter(new PostFilterParams()
            {
                CategorySlug = category,
                PageId = pageId,
                Take = 2,
                Title = q
            });
            return Partial("_search", model);
        }
    }
}
