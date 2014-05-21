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
    /// 
    /// Initialize a new instance of AssertPassword with your password requirements, then use the Validate() method to validate any password.
    /// </summary>
    public sealed class AssertPassword
    {
        #region Variables
        private static readonly char[] _NonalphanumericCharacters = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        int _MinLength;
        int _MaxLength;
        int _MinRequiredNonalphanumericCharacters;
        int _MinRequiredLowerCaseCharacters;
        int _MinRequiredUpperCaseCharacters;
        int _MinRequiredNumericCharacters;
        int _MinRequireSpecialCharacters;
        string _PasswordStrengthRegularExpression;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new AssertPassword object.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'minLength', 'maxLength', 'minRequiredLowerCaseCharacters', 'minRequiredUpperCaseCharacters', 'minRequiredNumericCharacters', 'minRequiredSpecialCharacters' cannot be less than zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'maxLength' must be creater than the sum of the other minimum requirements.</exception>
        /// <param name="minLength">Minimum length of characters required.</param>
        /// <param name="maxLength">Maximum length of characters required.</param>
        /// <param name="minRequiredNonalphanumericCharacters">Minimum number of nonalphanumeric characters required.</param>
        /// <param name="minRequiredLowerCaseCharacters">Minimum number of lower-case characters required.</param>
        /// <param name="minRequiredUpperCaseCharacters">Minimum number of upper-case characters required.</param>
        public AssertPassword(int minLength, int maxLength, int minRequiredNonalphanumericCharacters, int minRequiredLowerCaseCharacters, int minRequiredUpperCaseCharacters)
            : this(AssertPassword.CreatePasswordStrengthRegularExpression(minLength, maxLength, minRequiredNonalphanumericCharacters, minRequiredLowerCaseCharacters, minRequiredUpperCaseCharacters))
        {
            Fosol.Common.Validation.Assert.MinRange(minLength, 0, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, 0, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minRequiredNonalphanumericCharacters, 0, "minRequiredNonalphanumericCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredLowerCaseCharacters, 0, "minRequiredLowerCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredUpperCaseCharacters, 0, "minRequiredUpperCaseCharacters");

            if (maxLength > 0)
            {
                Fosol.Common.Validation.Assert.MaxRange(minLength, maxLength, "minLength");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredUpperCaseCharacters, maxLength, "minRequiredUpperCaseCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredNonalphanumericCharacters + minRequiredLowerCaseCharacters + minRequiredUpperCaseCharacters, maxLength, "maxLength", "Parameter 'maxLength' is not large enough to accept your other minimum requirements.");
            }

            _MinLength = minLength;
            _MaxLength = maxLength;
            _MinRequiredNonalphanumericCharacters = minRequiredNonalphanumericCharacters;
            _MinRequiredLowerCaseCharacters = minRequiredLowerCaseCharacters;
            _MinRequiredUpperCaseCharacters = minRequiredUpperCaseCharacters;
        }

        /// <summary>
        /// Creates a new AssertPassword object.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'minLength', 'maxLength', 'minRequiredLowerCaseCharacters', 'minRequiredUpperCaseCharacters', 'minRequiredNumericCharacters', 'minRequiredSpecialCharacters' cannot be less than zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'maxLength' must be creater than the sum of the other minimum requirements.</exception>
        /// <param name="minLength">Minimum length of the password required.</param>
        /// <param name="maxLength">Maximum length of the password required.</param>
        /// <param name="minRequiredLowerCaseCharacters">Minimum number of lower-case characters required.</param>
        /// <param name="minRequiredUpperCaseCharacters">Minimum number of upper-case characters require.</param>
        /// <param name="minRequiredNumericCharacters">Minimum number of numeric characters required.</param>
        /// <param name="minRequiredSpecialCharacters">Minimum number of special characters required.</param>
        public AssertPassword(int minLength, int maxLength, int minRequiredLowerCaseCharacters, int minRequiredUpperCaseCharacters, int minRequiredNumericCharacters, int minRequiredSpecialCharacters)
            : this(AssertPassword.CreatePasswordStrengthRegularExpression(minLength, maxLength, minRequiredLowerCaseCharacters, minRequiredUpperCaseCharacters, minRequiredNumericCharacters, minRequiredSpecialCharacters))
        {
            Fosol.Common.Validation.Assert.MinRange(minLength, 0, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, 0, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minRequiredLowerCaseCharacters, 0, "minRequiredLowerCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredUpperCaseCharacters, 0, "minRequiredUpperCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredNumericCharacters, 0, "minRequiredNumericCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredSpecialCharacters, 0, "minRequiredSpecialCharacters");

            if (maxLength > 0)
            {
                Fosol.Common.Validation.Assert.MaxRange(minLength, maxLength, "minLength");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredUpperCaseCharacters, maxLength, "minRequiredUpperCaseCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredLowerCaseCharacters + minRequiredUpperCaseCharacters + minRequiredNumericCharacters + minRequiredSpecialCharacters, maxLength, "maxLength", "Parameter 'maxLength' is not large enough to accept your other minimum requirements.");
            }

            _MinLength = minLength;
            _MaxLength = maxLength;
            _MinRequiredLowerCaseCharacters = minRequiredLowerCaseCharacters;
            _MinRequiredUpperCaseCharacters = minRequiredUpperCaseCharacters;
            _MinRequiredNumericCharacters = minRequiredNumericCharacters;
            _MinRequireSpecialCharacters = minRequiredSpecialCharacters;
        }

        /// <summary>
        /// Creates a new AssertPassword object.
        /// </summary>
        /// <param name="passwordStrengthRegularExpression">Regular expression string to validate the password.</param>
        public AssertPassword(string passwordStrengthRegularExpression)
        {
            _PasswordStrengthRegularExpression = passwordStrengthRegularExpression;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validates the specified password to determine if it adheres to the configured requirements.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter 'password' cannot be null.</exception>
        /// <param name="password">Password string to test.</param>
        /// <returns>True if the password is valid.</returns>
        public bool Validate(string password)
        {
            Fosol.Common.Validation.Assert.IsNotNull(password, "password");

            return AssertPassword.Validate(password, _PasswordStrengthRegularExpression);
        }

        /// <summary>
        /// Validates the specified password to determine if it adheres to the specified regular expression.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'passwordStrengthRegularExpression' cannot be empty or white space.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'password', and 'passwordStrengthRegularExpression' cannot be null.</exception>
        /// <param name="password">Password string to test.</param>
        /// <param name="passwordStrengthRegularExpression">Regular expression to test the strength of the password.</param>
        /// <returns>True if the password if valid.</returns>
        public static bool Validate(string password, string passwordStrengthRegularExpression)
        {
            Fosol.Common.Validation.Assert.IsNotNull(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(passwordStrengthRegularExpression, "passwordStrengthRegularExpression");

            return Regex.IsMatch(password, passwordStrengthRegularExpression);
        }

        /// <summary>
        /// Creates a regular expression the validates the specified rules.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'minLength', 'maxLength', 'minRequiredLowerCaseCharacters', 'minRequiredUpperCaseCharacters', 'minRequiredNumericCharacters', 'minRequiredSpecialCharacters' cannot be less than zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'maxLength' must be creater than the sum of the other minimum requirements.</exception>
        /// <param name="minLength">Minimum length of the password required.</param>
        /// <param name="maxLength">Maximum length of the password required.</param>
        /// <param name="minRequiredLowerCaseCharacters">Minimum number of lower-case characters required.</param>
        /// <param name="minRequiredUpperCaseCharacters">Minimum number of upper-case characters require.</param>
        /// <param name="minRequiredNumericCharacters">Minimum number of numeric characters required.</param>
        /// <param name="minRequiredSpecialCharacters">Minimum number of special characters required.</param>
        /// <returns>A new regular expression string.</returns>
        public static string CreatePasswordStrengthRegularExpression(int minLength, int maxLength, int minRequiredLowerCaseCharacters, int minRequiredUpperCaseCharacters, int minRequiredNumericCharacters, int minRequiredSpecialCharacters)
        {
            Fosol.Common.Validation.Assert.MinRange(minLength, 0, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, 0, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minRequiredLowerCaseCharacters, 0, "minRequiredLowerCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredUpperCaseCharacters, 0, "minRequiredUpperCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredNumericCharacters, 0, "minRequiredNumericCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredSpecialCharacters, 0, "minRequiredSpecialCharacters");

            if (maxLength > 0)
            {
                Fosol.Common.Validation.Assert.MaxRange(minLength, maxLength, "minLength");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredUpperCaseCharacters, maxLength, "minRequiredUpperCaseCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredLowerCaseCharacters + minRequiredUpperCaseCharacters + minRequiredNumericCharacters + minRequiredSpecialCharacters, maxLength, "maxLength", "Parameter 'maxLength' is not large enough to accept your other minimum requirements.");
            }

            var expression = new StringBuilder();

            expression.Append("^");

            if (minRequiredLowerCaseCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredLowerCaseCharacters; i++)
                    expression.Append(".*[a-z]");
                expression.Append(")");
            }

            if (minRequiredUpperCaseCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredUpperCaseCharacters; i++)
                    expression.Append(".*[A-Z]");
                expression.Append(")");
            }

            if (minRequiredNumericCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredNumericCharacters; i++)
                    expression.Append(".*[0-9]");
                expression.Append(")");
            }

            if (minRequiredSpecialCharacters > 0)
            {
                expression.Append("(?=");
                var special = ".*[" + Regex.Escape(new String(_NonalphanumericCharacters)) + "]";
                for (var i = 0; i < minRequiredSpecialCharacters; i++)
                    expression.Append(special);
                expression.Append(")");
            }

            if (minLength > 0 && maxLength > 0)
                expression.Append(string.Format(".{{0}, {1}}", minLength, maxLength));
            else if (minLength > 0)
                expression.Append(string.Format(".{{0}}", minLength));
            else if (maxLength > 0)
                expression.Append(string.Format(".{{0}}", maxLength));

            expression.Append("$");

            return expression.ToString();
        }

        /// <summary>
        /// Creates a regular expression the validates the specified rules.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'minLength', 'maxLength', 'minRequiredNonalphanumericCharacters', 'minRequiredLowerCaseCharacters', 'minRequiredUpperCaseCharacters' cannot be less than zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters 'maxLength' must be creater than the sum of the other minimum requirements.</exception>
        /// <param name="minLength">Minimum length of the password required.</param>
        /// <param name="maxLength">Maximum length of the password required.</param>
        /// <param name="minRequiredNonalphanumericCharacters">Minimum number of nonalphanumeric characters required.</param>
        /// <param name="minRequiredLowerCaseCharacters">Minimum number of lower-case characters required.</param>
        /// <param name="minRequiredUpperCaseCharacters">Minimum number of upper-case characters require.</param>
        /// <returns>A new regular expression string.</returns>
        public static string CreatePasswordStrengthRegularExpression(int minLength, int maxLength, int minRequiredNonalphanumericCharacters, int minRequiredLowerCaseCharacters, int minRequiredUpperCaseCharacters)
        {
            Fosol.Common.Validation.Assert.MinRange(minLength, 0, "minLength");
            Fosol.Common.Validation.Assert.MinRange(maxLength, 0, "maxLength");
            Fosol.Common.Validation.Assert.MinRange(minRequiredNonalphanumericCharacters, 0, "minRequiredNonalphanumericCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredLowerCaseCharacters, 0, "minRequiredLowerCaseCharacters");
            Fosol.Common.Validation.Assert.MinRange(minRequiredUpperCaseCharacters, 0, "minRequiredUpperCaseCharacters");

            if (maxLength > 0)
            {
                Fosol.Common.Validation.Assert.MaxRange(minLength, maxLength, "minLength");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredNonalphanumericCharacters, maxLength, "minRequiredNonalphanumericCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredLowerCaseCharacters, maxLength, "minRequiredLowerCaseCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredUpperCaseCharacters, maxLength, "minRequiredUpperCaseCharacters");
                Fosol.Common.Validation.Assert.MaxRange(minRequiredNonalphanumericCharacters + minRequiredLowerCaseCharacters + minRequiredUpperCaseCharacters, maxLength, "maxLength", "Parameter 'maxLength' is not large enough to accept your other minimum requirements.");
            }

            var expression = new StringBuilder();

            expression.Append("^");

            if (minRequiredLowerCaseCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredLowerCaseCharacters; i++)
                    expression.Append(".*[a-z]");
                expression.Append(")");
            }

            if (minRequiredUpperCaseCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredUpperCaseCharacters; i++)
                    expression.Append(".*[A-Z]");
                expression.Append(")");
            }

            if (minRequiredNonalphanumericCharacters > 0)
            {
                expression.Append("(?=");
                for (var i = 0; i < minRequiredNonalphanumericCharacters; i++)
                    expression.Append(".*[0-9!@#$&*]");
                expression.Append(")");
            }

            if (minLength > 0 && maxLength > 0)
                expression.Append(string.Format(".{{0}, {1}}", minLength, maxLength));
            else if (minLength > 0)
                expression.Append(string.Format(".{{0}}", minLength));
            else if (maxLength > 0)
                expression.Append(string.Format(".{{0}}", maxLength));

            expression.Append("$");

            return expression.ToString();
        }

        /// <summary>
        /// Generates a random password that follows the specified rules.
        /// Also ensures that the password itself is not a dangerous cross site script.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parametes 'length' must be greater than or equal to one, and 'minRequiredNonalphanumericCharacters' must be greater than or equal to zero (as well as less than or equal to 'length').</exception>
        /// <param name="length">Length of the password you would like to create.</param>
        /// <param name="minRequiredNonalphanumericCharacters">Minimum number of nonalphanumeric characters to be included in the password.</param>
        /// <returns>A new password.</returns>
        public static string GeneratePassword(int length, int minRequiredNonalphanumericCharacters)
        {
            Fosol.Common.Validation.Assert.MinRange(length, 1, "length");
            Fosol.Common.Validation.Assert.MinRange(minRequiredNonalphanumericCharacters, 0, "minRequiredNonalphanumericCharacters");
            Fosol.Common.Validation.Assert.MaxRange(minRequiredNonalphanumericCharacters, length, "Parameter 'minRequiredNonalphanumericCharacters' must be less than or equal to 'length'.");

            string password;

            do
            {
                var random_bytes = new byte[length];
                var random_characters = new char[length];
                new RNGCryptoServiceProvider().GetBytes(random_bytes);

                var special_character_count = 0;
                for (var i = 0; i < length; i++)
                {
                    var value = (int)random_bytes[i] % 87;

                    if (value < 10)
                    {
                        random_characters[i] = (char)(48 + value);
                    }
                    else
                    {
                        if (value < 36)
                        {
                            random_characters[i] = (char)(65 + value - 10);
                        }
                        else
                        {
                            if (value < 62)
                            {
                                random_characters[i] = (char)(97 + value - 36);
                            }
                            else
                            {
                                random_characters[i] = _NonalphanumericCharacters[value - 62];
                                special_character_count++;
                            }
                        }
                    }
                }

                // Ensure there are enough nonalphanumeric characters in the password.
                // If there are not enough, replace some of the alphanumeric characters.
                if (special_character_count < minRequiredNonalphanumericCharacters)
                {
                    var random = new Random();
                    for (var i = 0; i < minRequiredNonalphanumericCharacters - special_character_count; i++)
                    {
                        int pos;
                        do
                        {
                            pos = random.Next(0, length);
                        }
                        while (!char.IsLetterOrDigit(random_characters[pos]));

                        // Replace 
                        random_characters[pos] = _NonalphanumericCharacters[random.Next(0, _NonalphanumericCharacters.Length)];
                    }
                }

                password = new String(random_characters);
            } while (Fosol.Common.Web.CrossSiteScriptingValidation.IsDangerousString(password));

            return password;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
