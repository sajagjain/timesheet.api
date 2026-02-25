using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.model
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public TaskType TaskType { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
    }
}
