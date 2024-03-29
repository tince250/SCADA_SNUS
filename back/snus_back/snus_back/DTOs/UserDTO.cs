﻿using snus_back.Models;

namespace snus_back.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public UserDTO(int id, string username, string name, string lastname, string role)
        {
            Id = id;
            Username = username;
            Name = name;
            LastName = lastname;
            Role = role;
         }
    }
}
