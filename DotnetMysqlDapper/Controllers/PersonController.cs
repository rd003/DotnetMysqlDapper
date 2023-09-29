using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMysqlDapper.Controllers;

[Route("api/people")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepo;
    private readonly ILogger<PersonRepository> _logger;

    public PersonController(IPersonRepository personRepo, ILogger<PersonRepository> logger)
    {
        _personRepo = personRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var people = await _personRepo.GetPeopleAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var person = await _personRepo.GetPeopleByIdAsync(id);
            if (person == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person does not exist."
                });
            }
            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Person person)
    {
        try
        {
            var createdPerson = await _personRepo.CreatePersonAsync(person);
            return CreatedAtAction(nameof(Post), createdPerson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(Person person)
    {
        try
        {
            var existingPerson = await _personRepo.GetPeopleByIdAsync(person.Id);
            if (existingPerson == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person does not exist."
                });
            }
            await _personRepo.UpdatePersonAsync(person);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingPerson = await _personRepo.GetPeopleByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person does not exist."
                });
            }
            await _personRepo.DeletePersonAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }

}
