using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace WebApi.Repository
{
    public interface IBlogService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<ActionResult<IEnumerable<Posts>>> GetPostsOfUserById(int id);
        Task<User> CreateUserAsync([FromBody] string firstname, string lastname, string username, string password, string email);
        Task<User> Autenticate(string userName, byte[] password);
        Task<bool> UserAlreadyExists(string userName);
        Task<bool> EmailAlreadyExists(string email);
        Task<User> AutenticateUserAsync(string username, string password);
        Task DeleteUserAsync(int id);
        Task<Posts> CreatePostAsync(Posts newPost);
        Task<Posts> GetPostByIdAsync(int id);
        Task<Posts> EditPostAsync(int postId, string post);
        Task DeletePostAsync(int id);
        Task<Comments> CreateCommentAsync(Comments newComment);
        Task<Comments> GetCommentByIdAsync(int id);
        Task DeleteCommentAsync(int id);
        Task<Comments> EditCommentAsync(int commentId, string post);


    }
}