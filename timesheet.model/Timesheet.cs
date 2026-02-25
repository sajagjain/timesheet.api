using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timesheet.model
{
    //Tasks
    //Employee
    //Timesheet -> Day Task
    //
    internal class Timesheet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public ICollection<Task> Tasks { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset TimeSheetDate { get; private set; }
    }
}
