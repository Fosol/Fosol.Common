using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Validation
{
    /// <summary>
    /// AssertPassword provides a way to ensure passwords meet the specified requirements.
    /// </summary>
    public static class AssertPassword
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Validates the specified password to determine if it adheres to the specified regular expression.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'password', and 'passwordStrengthRegularExpression' cannot be null.</exception>
        /// <param name="password">The value to check.</param>
        /// <param name="passwordStrengthRegularExpression">Regular expression to test the strength of the password.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, string passwordStrengthRegularExpression, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!Utilities.PasswordUtility.IsValid(password, passwordStrengthRegularExpression))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_AssertPassword_IsValid, paramName));
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
