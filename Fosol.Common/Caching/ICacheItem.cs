using System;
namespace Fosol.Common.Caching
{
    /// <summary>
    /// Base interface for CacheItem objects.
    /// Only used internally.
    /// </summary>
    internal interface ICacheItem
    {
        DateTime CreatedDate { get; }
        void Dispose();
        DateTime? ExpiresOn { get; set; }
        string Key { get; }
        void Renew();
        void Renew(DateTime expiresOn);
        void Renew(TimeSpan timeToLive);
        bool RenewOnRequest { get; set; }
        TimeSpan? TimeToLive { get; set; }
    }
}
