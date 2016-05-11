using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class Role
    {
        public Role(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public Role()
        {
 
        }

        public override string ToString()
        {
            return Name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}
