using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class BookImg
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookImageUrl { get; set; }
        [ForeignKey("BookId")]
        /* Definir a qeé tabla pertenece LibroId */
        public virtual Book Book { get; set; }
    }
}
