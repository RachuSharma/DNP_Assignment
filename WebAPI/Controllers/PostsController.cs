using ApiContracts.PostDTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("Post")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly IUserRepository userRepo;

    public PostsController(IPostRepository postRepo, IUserRepository userRepo)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<AddPostDto>> AddPost([FromBody] CreatePostDto
        request)
    {
        try
        {
            await verifyUserIdIsAvailableAsync(request.Userid);

            Post post = new Post(request.Userid, request.Title, request.PostsBody);
            Post Created = await postRepo.AddPostAsync(post);

            AddPostDto dto = new()
            {
                Id = Created.Id,
                Title = Created.Title,
                PostsBody = Created.PostsBody
            };
            return base.Created ($"/Post {dto.Id} ", Created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task verifyUserIdIsAvailableAsync(int requestUserid)
    {
        // Get user by ID asynchronously
        var user = await userRepo.GetSingleAsync(requestUserid);

        // If user does not exist, throw an exception
        if (user == null)
        {
            Console.WriteLine($"User with ID {requestUserid} not found.");
        }
    }
    
    [HttpGet, Route("{id}")]
    public async Task<ActionResult<GetPostDto>> GetPostById([FromRoute] int id)
    {
        try
        {
            Post post = await postRepo.GetSinglePostAsync(id);
            GetPostDto postDto = new()
            {
                Id = post.Id,
                Title = post.Title,
                PostsBody = post.PostsBody
            };
            return Ok(postDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<GetPostDto>> GetManyPost()
    {
        return Ok( postRepo.GetManyPost());
    }
    [HttpPatch]
    public async Task<ActionResult<CreatePostDto>> UpdatePost([FromBody] Post post)
    {
        try
        {
            await postRepo.UpdatePostAsync(post);
            return Ok("Your post is Updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpDelete]
    public async Task<ActionResult> DeletePost([FromBody] int post)
    {
        try
        {
            await postRepo.DeletePostAsync(post);
            return Ok($"Post Id {post} Deleted");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }  
    }
}