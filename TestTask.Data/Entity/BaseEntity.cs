using System.ComponentModel.DataAnnotations;

namespace TestTask.Data.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
    }
}
