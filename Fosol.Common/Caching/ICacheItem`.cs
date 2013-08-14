using System;
namespace Fosol.Common.Caching
{
    /// <summary>
    /// Base interface for CacheItem objects.
    /// </summary>
    /// <typeparam name="T">Type of object being cached.</typeparam>
    interface ICacheItem<T>
        : ICacheItem
    {
        T Value { get; }
    }
}
