using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
  public  class OrderedTree
    {

        public OrderedTree()
        {
            Hijos = new List<Tematicas>();
            Nietos = new List<Tematicas>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Tematicas> Hijos { get; set; }
        public List<Tematicas> Nietos { get; set; }

    }
}
