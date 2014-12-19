using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Validation.Value
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
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, string passwordStrengthRegularExpression, string message = null)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(passwordStrengthRegularExpression, "passwordStrengthRegularExpression");

            if (!Security.PasswordValidator.IsValid(password, passwordStrengthRegularExpression))
                throw new Exceptions.ValueException(message ?? Resources.Multilingual.Exception_Validation_Value_IsValidPassword);
        }

        /// <summary>
        /// Validates the specified password to determine if it adheres to the specified regular expression.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'password', and 'requirements' cannot be null.</exception>
        /// <param name="password">The value to check.</param>
        /// <param name="requirements">PasswordRequirement object.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, Security.PasswordRequirement requirements, string message = null)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNull(requirements, "requirements");

            var util = new Security.PasswordValidator(requirements);
            if (!util.Validate(password))
                throw new Exceptions.ValueException(message ?? Resources.Multilingual.Exception_Validation_Value_IsValidPassword);
        }
        #endregion
    }
}
