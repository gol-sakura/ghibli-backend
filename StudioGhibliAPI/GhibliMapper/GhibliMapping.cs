using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudioGhibliAPI.Models;
using StudioGhibliAPI.Models.DTOs;

namespace StudioGhibliAPI.GhibliMapper
{
    public class GhibliMapping : Profile
    {
        public GhibliMapping()
        {
            CreateMap<Film, FilmDto>().ReverseMap();
        }
    }
}
