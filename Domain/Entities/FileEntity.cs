namespace Domain.Entities
{
    public class FileEntity : BaseEntity
    {
        public string? FileName { get; set; }
        public string? TransactionName { get; set; }
    }
}
