using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppSibers.Domain.Enum
{
    public enum TaskStatus
    {
        [Display(Name = "To Do")]
        ToDo,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Done")]
        Done
    }
}
