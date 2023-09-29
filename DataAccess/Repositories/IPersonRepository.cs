using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPeopleAsync();
    Task<Person> GetPeopleByIdAsync(int id);
    Task<Person> CreatePersonAsync(Person person);
    Task UpdatePersonAsync(Person person);
    Task DeletePersonAsync(int id);
}
