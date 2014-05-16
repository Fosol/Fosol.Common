using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;

namespace Fosol.Common.Web.Security
{
    class FosolMembershipProvider
        : MembershipProvider
    {
        #region Variables
        string _ApplicationName;
        bool _EnablePasswordReset;
        bool _EnablePasswordRetrieval;
        int _MaxInvalidPasswordAttempts;
        int _MinRequiredNonAlphanumericCharacters;
        int _MinRequiredPasswordLength;
        int _PasswordAttemptWindow;
        MembershipPasswordFormat _PasswordFormat;
        string _PasswordStrengthRegularExpression;
        bool _RequiresQuestionAndAnswer;
        bool _RequiresUniqueEmail;
        int _CommandTimeout;
        MembershipPasswordCompatibilityMode _LegacyPasswordCompatibilityMode;
        #endregion

        #region Properties
        public override string ApplicationName
        {
            get { return _ApplicationName; }
            set { _ApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            Validation.Assert.IsNotNull(config, "config");

            Initialization.Assert.IsNotDefault(ref name, "FosolMembershipProvider");

            base.Initialize(name, config);

            _EnablePasswordRetrieval = Initialization.Configuration.GetValue(config, "enablePasswordRetrieval", false);
            _EnablePasswordReset = Initialization.Configuration.GetValue(config, "enablePasswordReset", true);
            _RequiresQuestionAndAnswer = Initialization.Configuration.GetValue(config, "requiresQuestionAndAnswer", true);
            _RequiresUniqueEmail = Initialization.Configuration.GetValue(config, "requiresUniqueEmail", true);
            _MaxInvalidPasswordAttempts = Initialization.Configuration.GetValue(config, "maxInvalidPasswordAttempts", 5, 1, null);
            _PasswordAttemptWindow = Initialization.Configuration.GetValue(config, "passwordAttemptWindow", 10, 1, null);
            _MinRequiredPasswordLength = Initialization.Configuration.GetValue(config, "minRequiredPasswordLength", 7, 1, 128);
            _MinRequiredNonAlphanumericCharacters = Initialization.Configuration.GetValue(config, "minRequiredNonAlphanumericCharacters", 1, 0, 128);
            _PasswordStrengthRegularExpression = config["passwordStrengthRegularExpression"];

            if (_PasswordStrengthRegularExpression != null)
            {
                _PasswordStrengthRegularExpression = _PasswordStrengthRegularExpression.Trim();

                if (_PasswordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        new Regex(_PasswordStrengthRegularExpression);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new Exceptions.ConfigurationException(ex.Message, ex);
                    }
                }
                else
                    _PasswordStrengthRegularExpression = string.Empty;
            }

            if (_MinRequiredNonAlphanumericCharacters > _MinRequiredPasswordLength)
                throw new Exceptions.ConfigurationException("MinRequiredPasswordLength cannot be less than MinRequiredNonAlphanumericCharacters.");

            _CommandTimeout = Initialization.Configuration.GetValue(config, "commandTimeout", 30, 0, null);
            _ApplicationName = Initialization.Configuration.GetValue(config, "applicationName", Helpers.WebApplicationHelper.GetApplicationName);

            if (_ApplicationName.Length > 256)
                throw new Exceptions.ConfigurationException("Provider application name is too long.");

            var password_format = config["passwordFormat"];
            if (password_format == null)
                password_format = "Hashed";

            if (password_format != "Clear")
            {
                if (password_format != "Encrypted")
                {
                    if (password_format != "Hashed")
                        throw new Exceptions.ConfigurationException("Provider has an invalid password format.");

                    _PasswordFormat = MembershipPasswordFormat.Hashed;

                    if (_EnablePasswordRetrieval)
                        throw new Exceptions.ConfigurationException("Provider cannot retrieve hashed password.");
                }
                else
                    _PasswordFormat = MembershipPasswordFormat.Encrypted;
            }
            else
            {
                _PasswordFormat = MembershipPasswordFormat.Clear;
            }

            var password_compat_mode = config["passwordCompatMode"];
            if (!string.IsNullOrEmpty(password_compat_mode))
                _LegacyPasswordCompatibilityMode = (MembershipPasswordCompatibilityMode)Enum.Parse(typeof(MembershipPasswordCompatibilityMode), password_compat_mode);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion

    }
}
