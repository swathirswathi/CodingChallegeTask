using CodingChallenge.Models;

namespace CodingChallenge.Interfaces
{
    public interface ITaskServices
    {
        Task<List<Taskk>> GetAllTasksAsync();
        Task<Taskk> GetTaskByIdAsync(int id);
        Task<Taskk> AddTask(Taskk task);
        Task<Taskk> UpdateTaskAsync(int id, Taskk updatedTask);
        Task<Taskk> DeleteTaskAsync(int id);
    }
}
