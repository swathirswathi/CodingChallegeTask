using CodingChallenge.Contexts;
using CodingChallenge.Exceptions;
using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        CodingTaskContext _context;
        ILogger<UserRepository> _logger;

        public UserRepository(CodingTaskContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("User added " + item.UserId);
            return item;

        }

        public async Task<User> Delete(int key)
        {
            var user = await GetAsync(key);
            _context?.Users.Remove(user);
            _context?.SaveChanges();
            _logger.LogInformation("User deleted " + key);
            return user;
        }

        public async Task<User> GetAsync(int key)
        {
            var users = await GetAsync();
            var user = users.FirstOrDefault(e => e.UserId == key);
            if (user != null)
            {
                return user;
            }
            throw new NoSuchUserException();
        }

        public async Task<List<User>> GetAsync()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user = await GetAsync(item.UserId);
            _context.Entry<User>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("User updated " + item.UserId);
            return user;
        }
    }
}
