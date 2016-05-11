using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public  class Region
    {
        public Region(int REGION_ID, string NAME)
        {
            this.REGION_ID = REGION_ID;
            this.NAME = NAME;
          
        }

        public Region()
        {
 
        }
        public int REGION_ID { get; set; }
        public string NAME { get; set; }
        
    }
}
