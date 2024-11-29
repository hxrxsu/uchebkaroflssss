using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uchebkaroflssss.Data
{
    public class User
    {
        public int UserId {  get; set; }
        public string Role {  get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }
        public string? UserEventName {  get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
    }
}
