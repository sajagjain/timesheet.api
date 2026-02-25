using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using timesheet.model;

namespace timesheet.business.Dtos
{
    public class CreateTaskRequestDto
    {
        //Task Name
        public string TaskName { get; private set; }

        public string TaskDescription { get; private set; }

        public TaskType Type { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
    }
}
