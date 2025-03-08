using DummyProjectModel.Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyProject.BAL.IAuthServices
{
    public interface IAuthService 
    {
        
        Task<string> UserRegistration(Registration registration);
        Task<Registration> UserLogin(string email, string password);
        Task<IEnumerable<Registration>> GetAllUsers();
        Task<Registration> GetUserById(Guid userId);
    }
}
