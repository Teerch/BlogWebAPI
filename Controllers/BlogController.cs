using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using WebApi.Models.DTO.RequestDTO;
using WebApi.Models.DTO.ResponseDTO;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    BlogService _service;
    private readonly IMapper _mapper;

    public BlogController(BlogService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("User")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
    {
        var users = await _service.GetUsersAsync();
        if (users is not null)
        {
            return Ok(users.Select(_mapper.Map<UserResponseDTO>));
        }
        else
        {
            return NoContent();
        }
    }
    [HttpGet("AuthoriseUser")]
    public async Task<ActionResult<User>> AutenticateUserAsync(string username, string password)
    {
        var user = await _service.AutenticateUserAsync(username, password);
        if (user == null)
        {
            return BadRequest("Wrong username or password");
        }

        return user;
    }

    [HttpGet("User/id={id}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(int id)
    {
        var user = await _service.GetUserByIdAsync(id);

        if (user is not null)
        {
            return user;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("User/posts")]
    public async Task<ActionResult<IEnumerable<Posts>>> GetPostByUserById(int id)
    {
        var userPost = await _service.GetPostsOfUserById(id);
        return userPost;
    }

    [HttpPost("User")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRequestDTO newUser)
    {
        var CheckExistingUser = await _service.UserAlreadyExists(newUser.User_name);
        var CheckExistingEmail = await _service.EmailAlreadyExists(newUser.Email);

        // var user = _mapper.Map<User>(newUser);
        if (CheckExistingUser)
        {
            return BadRequest("Username already used");
        }
        if (CheckExistingEmail)
        {
            return BadRequest("Email already used");
        }

        await _service.CreateUserAsync(newUser.First_name, newUser.Last_name, newUser.User_name, newUser.Password, newUser.Email);
        return StatusCode(201);
    }

    [HttpDelete("User{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        if (user != null)
        {
            await _service.DeleteUserAsync(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("Post")]
    public async Task<IActionResult> CreatePostAsync([FromBody] PostRequestDTO newPost)
    {
        var post = _mapper.Map<Posts>(newPost);
        await _service.CreatePostAsync(post);
        if (!(newPost == null && post == null))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("Post")]
    public async Task<IActionResult> EditPostAsync(int id, string post)
    {
        var postToUpdate = await _service.EditPostAsync(id, post);
        if (postToUpdate is not null)
        {
            return Ok(postToUpdate);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("Post{id}")]
    public async Task<IActionResult> DeletePostAsync(int id)
    {
        var post = await _service.GetPostByIdAsync(id);
        if (post is not null)
        {
            await _service.DeletePostAsync(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("Comment")]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CommentRequestDTO newComment)
    {
        var comment = _mapper.Map<Comments>(newComment);
        await _service.CreateCommentAsync(comment);
        if (comment == null)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("Comment")]
    public async Task<IActionResult> DeleteCommentAsync(int id)
    {
        var comment = await _service.GetCommentByIdAsync(id);
        if (comment is not null)
        {
            await _service.DeleteCommentAsync(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("Comment/id={id}")]
    public async Task<IActionResult> EditCommentByIdAsync(int id, string comment)
    {
        var commentToEdit = await _service.EditCommentAsync(id, comment);
        if (commentToEdit is not null)
        {
            return Ok(commentToEdit);
        }
        else
        {
            return NotFound();
        }
    }
}