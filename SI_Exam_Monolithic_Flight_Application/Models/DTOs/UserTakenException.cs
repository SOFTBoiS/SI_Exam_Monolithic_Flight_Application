using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    public class UserTakenException : Exception
    {
        public UserTakenException(string? message) : base(message)
        {
        }
    }
}
