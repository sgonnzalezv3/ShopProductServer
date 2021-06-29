using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please insert the name of the book")]
        public string Name { get; set; }
        [Range(10, 100, ErrorMessage = "The regular price is between 10 and 100")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please insert the name of the author")]
        public string Author { get; set; }
        public string Details { get; set; }
        public virtual ICollection<BookImgDto> BookImgList { get; set; }
        public List<string> UrlImages { get; set; }

    }
}
