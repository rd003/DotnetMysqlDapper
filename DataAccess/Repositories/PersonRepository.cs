using System.Data;
using Dapper;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccess.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    public PersonRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("default");
    }

    private IDbConnection GetConnection()
    {
        using IDbConnection connection = new MySqlConnection(_connectionString);
        return connection;
    }
    public async Task<IEnumerable<Person>> GetPeopleAsync()
    {
        var connection = GetConnection();
        string sql = "select Id,Name,Email from Person";
        var people = await connection.QueryAsync<Person>(sql);
        return people;
    }

    public async Task<Person> GetPeopleByIdAsync(int id)
    {
        var connection = GetConnection();
        string sql = "select Id,Name,Email from Person where Id=@id";
        var people = await connection.QueryFirstOrDefaultAsync<Person>(sql, new { id });
        return people;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        var connection = GetConnection();
        string query = @"insert into Person(Name,Email) values(@Name,@Email); select Last_Insert_Id();";
        int createdId = await connection.ExecuteScalarAsync<int>(query, new { person.Name, person.Email });
        person.Id = createdId;
        return person;

    }

    public async Task UpdatePersonAsync(Person person)
    {
        var connection = GetConnection();
        string query = @"update Person set Name=@Name,Email=@Email where Id=@Id";
        await connection.ExecuteAsync(query, person);
    }

    public async Task DeletePersonAsync(int id)
    {
        var connection = GetConnection();
        string query = @"delete from person where Id=@id";
        await connection.ExecuteAsync(query, new { id });
    }

}
