using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public class FileLink
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public Document Document { get; set; }

        public FileLink(Guid FileId)
        {
            this.FileId = FileId;
        }
    }
}
