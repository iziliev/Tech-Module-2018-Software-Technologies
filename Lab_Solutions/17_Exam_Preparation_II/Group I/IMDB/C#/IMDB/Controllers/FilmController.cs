﻿namespace IMDB.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class FilmController : Controller
    {
        private readonly IMDBDbContext dbContext;

        public FilmController(IMDBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var films = dbContext.Films.ToList();
            return View(films);
        }

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                dbContext.Films.Add(film);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
                
        }

        [HttpGet]
        [Route("/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            Film film = dbContext
                .Films
                .Where(f => f.Id == id)
                .FirstOrDefault();

            return View(film);
        }

        [HttpPost]
        [Route("/edit/{id}")]
        public IActionResult Edit(Film film)
        {
            dbContext.Films.Update(film);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            Film film = dbContext
                .Films
                .Where(f => f.Id == id)
                .FirstOrDefault();

            return View(film);
        }

        [HttpPost]
        [Route("/delete/{id}")]
        public IActionResult Delete(Film film)
        {
            dbContext.Films.Remove(film);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
