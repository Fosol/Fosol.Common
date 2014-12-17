using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Validation.Argument
{
    /// <summary>
    /// AssertPassword static class provides a way to ensure passwords meet the specified requirements.
    /// </summary>
    public static class AssertPassword
    {
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
            if (!Security.PasswordUtility.IsValid(password, passwordStrengthRegularExpression))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsValidPassword, paramName));
        }

        /// <summary>
        /// Validates the specified password to determine if it adheres to the specified regular expression.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'password', and 'requirements' cannot be null.</exception>
        /// <param name="password">The value to check.</param>
        /// <param name="requirements">PasswordRequirement object.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, Security.PasswordRequirement requirements, string paramName, string message = null)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNull(requirements, "requirements");

            var util = new Security.PasswordUtility(requirements);
            if (!util.Validate(password))
                throw new ArgumentException(paramName, String.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsValidPassword, paramName));
        }
        #endregion
    }
}
