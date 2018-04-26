using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext database;

        public ArticleController(ApplicationDbContext context)
        {
            database = context;
        }

        // GET: Article
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //Get index Articles
        public ActionResult List()
        {
            var articles = database
                .Articles
                .Include(a=>a.Author)
                .ToList();

            return View(articles);
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(500);
            }

            var article = database.Articles
                .Include(a => a.Author)
                .First(a => a.Id == id);

            if (article == null)
            {
                return StatusCode(500);
            }

            return View(article);
        }

        // GET: Article/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                //get user id
                var authorId = database.Users
                    .Where(u => u.UserName == this.User.Identity.Name)
                    .First()
                    .Id;

                //Set articles author
                article.AuthorId = authorId;

                //save article
                database.Articles.Add(article);
                database.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Article/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            //check article exist
            if (id==null)
            {
                return NotFound();
            }

            //create view model
            var article = database.Articles
                .Where(a => a.Id == id)
                .Include(a => a.Author)
                .First();

            if (IsAuthorized(article) == false)
            {
                return Forbid();
            }

            var model = new ArticleViewModel(article);


            return View(model);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //get article
                var article = database.Articles
                    .Where(a => a.Id == model.Id)
                    .Include(a => a.Author)
                    .FirstOrDefault();

                //set new prop
                article.Title = model.Title;
                article.Content = model.Content;

                //save changes
                database.Update(article);
                database.SaveChanges();

                //redirect to index
                return RedirectToAction("Details", new { id = article.Id });
            }

            return View(model);
        }

        // GET: Article/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = database.Articles
                .Include(a => a.Author)
                .First(m => m.Id == id);

            if (IsAuthorized(article) == false)
            {
                return Forbid();
            }

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //get article
            var article = database.Articles
                .Include(a => a.Author)
                .First(m => m.Id == id);

            //check if exist
            if (article == null)
            {
                return NotFound();
            }

            //delete article
            database.Articles.Remove(article);
            database.SaveChanges();

            //Redirect to index
            return RedirectToAction("Index");

        }

        private bool ArticleExists(int id)
        {
            return database.Articles.Any(e => e.Id == id);
        }

        private bool IsAuthorized(Article article)
        {
            bool isAuthor = article.IsAuthor(User.Identity.Name);
            bool isAdmin = User.IsInRole("Admin");

            return isAdmin || isAuthor;
        }
    }
}
