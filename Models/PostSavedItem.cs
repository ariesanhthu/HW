using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Models
{
    public class PostSavedItem
    {
        public int PostId { get; set; }

        public string Title { get; set; }
        public string Time { get; set; }
        //vote total amount
        public PostSavedItem()
        {

        }

        public PostSavedItem(PostNewFeed postnewsfeed)
        {
            PostId = postnewsfeed.Id;
            Title = postnewsfeed.Title;
            Time = postnewsfeed.Time;
        }
    }
}
