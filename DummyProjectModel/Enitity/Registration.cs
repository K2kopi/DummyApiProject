using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyProjectModel.Enitity
{
    public class Registration
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserName{ get; set; }      

        public string? Password { get; set; }
        public string? EmailId{ get; set; }
        public string? FirstName { get; set; }
        public string? Lastname{ get; set; }

        //public byte[] PasswordHash { get; set; }

      //  public Guid PasswordSalt { get; set; }
    }
}
