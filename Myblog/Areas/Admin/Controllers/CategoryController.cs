using Microsoft.AspNetCore.Mvc;
using Myblog.Areas.Admin.Models.Categories;
using weblog.CoreLayer.DTOs.Category;
using weblog.CoreLayer.Services.Categories;
using weblog.CoreLayer.Utilities;
using weblog.DataLayer.Entities;

namespace Myblog.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategory());
        }

        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel create)
        {
            create.ParentId = parentId;
            var result = _categoryService.CreateCategory(create.MapCategoryDto());
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(create.Slug), result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
                return RedirectToAction("Index");

            var modelEdit = new EditCategoryViewModel()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                Title = category.Title
            };
            return View(modelEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editModel)
        {
            var result = _categoryService.EditCategory(new EditCategoryDto()
            {
                Slug = editModel.Slug,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription,
                Title = editModel.Title,
                Id = id,
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editModel.Slug), result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        //انتخاب زیر گروه
        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categoryService.GetChildCategories(parentId);

            return new JsonResult(category);
        }
    }
}
