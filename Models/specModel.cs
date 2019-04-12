using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class specModel
    {
        string name;
        string role;
        string backgroundImage;
        string icon;
        string description;
        int order;

        public string View()
        {
            return String.Format("spec name is: {0}, its role is: {1} \n Description: {2} \n", name, role, description); 
        }
    }
}
