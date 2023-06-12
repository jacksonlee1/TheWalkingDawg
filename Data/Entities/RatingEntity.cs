using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class RatingEntity
    {
        [Key]
        public int Id {get;set;}

        [ForeignKey("Walks")]
        public int WalkId{get;set;}


        //public WalkEntity Walk{get;set;}

        [ForeignKey(nameof(Author))]
        public int AuthorId {get; set;}

        public virtual UserEntity Author{get;set;}
        
        [Required]
        public double Score{get;set;}

        public string? Comment{get;set;}
        

    }
}