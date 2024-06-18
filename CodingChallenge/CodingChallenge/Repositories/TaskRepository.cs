using CodingChallenge.Contexts;
using CodingChallenge.Exceptions;
using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Repositories
{
    public class TaskRepository: IRepository<int, Taskk>
    {
            CodingTaskContext _context;
            ILogger<TaskRepository> _logger;

            public TaskRepository(CodingTaskContext context, ILogger<TaskRepository> logger)
            {
                _context = context;
                _logger = logger;

            }
            public async Task<Taskk> Add(Taskk item)
            {
                _context.Add(item);
                _context.SaveChanges();
                _logger.LogInformation("Task added " + item.TaskId);
                return item;
            }

            public async Task<Taskk> Delete(int key)
            {
                var task = await GetAsync(key);
                _context?.Taskks.Remove(task);
                _context?.SaveChanges();
                _logger.LogInformation("Task deleted " + key);
                return task;
            }

            public async Task<Taskk> GetAsync(int key)
            {
                var tasks = await GetAsync();
                var task = tasks.FirstOrDefault(e => e.TaskId == key);
                if (task != null)
                {
                    return task;
                }
                throw new NoSuchTaskException();

            }

            public async Task<List<Taskk>> GetAsync()
            {
                var tasks = _context.Taskks.ToList();
                return tasks;
            }

            public async Task<Taskk> Update(Taskk item)
            {
                var task = await GetAsync(item.TaskId);
                _context.Entry<Taskk>(item).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation("Task updated " + item.TaskId);
                return task;
            }
        
    }

}
