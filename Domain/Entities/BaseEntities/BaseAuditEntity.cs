namespace Domain.Entities.BaseEntities
{
    public class BaseAuditEntity<TKey> : BaseEntity<TKey>
    {
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTimeOffset.UtcNow;
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
