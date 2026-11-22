using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string PhotoPath { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
    }
}