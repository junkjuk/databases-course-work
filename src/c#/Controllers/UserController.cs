using Lab6.DataController;
using Lab6.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    public readonly UsersDataController DataController;

    public UserController(ILogger<UserController> logger, UsersDataController dataController)
    {
        _logger = logger;
        DataController = dataController;
    }
    
    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
    {
        var a = await DataController.GetAll();
        return a;
    }
    [HttpGet("{id}")]
    public async Task<User> GetById(int id)
    {
        return await DataController.GetById(id);
    }
    
    [HttpPost]
    public async Task<bool> Create([FromBody] User user)
    {
        return await DataController.Create(user);
    }
    
    [HttpPut]
    public async Task<bool> Update([FromBody] User user)
    {
        return await DataController.Update(user);
    }
    
    [HttpDelete]
    public async Task<bool> Update([FromBody] int id)
    {
        return await DataController.Delete(id);
    }
}