namespace EGeek.Catalog.Entities
{
    public abstract class AuditableEntity
        :Entity
    {
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
