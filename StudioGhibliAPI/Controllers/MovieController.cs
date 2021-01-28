using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioGhibliAPI.Context;
using StudioGhibliAPI.Models;
using StudioGhibliAPI.Models.DTOs;
using StudioGhibliAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudioGhibliAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly GhibliDbContext _context;

        private readonly IGhibliRepository _repo;
        private readonly IMapper _mapper;


        public MovieController(GhibliDbContext context, IGhibliRepository repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAllMovies()
        {
            var flist = await _repo.GetFilms();
            var dtoObj = new List<FilmDto>();

            foreach (var obj in flist)
            {
                 dtoObj.Add(_mapper.Map<FilmDto>(obj));
            }

            return Ok(dtoObj);
        }

        // GET api/<MovieController>/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetMovieByID(string id)
        {
            //var movie = await _context.Film.FirstOrDefaultAsync(f => f.Id == id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}
            //return movie;
            var obj = await _repo.GetFilmById(id);
            if (obj == null)
            {
                return NotFound();
            }

            var dtoObj =  _mapper.Map<FilmDto>(obj);

            return Ok(dtoObj);
        }


        // POST api/<MovieController>
        [HttpPost]
        public async Task<ActionResult<Film>> AddMovie([FromBody] FilmDto filmdto)
        {
            //_context.Film.Add(film);
            //await _context.SaveChangesAsync();

            //return Ok(_context.Film);
            if (filmdto == null)
            {
                return BadRequest(ModelState);
            }
            var obj = _mapper.Map<Film>(filmdto);
            await  _repo.CreateAsync(obj);

           return Ok(obj);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(string id, [FromBody] FilmDto filmdto)
        {
            //var dbFilm = await _context.Film.FindAsync(id);
            //if (dbFilm == null)
            //{
            //    return NotFound();
            //}

            //dbFilm.Title = film.Title;
            //dbFilm.Director = film.Director;
            //dbFilm.Release_Date = film.Release_Date;
            //dbFilm.Description = film.Description;
            //dbFilm.Producer = film.Producer;
            //dbFilm.Rate_Score = film.Rate_Score;

            //await _context.SaveChangesAsync();
            if (filmdto == null || id != filmdto.Id)
            {
                return BadRequest(ModelState);
            }

            var dtoObj = _mapper.Map<Film>(filmdto);

            await _repo.UpdateAsync(dtoObj);


            return Ok(dtoObj);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteSeminar(string id)
        {
            //var film = await _context.Film.FindAsync(id);
            //if (film == null)
            //{
            //    return NotFound();
            //}

            //_context.Film.Remove(film);
            //await _context.SaveChangesAsync();

            //return Ok(_context.Film);
            if (!FilmExists(id))
            {
                return NotFound();
            }

            var obj = await _repo.GetFilmById(id);

            await _repo.DeleteAsync(obj);

            return Ok("Deleted!");


        }


        private bool FilmExists(string id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
