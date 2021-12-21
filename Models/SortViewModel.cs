using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mus.Models
{
    public class SortViewModel
    {
        public Sort NameSort { get; private set; }
        public Sort AgeSort { get; private set; }
        public Sort GenreSort { get; private set; }
        public Sort SongSort { get; private set; }
        public Sort Current { get; private set; }

        public SortViewModel(Sort sortOrder)
        {
            NameSort = sortOrder == Sort.NameAsc ? Sort.NameDesc : Sort.NameAsc;
            AgeSort = sortOrder == Sort.AgeAsc ? Sort.AgeDesc : Sort.AgeAsc;
            GenreSort = sortOrder == Sort.GenreAsc ? Sort.GenreDesc : Sort.GenreAsc;
            SongSort = sortOrder == Sort.SongAsc ? Sort.SongDesc : Sort.SongAsc;
            Current = sortOrder;
        }
    }
}
