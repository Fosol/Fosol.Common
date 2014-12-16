using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common
{
    /// <summary>
    /// SpecialEnum abstract class provides a way to handle complex Enum operations.
    /// 
    /// Example usage below;
    /// public sealed class MyEnum : SpecialEnum<int, MyEnum>
    /// {
    ///     [Description("one")]
    ///     public static readonly MyEnum One = new MyEnum(1);
    ///     
    ///     [Description("two")]
    ///     public static readonly MyEnum Two = new MyEnum(2);
    ///     
    ///     [Description("three")]
    ///     public static readonly MyEnum Three = new MyEnum(3);
    ///     
    ///     private MyEnum(int value)
    ///         : base(value) {}
    ///         
    ///     public static implicit operator MyEnum(int value)
    ///     {
    ///         return Convert(value); }
    ///     }
    /// }
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TDerived"></typeparam>
    public abstract class SpecialEnum<TValue, TDerived>
        : IEquatable<TDerived>, IComparable<TDerived>, IComparable, IComparer<TDerived>
        where TValue : struct, IComparable<TValue>, IEquatable<TValue>
        where TDerived : SpecialEnum<TValue, TDerived>
    {
        #region Variables
        private readonly TValue _Value;
        private static readonly SortedList<TValue, TDerived> _Values = new SortedList<TValue, TDerived>();
        private string _Name;
        private DescriptionAttribute _DescriptionAttribute;
        #endregion

        #region Properties
        public TValue Value
        {
            get { return _Value; }
        }

        public static IEnumerable<TDerived> Values
        {
            get { return _Values.Values; }
        }

        public string Name
        {
            get { return _Name; }
        }

        public string Description
        {
            get
            {
                if (_DescriptionAttribute != null)
                    return _DescriptionAttribute.Description;

                return _Name;
            }
        }
        #endregion

        #region Constructors
        static SpecialEnum()
        {
            var fields = typeof(TDerived)
                .GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public)
                .Where(t => t.FieldType == typeof(TDerived));

            foreach (var field in fields)
            {
                field.GetValue(null);

                var instance = (TDerived)field.GetValue(null);
                instance._Name = field.Name;
                instance._DescriptionAttribute = field.GetCustomAttributes(true).OfType<DescriptionAttribute>().FirstOrDefault();
            }
        }

        protected SpecialEnum(TValue value)
        {
            _Value = value;
            _Values.Add(value, (TDerived)this);
        }
        #endregion

        #region Methods
        public static TDerived Convert(TValue value)
        {
            return _Values[value];
        }

        public static bool TryConvert(TValue value, out TDerived result)
        {
            return _Values.TryGetValue(value, out result);
        }

        public static implicit operator TValue(SpecialEnum<TValue, TDerived> value)
        {
            return value.Value;
        }

        public static implicit operator SpecialEnum<TValue, TDerived>(TValue value)
        {
            return _Values[value];
        }

        public static implicit operator TDerived(SpecialEnum<TValue, TDerived> value)
        {
            return value;
        }

        public override string ToString()
        {
            return _Name;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is TValue)
                    return Value.Equals((TValue)obj);

                if (obj is TDerived)
                    return _Values.Equals(((TDerived)obj).Value);
            }
            return false;
        }

        bool IEquatable<TDerived>.Equals(TDerived other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        int IComparable<TDerived>.CompareTo(TDerived other)
        {
            return this.Value.CompareTo(other.Value);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj != null)
            {
                if (obj is TValue)
                    return this.Value.CompareTo((TValue)obj);

                if (obj is TDerived)
                    return this.Value.CompareTo(((TDerived)obj).Value);
            }

            return -1;
        }

        int IComparer<TDerived>.Compare(TDerived x, TDerived y)
        {
            return (x == null) ? -1 : (y == null) ? 1 : x.Value.CompareTo(y.Value);
        }

        public static TDerived Parse(string name)
        {
            foreach (TDerived value in Values)
            {
                if (0 == String.Compare(value.Name, name, true))
                    return value;
            }

            return null;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
