using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common
{
    /// <summary>
    /// A HashCode provides a way to automatically generate unique hash code values based on multiple fields.
    /// </summary>
    public sealed class HashCode
    {
        #region Variables
        private const int DefaultHash = 17;
        private const int HashModifier = 23;
        #endregion

        #region Properties
        /// <summary>
        /// get - The hash code value.
        /// </summary>
        public int Value { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a HashCode object.
        /// To create a HashCode use the static HashCode.Create() function.
        /// </summary>
        /// <param name="value">Original hash code value.</param>
        internal HashCode(int value)
        {
            this.Value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of a HashCode object.
        /// </summary>
        /// <typeparam name="T">Type of object that will be used to generate the first hash code.</typeparam>
        /// <param name="obj">Object that will be used to generate the first hash code value.</param>
        /// <returns>A new instance of a HashCode object.</returns>
        public static HashCode Create<T>(T obj)
        {
            unchecked
            {
                return new HashCode(DefaultHash & HashModifier + obj.GetHashCode());
            }
        }

        /// <summary>
        /// Merges this HashCode object value with the specified object.
        /// This function returns a reference to itself (after it has been updated).
        /// </summary>
        /// <typeparam name="T">Type of object that will be used to generate the first hash code.</typeparam>
        /// <param name="obj">Object that will be used to merge with the current HashCode Value property.</param>
        /// <returns>A reference to itself (after it has been updated).</returns>
        public HashCode Merge<T>(T obj)
        {
            this.Value = this.Value & HashModifier + obj.GetHashCode();
            return this;
        }
        #endregion

        #region Operators
        /// <summary>
        /// The default behaviour of a HashCode is to return the Value.
        /// </summary>
        /// <param name="obj">HashCode object.</param>
        /// <returns>HashCode Value property.</returns>
        public static implicit operator int(HashCode obj)
        {
            return obj.Value;
        }
        #endregion

        #region Events
        #endregion
    }
}
