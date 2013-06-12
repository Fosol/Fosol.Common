using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// Contains a dictionary of currently configured FormatKeyword objects.
    /// </summary>
    public static class FormatKeywordLibrary
    {
        #region Variables
        private static readonly Caching.SimpleCache<Type> _Cache = new Caching.SimpleCache<Type>();
        #endregion

        #region Properties
        /// <summary>
        /// get - An array of key names within the library.
        /// </summary>
        public static string[] Keys
        {
            get
            {
                return _Cache.Keys.ToArray();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the library with 
        /// The Logger cannot use a keyword unless it's been registered.
        /// </summary>
        static FormatKeywordLibrary()
        {
            foreach (var type in GetKeywordTypes(Assembly.GetCallingAssembly(), typeof(FormatKeyword).Namespace))
            {
                Add(type);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add the FormatKeywordBase Type to the library.
        /// </summary>
        /// <param name="keywordType">FormatKeywordBase Type.</param>
        /// <returns>Number of items in library.</returns>
        public static int Add(Type keywordType)
        {
            Common.Validation.Assert.IsNotNull(keywordType, "keywordType");
            Common.Validation.Assert.IsAssignableFromType(keywordType, typeof(FormatKeyword), "keywordType");
            Common.Validation.Assert.HasAttribute(keywordType, typeof(FormatKeywordAttribute), "keywordType");

            var attr = keywordType.GetCustomAttribute(typeof(FormatKeywordAttribute)) as FormatKeywordAttribute;
            if (_Cache.ContainsKey(attr.Name))
            {
                if (attr.Override)
                    _Cache.Remove(attr.Name);
                else
                    throw new InvalidOperationException(string.Format(Resources.Strings.Exception_FormatKeyword_Already_Exists, attr.Name));
            }

            _Cache.Add(attr.Name, keywordType);
            return _Cache.Count;
        }

        /// <summary>
        /// Add all the FormatKeywordBase Types within the specified Assembly and Namespace.
        /// </summary>
        /// <param name="assemblyString">Fully qualified name of the assembly.</param>
        /// <param name="nameOrNamespace">Namespace or fully qualified name to the FormatKeywordBase(s).</param>
        /// <returns>Number of items in library.</returns>
        public static int Add(string assemblyString, string nameOrNamespace)
        {
            Common.Validation.Assert.IsNotNullOrEmpty(assemblyString, "assemblyString");
            Common.Validation.Assert.IsNotNullOrEmpty(nameOrNamespace, "nameOrNamespace");

            var assembly = Assembly.Load(assemblyString);
            if (assembly == null)
                throw new InvalidOperationException(string.Format(Resources.Strings.Exception_Assembly_Is_Invalid, assemblyString));

            // Fetch every FormatKeywordBase in the specified namespacePath.
            foreach (var type in GetKeywordTypes(assembly, nameOrNamespace))
            {
                Add(type);
            }

            return _Cache.Count;
        }

        /// <summary>
        /// Fetch all the FormatKeywordBase Type objects in the specified Assembly and Namespace.
        /// </summary>
        /// <param name="assembly">Assembly containing FormatKeywordBase objects.</param>
        /// <param name="nameOrNamespace">Namespace or fully qualified name to the FormatKeywordBase(s).</param>
        /// <returns>Collection of FormatKeywordBase Types.</returns>
        static IEnumerable<Type> GetKeywordTypes(Assembly assembly, string nameOrNamespace)
        {
            var type = GetKeywordType(assembly, nameOrNamespace);
            if (type != null)
                return new List<Type>() { type };

            // The keywordNamespace is only a path to possibly numerous FormatKeywordBase objects.
            return (
                from t in assembly.GetTypes()
                where String.Equals(t.Namespace, nameOrNamespace, StringComparison.Ordinal)
                    && typeof(FormatKeyword).IsAssignableFrom(t)
                    && t.HasAttribute(typeof(FormatKeywordAttribute))
                select t);
        }

        /// <summary>
        /// Checks to see if the fullyQualifiedTypeName is of Type FormatKeywordBase.
        /// </summary>
        /// <param name="assembly">Assembly containing FormatKeywordBase objects.</param>
        /// <param name="fullyQualifiedTypeName">Fully qualified name of the FormatKeywordBase.</param>
        /// <returns>FormatKeywordBase Type, or null if the fullyQualifiedTypeName was only a namespace.</returns>
        static Type GetKeywordType(Assembly assembly, string fullyQualifiedTypeName)
        {
            var type = assembly.GetType(fullyQualifiedTypeName, false);

            // The keywordNamespace pointed directly to a Type.
            // The keywordNamespace is a valid type.
            // And it has been marked with the KeywordAttribute.
            if (type != null)
            {
                if (typeof(FormatKeyword).IsAssignableFrom(type)
                    && type.HasAttribute(typeof(FormatKeywordAttribute)))
                    return type;

                throw new InvalidOperationException(string.Format(Resources.Strings.Exception_FormatKeyword_Is_Not_Valid, fullyQualifiedTypeName));
            }

            return null;
        }

        /// <summary>
        /// Get the Keyword Type for the specified key name.
        /// First it will check the library for an existing FormatKeywordBase.
        /// Then it will check if the executing assembly contains a FormatKeywordBase with the specified name.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "typeName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "typeName" cannot be null.</exception>
        /// <exception cref="System.InvalidOperation">Type must exist.</exception>
        /// <param name="name">Unique name to identify the Target keyword.</param>
        /// <returns>Type of Target.</returns>
        public static Type Get(string name)
        {
            Common.Validation.Assert.IsNotNullOrEmpty(name, "name");

            Type type = null;
            // The cache contains the LogTarget so return it.
            if (_Cache.ContainsKey(name))
                type = _Cache[name];

            // Check if the name is a fully qualified type name in the executing assembly.
            if (type == null)
                type = GetKeywordType(Assembly.GetEntryAssembly(), name);
            if (type == null)
                type = GetKeywordType(Assembly.GetCallingAssembly(), name);
            if (type == null)
                type = GetKeywordType(Assembly.GetExecutingAssembly(), name);
            if (type == null)
                throw new InvalidOperationException(string.Format(Resources.Strings.Exception_FormatKeyword_Does_Not_Exist, name));
            return type;
        }

        /// <summary>
        /// Checks to see if the library contains the FormatKeywordBase with the specified name.
        /// </summary>
        /// <param name="name">Name of FormatKeywordBase.</param>
        /// <returns>True if exists.</returns>
        public static bool ContainsKey(string name)
        {
            return _Cache.ContainsKey(name);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
