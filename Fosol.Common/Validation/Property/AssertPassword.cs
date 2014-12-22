using System;

namespace Fosol.Common.Validation.Property
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
        /// <exception cref="Exceptions.PropertyException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="Exceptions.PropertyNullException">Parameters 'password', and 'passwordStrengthRegularExpression' cannot be null.</exception>
        /// <param name="password">The value to check.</param>
        /// <param name="passwordStrengthRegularExpression">Regular expression to test the strength of the password.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, string passwordStrengthRegularExpression, string paramName, string message = null)
        {
            if (!Security.PasswordValidator.IsValid(password, passwordStrengthRegularExpression))
                throw new Exceptions.PropertyException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValidPassword, paramName));
        }

        /// <summary>
        /// Validates the specified password to determine if it adheres to the specified regular expression.
        /// </summary>
        /// <exception cref="Exceptions.PropertyException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="Exceptions.PropertyNullException">Parameters 'password', and 'requirements' cannot be null.</exception>
        /// <param name="password">The value to check.</param>
        /// <param name="requirements">PasswordRequirement object.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsValid(string password, Security.PasswordRequirement requirements, string paramName, string message = null)
        {
            Fosol.Common.Validation.Property.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Property.Assert.IsNotNull(requirements, "requirements");

            var util = new Security.PasswordValidator(requirements);
            if (!util.Validate(password))
                throw new Exceptions.PropertyException(paramName, String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValidPassword, paramName));
        }
        #endregion
    }
}
