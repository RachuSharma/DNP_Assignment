using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto
        request)
    {
        try
        {
            await verifyUserNameIsAvailableAsync(request.UserName);

            User user = new(request.UserName, request.Password);
            User created = await userRepo.AddUserAsync(user);

            UserDto dto = new()
            {
                Id = created.Id,
                UserName = created.UserName
            };
            return Created($"/Users{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    private async Task verifyUserNameIsAvailableAsync(object userName)
    {
        /*
         * check character length
         else throw exception
         * check if user is already exiest
         */

        if (userName.ToString().Length <= 4 || userName.ToString().Length > 15)
        {
            throw new Exception("Username should be between 5-15 character");
        }

        bool isUserExist = userRepo.GetManyUser().Any(u => u.UserName == userName.ToString());
        if (isUserExist)
        {
            throw new Exception("Username already exist");
        }
    }

    [HttpGet, Route("{id}")]
    public async Task<ActionResult<UserDto>> GetSingleUserById([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);
            UserDto userDto = new()
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("/username/{userName}")]
    public async Task<ActionResult<UserDto>> GetSingleUserByUserName([FromRoute] string userName)
    {
        try
        {
            User user = await userRepo.GetSingleUserByUserNameAsync(userName);
            UserDto userDto = new()
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetManyUsers()
    {
        return Ok( userRepo.GetManyUser());
    }

    [HttpPatch]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] User user)
    {
        try
        {
            await userRepo.UpdateUserAsync(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<UserDto>> DeleteUser([FromBody] int user)
    {
        try
        {
            await userRepo.DeleteUserAsync(user);
            return Ok($"User Id {user} Deleted");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }  
    }
}