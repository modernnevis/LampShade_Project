namespace CommentManagement.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Website { get; set; }
        public string OwnerRecord { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }
        public bool Confirmed { get;  set; }
        public bool Canceled { get;  set; }
        public string CommentDate { get; set; }
    }
}