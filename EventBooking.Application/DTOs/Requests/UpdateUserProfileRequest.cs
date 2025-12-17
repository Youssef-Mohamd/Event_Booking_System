using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.DTOs.Requests
{
    public class UpdateUserProfileRequest
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
    }
}
