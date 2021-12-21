using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mus.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }

        public List<Singer> Singers { get; set; }
        public Song()
        {
            Singers = new List<Singer>();
        }
    }
}
