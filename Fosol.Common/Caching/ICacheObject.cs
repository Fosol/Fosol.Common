using System;
namespace Fosol.Common.Caching
{
    /// <summary>
    /// Interface for caching objects.
    /// </summary>
    public interface ICacheObject
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - When the item will expire and be disposed.
        /// </summary>
        DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// get - The object being cached.
        /// </summary>
        object Item { get; }

        /// <summary>
        /// get/set - The length of time before cache expires for this item.
        /// </summary>
        TimeSpan? LengthOfTime { get; set; }

        /// <summary>
        /// get/set - When 'true' it will reset the ExpiresOn based on the LengthOfTime when the object has been requested.
        /// </summary>
        bool RenewOnRequest { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Dispose the reference to the cached object.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Renew the ExpiresOn based on the LengthOfTime.
        /// If LengthOfTime is not set this method cannot do anything.
        /// </summary>
        void Renew();

        /// <summary>
        /// Renew the ExpiresOn cache length.
        /// </summary>
        /// <param name="expiresOn">Time when object should be released from cache.</param>
        void Renew(DateTime expiresOn);

        /// <summary>
        /// Renew the ExpiresOn based on the LengthOfTime.
        /// </summary>
        /// <param name="lengthOfTime">Length of time before this item is released from cache.</param>
        void Renew(TimeSpan lengthOfTime);
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
