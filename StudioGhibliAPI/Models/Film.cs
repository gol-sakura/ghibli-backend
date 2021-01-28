using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudioGhibliAPI.Models
{
    public class Film
    {
        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("title")]
        [Required]
        public string Title { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("producer")]
        public string Producer { get; set; }

        [JsonProperty("release_date")]
        public string Release_Date { get; set; }

        [JsonProperty("rt_score")]
        public string Rate_Score { get; set; }

       
    }  

}
