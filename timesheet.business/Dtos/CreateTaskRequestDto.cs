using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using timesheet.model;

namespace timesheet.business.Dtos
{
    public class CreateTaskRequestDto
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }
}
