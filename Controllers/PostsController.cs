using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blogify.DAL;

namespace Blogify.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogDAL _blogDAL;

        public PostsController(BlogDAL blogDAL)
        {
            _blogDAL = blogDAL;
        }

        // GET: /Posts
        public IActionResult Index()
        {
            var posts = _blogDAL.GetAllPosts();
            return View(posts);
        }

        // GET: /Posts/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Posts/Add
        [HttpPost]
        public IActionResult Add(Post post)
        {
            if (ModelState.IsValid)
            {
                _blogDAL.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: /Posts/Edit/5
        public IActionResult Edit(int id)
        {
            var post = _blogDAL.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: /Posts/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                _blogDAL.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: /Posts/Delete/5
        public IActionResult Delete(int id)
        {
            var post = _blogDAL.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: /Posts/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogDAL.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }
    }
}