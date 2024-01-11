using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class FileShowContent
    {
        public string Title { get; set; }

        public byte[] Content { get; set; }
        public string TypeContent { get; set; }
    }
}
