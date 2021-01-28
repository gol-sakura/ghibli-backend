using StudioGhibliAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioGhibliAPI.Repository.IRepository
{
    public interface IGhibliRepository
    {
        // perform operation on films

        Task<ICollection<Film>> GetFilms();
        Task<Film> GetFilmById(string id);
        
        Task<Film> CreateAsync(Film film);
        Task<Film> UpdateAsync(Film film);
        Task<Film> DeleteAsync(Film film);
        

    }
}
