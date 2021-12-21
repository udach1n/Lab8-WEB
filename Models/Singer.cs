using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mus.Models
{
    public class Singer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }

    }
}
