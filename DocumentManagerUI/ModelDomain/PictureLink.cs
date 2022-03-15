using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public class PictureLink
    {
        public Guid Id { get; set; }
        public Guid PictureId { get; set; }
        public Document Document { get; set; }
        public PictureLink(Guid PictureId)
        {
            this.PictureId = PictureId;
        }
    }
}
