using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Mus.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Song> songs, int? song, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            songs.Insert(0, new Song { Name = "Все", Id = 0 });
            Songs = new SelectList(songs, "Id", "Name", song);
            SelectedSong = song;
            SelectedName = name;
        }
        public SelectList Songs { get; private set; } 
        public int? SelectedSong{ get; private set; }  
        public string SelectedName { get; private set; }    
    }
}