using DummyProject.BAL.IAuthServices;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DummyProjectDAL.AppDbContext;
using Dapper;
using DummyProjectModel.Enitity;
using Azure.Identity;

namespace DummyProject.BAL.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContexts _appContext;
        //private readonly DbContext _dbContext;
        private readonly string _connectionString = "DefaultConnection";
        public AuthService(IConfiguration configuration, AppDbContexts appContext)
        {
            _configuration = configuration;

            _appContext = appContext;
            //_connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> UserRegistration(Registration registration)
        {

            try
            {
                using var connection = _appContext.Database.GetDbConnection();

                string query = "Ups_UserRegistration";
                var param = new
                {
                    UserId = Guid.NewGuid(),
                    UserName = registration.UserName,
                    EmailId = registration.EmailId,
                    Password = registration.Password,
                    FirstName = registration.FirstName,
                    Lastname = registration.Lastname
                };

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();
                int result = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    _appContext.Registration.Add(registration);
                    await _appContext.SaveChangesAsync();
                    return "Done";

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Registration> UserLogin( string email, string password)
        {
             var result =await _appContext.Registration.Where(user => user.EmailId == email&& user.Password == password).FirstOrDefaultAsync();
            return result;
          
        }
        public async Task<IEnumerable<Registration>> GetAllUsers()
        {
            var result =await _appContext.Registration.ToListAsync();// linq
            return result;
            
        }
        public async Task<Registration> GetUserById(Guid userId)
        {
            var result = await _appContext.Registration.Where(user => user.UserId == userId).FirstOrDefaultAsync();// linq
            return result;
        }
    }
}
