using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timesheet.business.Dtos;

public record AutoCompleteResponseDto(List<TaskForAutoComplete> results);

public record TaskForAutoComplete(int Id, string Name);
