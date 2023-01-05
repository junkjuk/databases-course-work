using System.Data.MySqlClient;
using Dapper;
using Lab6.DTO;

namespace Lab6.DataController;

public class UsersDataController
{
    private IConfiguration _configuration;
    private string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

    public UsersDataController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected SqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        using (var connection = GetConnection())
        {
            await connection.OpenAsync();
            
            return await connection.QueryAsync<User>("SELECT * FROM users");;
        }
    }
    
    public async Task<User> GetById(int id)
    {
        using (var connection = GetConnection())
        {
            await connection.OpenAsync();
            
            return await connection.QuerySingleAsync<User>($"SELECT * FROM users WHERE id={id}");;
        }
    }
    
    public async Task<bool> Update(User user)
    {
        using (var connection = GetConnection())
        {
            await connection.OpenAsync();
            var rows = await connection.ExecuteAsync($"UPDATE users SET UserName = \'{user.UserName}\', Email = \'{user.Email}\' WHERE id={user.Id}");
            return rows > 0;
        }
    }
    
    public async Task<bool> Delete(int id)
    {
        using (var connection = GetConnection())
        {
            await connection.OpenAsync();
            
            var rows = await connection.ExecuteAsync($"DELETE FROM users WHERE id = {id}");
            return rows > 0;
        }
    }
    
    public async Task<bool> Create(User user)
    {
        using (var connection = GetConnection())
        {
            await connection.OpenAsync();
            var rows = await connection.ExecuteAsync($"INSERT INTO users VALUES(\'{user.UserName}\', \'{user.Email}\')");
            return rows > 0;
        }
    }
}