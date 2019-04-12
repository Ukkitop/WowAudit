using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class AccessToken
    {
        public string access_token;
        public string token_type;
        public int expires_in;

        public string viewKey()
        {
            return String.Format("Token: {0}, token_type: {1}, expires_in: {2}", access_token, token_type, expires_in); 
        }
    }

    
}
