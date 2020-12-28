using RestWithAspNet5.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace RestWithAspNet5.Model
{

    [Table("book")]
    public class Book : BaseEntity
    {

        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("lauch_date")]
        public DateTime LaunchDate { get; set; }

    }
}
