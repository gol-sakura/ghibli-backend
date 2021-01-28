using Microsoft.EntityFrameworkCore;
using StudioGhibliAPI.Context;
using StudioGhibliAPI.Models;
using StudioGhibliAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioGhibliAPI.Repository
{
    public class GhibliRepository : IGhibliRepository
    {
        private readonly GhibliDbContext _db;

        public GhibliRepository(GhibliDbContext db)
        {
            _db = db;
        }
       
        
        async Task<ICollection<Film>> IGhibliRepository.GetFilms()
        {
            return await _db.Film.ToListAsync();
        }

        async Task<Film> IGhibliRepository.GetFilmById(string id)
        {
            return await _db.Film.FirstOrDefaultAsync(a => a.Id == id);
        }

        async Task<Film> IGhibliRepository.CreateAsync(Film film)
        {
            film.Id = Guid.NewGuid().ToString();

            await _db.Film.AddAsync(film);

            await _db.SaveChangesAsync();

            return film;
            
        }


        async Task<Film> IGhibliRepository.UpdateAsync(Film film)
        {
            _db.Film.Update(film);

            await _db.SaveChangesAsync();

            return film;

        }

        async Task<Film> IGhibliRepository.DeleteAsync(Film film)
        {
           // var f = await _db.Film.FindAsync();
            _db.Film.Remove(film);
            await _db.SaveChangesAsync();
            return film;

        }


    }
}
