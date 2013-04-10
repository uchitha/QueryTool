using System;

namespace Qt.Domain
{
    public interface IIdentityAware
    {
        long Id { get; set; }
        DateTime CreatedDateTime { get; set; }
        //public User LastModifiedBy { get; set; }
        //public DateTime LastModifiedDateTime { get; set; }
    }
}
