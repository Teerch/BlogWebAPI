using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using WebApi.Repository;
namespace Web.Services;
public class BlogService : IBlogService
{
    private readonly BlogContext _context;
    public BlogService(BlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var user = await _context.Users.AsNoTracking().ToListAsync();
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Email == email);
        return user;
    }
    public async Task<ActionResult<IEnumerable<Posts>>> GetPostsOfUserById(int id)
    {
        var user = await _context.Posts.Where(x => x.UserId == id).ToListAsync();
        return user;
    }

    public async Task<User> CreateUserAsync([FromBody] string firstname, string lastname, string username, string password, string email)
    {
        byte[] passwordHash, passwordKey;
        using (var hmac = new HMACSHA512())
        {
            passwordKey = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        User user = new();
        user.First_name = firstname;
        user.Last_name = lastname;
        user.User_name = username;
        user.Password = passwordHash;
        user.Passwordkey = passwordKey;
        user.Email = email;


        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
    public async Task<bool> UserAlreadyExists(string userName)
    {
        return await _context.Users.AnyAsync(x => x.User_name == userName);
    }
    public async Task<bool> EmailAlreadyExists(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }
    public async Task<User> Autenticate(string userName, byte[] password)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.User_name == userName && x.Password == password);
    }

    public async Task<User> AutenticateUserAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.User_name == username);

        if (user == null || user.Passwordkey == null)
        {
            return null;
        }
        if (!MatchPasswordHash(password, user.Password, user.Passwordkey))
        {
            return null;
        }
        return user;
    }

    private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
    {
        using (var hmac = new HMACSHA512(passwordKey))
        {
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
            for (int i = 0; i < password.Length; i++)
            {
                if (passwordHash[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
    }


    public async Task DeleteUserAsync(int id)
    {
        var UserToDelete = _context.Users.Find(id);
        if (UserToDelete != null)
        {
            _context.Users.Remove(UserToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Posts> CreatePostAsync(Posts newPost)
    {
        await _context.AddAsync(newPost);
        await _context.SaveChangesAsync();

        return newPost;
    }

    public async Task<Posts> GetPostByIdAsync(int id)
    {
        var post = await _context.Posts
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);
        return post;
    }

    public async Task<Posts> EditPostAsync(int postId, string post)
    {
        var postToUpdate = await _context.Posts.FindAsync(postId);

        if (postToUpdate is null)
        {
            throw new InvalidOperationException("Post not available");
        }
        postToUpdate.Post = post;
        await _context.SaveChangesAsync();
        return postToUpdate;
    }

    public async Task DeletePostAsync(int id)
    {
        var PostToDelete = await GetPostByIdAsync(id);
        if (PostToDelete != null)
        {
            _context.Posts.Remove(PostToDelete);
            await _context.SaveChangesAsync();
        }

    }

    public async Task<Comments> CreateCommentAsync(Comments newComment)
    {
        await _context.AddAsync(newComment);
        await _context.SaveChangesAsync();

        return newComment;
    }

    public async Task<Comments> GetCommentByIdAsync(int id)
    {
        var comment = await _context.Comments
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);
        return comment;
    }
    public async Task DeleteCommentAsync(int id)
    {
        var commentToDelete = await GetCommentByIdAsync(id);
        if (commentToDelete is not null)
        {
            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Comments> EditCommentAsync(int commentId, string post)
    {
        var editComment = await _context.Comments.FindAsync(commentId);
        if (editComment == null)
        {
            throw new InvalidOperationException("error...");
        }
        editComment.Comment = post;
        await _context.SaveChangesAsync();
        return editComment;
    }


}