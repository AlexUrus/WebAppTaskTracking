using WebAppSibers.Domain.Enum;

namespace WebAppSibers.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int AuthorId { get; set; }  
        public Employee Author { get; set; }
        public int AssigneeId { get; set; }  
        public Employee Assignee { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; } 
        public Project Project { get; set; }
    }
}
