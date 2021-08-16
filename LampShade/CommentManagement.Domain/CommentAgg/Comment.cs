using System.Collections.Generic;
using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public string Website { get; set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public bool Confirmed { get; private set; }
        public bool Canceled { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }


        protected Comment(string website)
        {
            Website = website;
        }
        public Comment(string name, string email, string message, string website, long ownerRecordId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
            Website = website;
         
        }


        public void Confirm()
        {
            Confirmed = true;
        }
        public void Cancel()
        {
            Canceled = true;
        }
    }
}
