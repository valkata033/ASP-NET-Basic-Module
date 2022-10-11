using ForumAppExtended.Data;
using ForumAppExtended.Data.Entities;
using ForumAppExtended.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumAppExtended.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext context;

        public PostsController(ForumAppDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await context.Posts
                .Where(x => x.IsDeleted == false)
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                })
                .ToListAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await context.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).FirstOrDefaultAsync();

            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = await context.Posts.FindAsync(model.Id);

            if (post != null)
            {
                post.Title = model.Title;
                post.Content = model.Content;
            }

            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await context.Posts.FindAsync(id);

            if (post != null)
            {
                post.IsDeleted = true;

                await this.context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
