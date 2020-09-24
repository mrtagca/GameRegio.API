using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.User
{
    public class UserUpdateModel
    {
        public string ObjectId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string ProfilePhoto { get; set; }
        public string Bio { get; set; }
        public int IsBanned { get; set; }
        public int IsDeleted { get; set; }
        public int RegisterDate { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
