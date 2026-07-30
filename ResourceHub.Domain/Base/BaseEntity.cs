using System.ComponentModel.DataAnnotations;

namespace ResourceHub.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}