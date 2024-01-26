using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class TematicTree
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Tematicas Hijo { get; set; }
        public Tematicas Nieto { get; set; }

    }
}
