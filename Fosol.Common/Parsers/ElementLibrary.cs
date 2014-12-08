using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// Contains a dictionary of currently configured FormatElement objects.
    /// </summary>
    public static class ElementLibrary
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
        static ElementLibrary()
        {
            Initialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize what FormatElement types are in the library.
        /// </summary>
        private static void Initialize()
        {
            foreach (var type in GetKeywordTypes(Assembly.GetCallingAssembly(), typeof(Fosol.Common.Parsers.Elements.TextElement).Namespace))
            {
                Add(type);
            }
        }

        /// <summary>
        /// Refresh the library so that it only contains the default FormatElement types.
        /// </summary>
        public static void Refresh()
        {
            _Cache.Clear();
            Initialize();
        }

        /// <summary>
        /// Add the FormatElement Type to the library.
        /// </summary>
        /// <param name="type">FormatElement Type.</param>
        /// <returns>Number of items in library.</returns>
        public static int Add(Type type)
        {
            Common.Validation.Assert.IsNotNull(type, "type");
            Common.Validation.Assert.IsAssignable(type, typeof(FormatElement), "type");
            Common.Validation.Assert.HasAttribute(type, typeof(ElementAttribute), "type");

            var attr = type.GetCustomAttribute(typeof(ElementAttribute)) as ElementAttribute;
            if (_Cache.ContainsKey(attr.Name))
            {
                if (attr.Override)
                    _Cache.Remove(attr.Name);
                else
                    throw new InvalidOperationException(string.Format(Resources.Multilingual.Exception_FormatElement_Already_Exists, attr.Name));
            }

            _Cache.Add(attr.Name, type);
            return _Cache.Count;
        }

        /// <summary>
        /// Add all the FormatElement Types within the specified Assembly and Namespace.
        /// </summary>
        /// <param name="assemblyString">Fully qualified name of the assembly.</param>
        /// <param name="nameOrNamespace">Namespace or fully qualified name to the FormatElement(s).</param>
        /// <returns>Number of items in library.</returns>
        public static int Add(string assemblyString, string nameOrNamespace)
        {
            Common.Validation.Assert.IsNotNullOrEmpty(assemblyString, "assemblyString");
            Common.Validation.Assert.IsNotNullOrEmpty(nameOrNamespace, "nameOrNamespace");

            var assembly = Assembly.Load(assemblyString);
            if (assembly == null)
                throw new InvalidOperationException(string.Format(Resources.Multilingual.Exception_Assembly_Is_Invalid, assemblyString));

            return Add(assembly, nameOrNamespace);
        }

        /// <summary>
        /// Add all the FormatElement Types within the specified Assembly and Namespace.
        /// </summary>
        /// <param name="assembly">Assembly containing FormatElement(s).</param>
        /// <param name="nameOrNamespace">Namespace or fully qualified name to the FormatElement(s).</param>
        /// <returns>Number of items in library.</returns>
        public static int Add(Assembly assembly, string nameOrNamespace)
        {
            // Fetch every FormatElement in the specified namespacePath.
            foreach (var type in GetKeywordTypes(assembly, nameOrNamespace))
            {
                Add(type);
            }

            return _Cache.Count;
        }

        /// <summary>
        /// Fetch all the FormatElement Type objects in the specified Assembly and Namespace.
        /// </summary>
        /// <param name="assembly">Assembly containing FormatElement objects.</param>
        /// <param name="nameOrNamespace">Namespace or fully qualified name to the FormatElement(s).</param>
        /// <returns>Collection of FormatElement Types.</returns>
        static IEnumerable<Type> GetKeywordTypes(Assembly assembly, string nameOrNamespace)
        {
            var type = GetKeywordType(assembly, nameOrNamespace);
            if (type != null)
                return new List<Type>() { type };

            // The keywordNamespace is only a path to possibly numerous FormatElement objects.
            return (
                from t in assembly.GetTypes()
                where String.Equals(t.Namespace, nameOrNamespace, StringComparison.Ordinal)
                    && typeof(FormatElement).IsAssignableFrom(t)
                    && t.HasAttribute(typeof(ElementAttribute))
                select t);
        }

        /// <summary>
        /// Checks to see if the fullyQualifiedTypeName is of Type FormatElement.
        /// </summary>
        /// <param name="assembly">Assembly containing FormatElement objects.</param>
        /// <param name="fullyQualifiedTypeName">Fully qualified name of the FormatElement.</param>
        /// <returns>FormatElement Type, or null if the fullyQualifiedTypeName was only a namespace.</returns>
        static Type GetKeywordType(Assembly assembly, string fullyQualifiedTypeName)
        {
            var type = assembly.GetType(fullyQualifiedTypeName, false);

            // The keywordNamespace pointed directly to a Type.
            // The keywordNamespace is a valid type.
            // And it has been marked with the KeywordAttribute.
            if (type != null)
            {
                if (typeof(FormatElement).IsAssignableFrom(type)
                    && type.HasAttribute(typeof(ElementAttribute)))
                    return type;

                throw new InvalidOperationException(string.Format(Resources.Multilingual.Exception_FormatElement_Is_Not_Valid, fullyQualifiedTypeName));
            }

            return null;
        }

        /// <summary>
        /// Get the Keyword Type for the specified key name.
        /// First it will check the library for an existing FormatElement.
        /// Then it will check if the executing assembly contains a FormatElement with the specified name.
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
            // The cache contains the FormatElement so return it.
            if (_Cache.ContainsKey(name))
                type = _Cache[name].Value;

            // Check if the name is a fully qualified type name in the executing assembly.
            if (type == null)
                type = GetKeywordType(Assembly.GetEntryAssembly(), name);
            if (type == null)
                type = GetKeywordType(Assembly.GetCallingAssembly(), name);
            if (type == null)
                type = GetKeywordType(Assembly.GetExecutingAssembly(), name);
            if (type == null)
                throw new InvalidOperationException(string.Format(Resources.Multilingual.Exception_FormatElement_Does_Not_Exist, name));
            return type;
        }

        /// <summary>
        /// Checks to see if the library contains the FormatElement with the specified name.
        /// </summary>
        /// <param name="name">Name of FormatElement.</param>
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
