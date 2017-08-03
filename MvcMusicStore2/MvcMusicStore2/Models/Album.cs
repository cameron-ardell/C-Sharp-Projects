using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore2.Models
{
    public class Album
    {
        public int AlbumID { get; set; }

        public string Title { get; set; }

        public Artist Arist { get; set; }

        public virtual List<Review> Review { get; set; }
    }
}