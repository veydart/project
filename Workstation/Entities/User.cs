using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workstation.Enums;

namespace Workstation.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Login { get; set; }

        public string Password { get; set; }

        public RoleOption Role { get; set; }
    }
}
