﻿namespace WBAuth.DAO.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public Role? Role { get; set; }
        public int IdFonction { get; set; }
        public Fonction? Fonction { get; set; }
        public string? Nom { get; set; }
        public string? Status { get; set; } 

    }
}
