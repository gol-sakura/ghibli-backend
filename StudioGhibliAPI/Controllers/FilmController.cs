using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudioGhibliAPI.Context;
using StudioGhibliAPI.Models;
using StudioGhibliAPI.Models.DTOs;
using StudioGhibliAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace StudioGhibliAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class FilmController : Controller
    {
        private IGhibliRepository _ghibli;
        private readonly IMapper _mapper;

        private readonly HttpClient _client;

        private readonly GhibliDbContext db;
        private const string url = "https://ghibliapi.herokuapp.com/films";
        private readonly IMemoryCache _memoryCache;



        public FilmController(IGhibliRepository ghibli, IMapper mapper, HttpClient client, IMemoryCache cache, GhibliDbContext _db)
        {
            _ghibli = ghibli;
            _mapper = mapper;
            _client = client;
             db = _db;
            _memoryCache = cache;
        }


        [HttpGet]
         public async Task<IActionResult> GetAll()
        {
            string baseUrl = "https://ghibliapi.herokuapp.com/films";

            var cacheKey = $"Get_Film";

            if (_memoryCache.TryGetValue(cacheKey, out string cachedValue))
                return Ok(cachedValue);


            var res = await _client.GetAsync(baseUrl);

            if (!res.IsSuccessStatusCode)
            {
                throw new Exception("cannot read data!");
            }

            var content = await res.Content.ReadAsStringAsync();
            var f = JsonConvert.DeserializeObject<ICollection<Film>>(content);

            
            foreach (var x in f)
            {
                if (!db.Film.Any(f => f.Id == x.Id))
                {
                    
                    await db.AddAsync(x);
                    await db.SaveChangesAsync();
                }
               
            }


            return Ok(f);


        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var responseHttp = await _client.GetAsync($"{url}/{id}");

            var cacheKey = $"Get_On_Film_Id-{id}";

          
            if (_memoryCache.TryGetValue(cacheKey, out string cachedValue))
                return Ok(cachedValue);

            try
            {
                if (!responseHttp.IsSuccessStatusCode)
                {
                    throw new Exception("cannot read Id!");
                }

                var content = await responseHttp.Content.ReadAsStringAsync();
                var filmId = JsonConvert.DeserializeObject<Film>(content);
                return Ok(filmId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            } 

        }


       
      
    }
}
