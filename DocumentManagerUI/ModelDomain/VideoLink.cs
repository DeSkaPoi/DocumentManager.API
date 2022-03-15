using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public class VideoLink
    {
        public Guid Id { get; set; }
        //public string ServiceUrl { get; set; }
        public Guid VideoId { get; set; }
        public Document Document { get; set; }
        public VideoLink(Guid VideoId)
        {
            this.VideoId = VideoId;
        }
    }
}
