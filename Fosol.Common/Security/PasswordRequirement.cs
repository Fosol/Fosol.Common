using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Security
{
    /// <summary>
    /// PasswordRequirement class provides a way to define the required composition of a password.
    /// </summary>
    public class PasswordRequirement
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - Minimum number of characters.
        /// </summary>
        public int MinLength { get; private set; }

        /// <summary>
        /// get - Maximum number of characters.
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// get - Minimum number of nonalphabetic characters.
        /// </summary>
        public int MinNonalphaCharacters { get; private set; }

        /// <summary>
        /// get - Minimum number of nonalphabetic and nondigit characters.
        /// </summary>
        public int MinNonalphaDigitCharacters { get; private set; }

        /// <summary>
        /// get - Minimum number of alphabetic characters.
        /// </summary>
        public int MinAlphaCharacters { get; private set; }

        /// <summary>
        /// get - Minimum number of digit characters.
        /// </summary>
        public int MinDigitCharacters { get; private set; }

        /// <summary>
        /// get - Minimum number of uppercase characters.
        /// </summary>
        public int MinUppercaseCharacters { get; private set; }

        /// <summary>
        /// get - Minimum number of lowercase characters.
        /// </summary>
        public int MinLowercaseCharacters { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a PasswordRequirement class.
        /// </summary>
        /// <param name="minLength">Minimum length of password.</param>
        /// <param name="maxLength">Maximum length of password.</param>
        /// <param name="minAlphaCharacters">Minimum number of alphabet characters.</param>
        /// <param name="minNonalphaCharacters">Minimum number of nonalphabet characters (digits and symbols).</param>
        public PasswordRequirement(int minLength, int maxLength, int minAlphaCharacters, int minNonalphaCharacters)
        {
            Fosol.Common.Validation.Assert.Range(minLength, 0, maxLength, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, minLength, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minAlphaCharacters, 0, "minAlphaCharacters");
            Fosol.Common.Validation.Assert.MinRange(minNonalphaCharacters, 0, "minNonalphaCharacters");
            Fosol.Common.Validation.Assert.MaxRange(minAlphaCharacters + minNonalphaCharacters, maxLength, "maxLength");

            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.MinAlphaCharacters = minAlphaCharacters;
            this.MinNonalphaCharacters = minNonalphaCharacters;
        }

        /// <summary>
        /// Creates a new instance of a PasswordRequirement class.
        /// </summary>
        /// <param name="minLength">Minimum length of password.</param>
        /// <param name="maxLength">Maximum length of password.</param>
        /// <param name="minNonalphaCharacters">Minimum number of nonalphabet characters (digits and symbols).</param>
        /// <param name="minUppercaseCharacters">Minimum number of uppercase characters.</param>
        /// <pparam name="minLowercaseCharacters">Minimum number of lowercase characters.</pparam>
        public PasswordRequirement(int minLength, int maxLength, int minNonalphaCharacters, int minUppercaseCharacters, int minLowercaseCharacters)
        {
            Fosol.Common.Validation.Assert.Range(minLength, 0, maxLength, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, minLength, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minNonalphaCharacters, 0, "minNonalphaCharacters");
            Fosol.Common.Validation.Assert.MinRange(minUppercaseCharacters, 0, "minUppercaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minLowercaseCharacters, 0, "minLowercaseCharacters");
            Fosol.Common.Validation.Assert.MaxRange(minNonalphaCharacters + minUppercaseCharacters + minLowercaseCharacters, maxLength, "maxLength");

            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.MinNonalphaCharacters = minNonalphaCharacters;
            this.MinUppercaseCharacters = minUppercaseCharacters;
            this.MinLowercaseCharacters = minLowercaseCharacters;
        }

        /// <summary>
        /// Creates a new instance of a PasswordRequirement class.
        /// </summary>
        /// <param name="minLength">Minimum length of password.</param>
        /// <param name="maxLength">Maximum length of password.</param>
        /// <param name="minUppercaseCharacters">Minimum number of uppercase characters.</param>
        /// <pparam name="minLowercaseCharacters">Minimum number of lowercase characters.</pparam>
        /// <param name="minNonalphaDigitCharacters">Minimum number of nonalphabet and nondigit characters (symbols).</param>
        /// <param name="minDigitCharacters">Minimum number of digit characters (0-9).</param>
        public PasswordRequirement(int minLength, int maxLength, int minUppercaseCharacters, int minLowercaseCharacters, int minNonalphaDigitCharacters, int minDigitCharacters)
        {
            Fosol.Common.Validation.Assert.Range(minLength, 0, maxLength, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, minLength, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minUppercaseCharacters, 0, "minUppercaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minLowercaseCharacters, 0, "minLowercaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minNonalphaDigitCharacters, 0, "minNonalphaDigitCharacters");
            Fosol.Common.Validation.Assert.MinRange(minDigitCharacters, 0, "minDigitCharacters");
            Fosol.Common.Validation.Assert.MaxRange(minUppercaseCharacters + minLowercaseCharacters + minNonalphaDigitCharacters + minDigitCharacters, maxLength, "maxLength");

            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.MinUppercaseCharacters = minUppercaseCharacters;
            this.MinLowercaseCharacters = minLowercaseCharacters;
            this.MinNonalphaDigitCharacters = minNonalphaDigitCharacters;
            this.MinDigitCharacters = minDigitCharacters;
        }
        #endregion

        #region Methods
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
