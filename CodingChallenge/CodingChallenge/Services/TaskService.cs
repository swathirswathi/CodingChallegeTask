using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Repositories;

namespace CodingChallenge.Services
{
    public class TaskService : ITaskServices
    {
        private readonly IRepository<int, Taskk> _taskRepository;

        public TaskService(IRepository<int, Taskk> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<Taskk>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAsync();
        }

        public async Task<Taskk> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetAsync(id);
        }

        public async Task<Taskk> AddTask(Taskk task)
        {
            // You may want to add some validation here before adding the member
            return await _taskRepository.Add(task);
        }

        public async Task<Taskk> UpdateTaskAsync(int id, Taskk updatedTask)
        {
            var existingMember = await _taskRepository.GetAsync(id);
            if (existingMember == null)
            {
                // Handle not found scenario
                throw new Exception("Task not found");
            }

            // Update properties of existingMember with values from updatedMember
            existingMember.Title = updatedTask.Title;
            existingMember.Description = updatedTask.Description;
            existingMember.DueDate = updatedTask.DueDate;
            existingMember.Status = updatedTask.Status;
            existingMember.CompletedDate = updatedTask.CompletedDate;

            return await _taskRepository.Update(existingMember);
        }

        public async Task<Taskk> DeleteTaskAsync(int id)
        {
            var existingMember = await _taskRepository.GetAsync(id);
            if (existingMember == null)
            {
                // Handle not found scenario
                throw new Exception("Task not found");
            }

            return await _taskRepository.Delete(id);
        }
    }
}
