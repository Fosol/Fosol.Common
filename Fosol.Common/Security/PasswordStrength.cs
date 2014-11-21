using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// PasswordStrength provides a way to define how strong a given password is.
    /// </summary>
    public sealed class PasswordStrength
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - Number of characters.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// get - Number of nonalphabetic characters.
        /// </summary>
        public int NonalphaCharacters { get; private set; }

        /// <summary>
        /// get - Number of nonalphabetic and nondigit characters.
        /// </summary>
        public int NonalphaDigitCharacters { get; private set; }

        /// <summary>
        /// get - Number of alphabetic characters.
        /// </summary>
        public int AlphaCharacters { get; private set; }

        /// <summary>
        /// get - Number of digit characters.
        /// </summary>
        public int DigitCharacters { get; private set; }

        /// <summary>
        /// get - Number of uppercase characters.
        /// </summary>
        public int UppercaseCharacters { get; private set; }

        /// <summary>
        /// get - Number of lowercase characters.
        /// </summary>
        public int LowercaseCharacters { get; private set; }
        #endregion

        #region Constructors
        internal PasswordStrength()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Evaluates the specified password and returns a new instance of a PasswordStrength object which provides the strength of the password.
        /// </summary>
        /// <param name="password">Password to evaluate.</param>
        /// <returns>New instance of a PasswordStrength object.</returns>
        public static PasswordStrength Evaluate(string password)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");

            int nonalpha = 0;
            int nonalphadigit = 0;
            int alpha = 0;
            int digit = 0;
            int upper = 0;
            int lower = 0;

            // Check each character and increment the various strength indicators.
            foreach (var c in password.ToArray())
            {
                if (Char.IsLetter(c))
                    alpha++;
                else
                    nonalpha++;
                if (Char.IsDigit(c))
                    digit++;
                if (Char.IsLower(c))
                    lower++;
                if (Char.IsUpper(c))
                    upper++;
                if (!Char.IsLetterOrDigit(c))
                    nonalphadigit++;
            }

            return new PasswordStrength()
            {
                Length = password.Length,
                NonalphaCharacters = nonalpha,
                NonalphaDigitCharacters = nonalphadigit,
                AlphaCharacters = alpha,
                DigitCharacters = digit,
                UppercaseCharacters = upper,
                LowercaseCharacters = lower
            };
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
