using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mus.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace Mus.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;
        public HomeController(UsersContext context)
        {
            this.db = context;
            // добавляем начальные данные
            if (db.Songs.Count() == 0)
            {
                Song song1 = new Song { Name = "Pip",Genre="qg" };
                Song song2 = new Song { Name = "Pop", Genre="an" };
                Song song3 = new Song { Name = "Girl", Genre = "wx" };
                Song song4 = new Song { Name = "Day", Genre = "sp" };
                Song song5 = new Song { Name = "Sun", Genre = "ym" };
                Song song6 = new Song { Name = "Apple", Genre = "cr" };

                Singer singer1 = new Singer { Name = "Man",Country="USA", Song = song1, Age = 21 };
                Singer singer2 = new Singer { Name = "Friend", Country = "England", Song = song2, Age = 24 };
                Singer singer3 = new Singer { Name = "Chell", Country = "Spain", Song = song4, Age = 25 };
                Singer singer4 = new Singer { Name = "Dall", Country = "Ukraine", Song = song5, Age = 30 };
                Singer singer5 = new Singer { Name = "Phill", Country = "Portugal", Song = song3, Age = 23 };
                Singer singer6 = new Singer { Name = "Grak", Country = "Canada", Song = song6, Age = 17 };
                

                db.Songs.AddRange(song1, song2, song3, song4, song5, song6);
                db.Singers.AddRange(singer1, singer2, singer3, singer4, singer5, singer6);
                db.SaveChanges();
            }
        }
        public async Task<IActionResult> Index(int? song, string name, int page = 1,
            Sort sortOrder = Sort.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<Singer> singers = db.Singers.Include(x => x.Song);

            if (song != null && song != 0)
            {
                singers = singers.Where(p => p.SongId == song);
            }
            if (!String.IsNullOrEmpty(name))
            {
                singers = singers.Where(p => p.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case Sort.NameDesc:
                    singers = singers.OrderByDescending(s => s.Name);
                    break;
                case Sort.AgeAsc:
                    singers = singers.OrderBy(s => s.Age);
                    break;
                case Sort.AgeDesc:
                    singers = singers.OrderByDescending(s => s.Age);
                    break;
                case Sort.GenreAsc:
                    singers = singers.OrderBy(s => s.Song.Genre);
                    break;
                case Sort.GenreDesc:
                    singers = singers.OrderByDescending(s => s.Song.Genre);
                    break;
                case Sort.SongAsc:
                    singers = singers.OrderBy(s => s.Song.Name);
                    break;
                case Sort.SongDesc:
                    singers = singers.OrderByDescending(s => s.Song.Name);
                    break;
                default:
                    singers = singers.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await singers.CountAsync();
            var items = await singers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Songs.ToList(), song, name),
                Singers = items
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}