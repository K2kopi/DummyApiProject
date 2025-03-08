using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyProjectModel.Enitity
{
    public class Login
    {
        [Key]
        public string EmailId { get; set; }
        
        public string Password { get; set; }
    }
}
