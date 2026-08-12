using ResourceHub.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace ResourceHub.Domain.Entities
{
    public class Users : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

    }
}
