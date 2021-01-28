using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudioGhibliAPI.Models.DTOs
{
    public class FilmDto
    {
        
        
        public string Id { get; set; }  
        public string Title { get; set; }   
        public string Description { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string Release_Date { get; set; }
        public string Rate_Score { get; set; }
             
       
    }
}
