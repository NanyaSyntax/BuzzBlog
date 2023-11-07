using BuzzBlog.Models.Domain;
using BuzzBlog.Models.ViewModels;
using BuzzBlog.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuzzBlog.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostrepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostrepository blogPostrepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostrepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
            
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return View();
        }
    }
}
