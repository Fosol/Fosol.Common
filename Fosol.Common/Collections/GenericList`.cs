using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Collections.Generic;

namespace Fosol.Common.Collections
{
    /// <summary>
    /// This GenericList is a source code copy of the System.Collections.List object.
    /// 
    /// Implements a variable-size List that uses an array of objects to store the
    /// elements. A List has a capacity, which is the allocated length
    /// of the internal array. As elements are added to a List, the capacity
    /// of the List is automatically increased as required by reallocating the internal array. 
    /// </summary>
    /// <typeparam name="T">Object type this collection will hold.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [Serializable()]
    public class GenericList<T> : IList<T>, System.Collections.IList
    {
        #region Variables
        private const int _DefaultCapacity = 4;
        private T[] _Items;
        private int _Size;
        private int _Version;
        [NonSerialized]
        private Object _Lock;
        static T[] _EmptyArray = new T[0];
        #endregion

        #region Properties
        /// <summary>
        /// Gets and sets the capacity of this list.  The capacity is the size of
        /// the internal array used to hold items.  When set, the internal 
        /// array of the list is reallocated to the given capacity. 
        /// </summary>
        public int Capacity
        {
            get { return _Items.Length; }
            set
            {
                if (value != _Items.Length)
                {
                    Validation.Argument.Assert.MinRange(value, _Size, "Capacity");

                    if (value > 0)
                    {
                        var newItems = new T[value];
                        if (_Size > 0)
                            Array.Copy(_Items, 0, newItems, 0, _Size);
                        _Items = newItems;
                    }
                    else
                    {
                        _Items = _EmptyArray;
                    }

                }

            }

        }

        /// <summary>
        /// get - Read-only property describing how many elements are in the List. 
        /// </summary>
        public int Count
        {
            get { return _Size; }
        }

        /// <summary>
        /// get - Whether this collection is fixed size.
        /// </summary>
        bool System.Collections.IList.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// get - Is this List read-only?
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// get - Whether this collection is readonly.
        /// </summary>
        bool System.Collections.IList.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// get - Is this List synchronized (thread-safe)?
        /// </summary>
        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// get - Synchronization root for this object.
        /// </summary>
        Object System.Collections.ICollection.SyncRoot
        {
            get
            {
                if (_Lock == null)
                    System.Threading.Interlocked.CompareExchange(ref _Lock, new Object(), null);

                return _Lock;
            }
        }

        /// <summary>
        /// get/set - the element at the given index.
        /// </summary>
        /// <param name="index">Index position within the collection.</param>
        /// <returns>Object at the index position.</returns>
        public T this[int index]
        {
            get
            {
                Validation.Argument.Assert.MaxRange((uint)index, (uint)_Size - 1, "index");
                return _Items[index];
            }
            set
            {
                Validation.Argument.Assert.MaxRange((uint)index, (uint)_Size - 1, "index");
                _Items[index] = value;
                _Version++;
            }
        }

        /// <summary>
        /// get/set - Return object at index position.
        /// </summary>
        /// <param name="index">Index position within the collection.</param>
        /// <returns>Object at the index position.</returns>
        Object System.Collections.IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                VerifyValueType(value);
                this[index] = (T)value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a List. The list is initially empty and has a capacity 
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to 16, and then increased in multiples of two as required. 
        /// </summary>
        public GenericList()
        {
            _Items = _EmptyArray;
        }

        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements 
        /// before any reallocations are required.
        /// </summary>
        /// <param name="capacity"></param>
        public GenericList(int capacity)
        {
            Validation.Argument.Assert.MinRange(capacity, 0, "capacity");
            _Items = new T[capacity];
        }

        /// <summary>
        /// Constructs a List, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the given collection. 
        /// </summary>
        /// <param name="collection">Collection of objects.</param>
        public GenericList(IEnumerable<T> collection)
        {
            Validation.Argument.Assert.IsNotNull(collection, "collection");
            var c = collection as ICollection<T>;

            if (c != null)
            {
                int count = c.Count;
                _Items = new T[count];
                c.CopyTo(_Items, 0);
                _Size = count;
            }
            else
            {
                _Size = 0;
                _Items = new T[_DefaultCapacity];
                using (IEnumerator<T> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                        Add(en.Current);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks whether the oject is compatible with this collection type.
        /// </summary>
        /// <param name="value">Object to test.</param>
        /// <returns>True if compatible.</returns>
        private static bool IsCompatibleObject(object value)
        {
            if ((value is T) || (value == null && !typeof(T).IsValueType))
                return true;
            return false;
        }

        /// <summary>
        /// Verify that the type is compatible.
        /// </summary>
        /// <param name="value">Object to test.</param>
        private static void VerifyValueType(object value)
        {
            Validation.Argument.Assert.IsTrue(IsCompatibleObject(value), "value");
        }
        /// <summary>
        /// Adds the given object to the end of this list. The size of the list is 
        /// increased by one. If required, the capacity of the list is doubled
        /// before adding the new element. 
        /// </summary>
        /// <param name="item">The object to add.</param>
        public void Add(T item)
        {
            if (_Size == _Items.Length) EnsureCapacity(_Size + 1);
            _Items[_Size++] = item;
            _Version++;
        }

        /// <summary>
        /// Add the object to the collection.
        /// </summary>
        /// <param name="item">Item to add to the collection.</param>
        /// <returns>Number of items in the collection.</returns>
        int System.Collections.IList.Add(Object item)
        {
            VerifyValueType(item);
            Add((T)item);
            return Count - 1;
        }

        /// <summary>
        /// Adds the elements of the given collection to the end of this list. If 
        /// required, the capacity of the list is increased to twice the previous
        /// capacity or the new size, whichever is larger. 
        /// </summary>
        /// <param name="collection">Collection of objects to add to this collection.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            InsertRange(_Size, collection);
        }

        /// <summary>
        /// Return the collection as a ReadOnlyCollection.
        /// </summary>
        /// <returns>ReadOnlyCollection of type T.</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
        }

        /// <summary>
        /// Searches a section of the list for a given element using a binary search
        /// algorithm. Elements of the list are compared to the search value using
        /// the given IComparer interface. If comparer is null, elements of
        /// the list are compared to the search value using the IComparable 
        /// interface, which in that case must be implemented by all elements of the
        /// list and the given search value. This method assumes that the given 
        /// section of the list is already sorted; if this is not the case, the 
        /// result will be incorrect.
        /// 
        /// The method returns the index of the given value in the list. If the
        /// list does not contain the given value, the method returns a negative
        /// integer. The bitwise complement operator (~) can be applied to a
        /// negative result to produce the index of the first element (if any) that 
        /// is larger than the given search value. This is also the index at which
        /// the search value should be inserted into the list in order for the list 
        /// to remain sorted. 
        ///
        /// The method uses the Array.BinarySearch method to perform the 
        /// search.
        /// </summary>
        /// <param name="index">Index position within collection</param>
        /// <param name="count">Number of items to search through.</param>
        /// <param name="item">Object to search for.</param>
        /// <param name="comparer">Comparer object.</param>
        /// <returns>Index position of the item if found, or -1 if not found.</returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MinRange(_Size - index, count, "index");
            return Array.BinarySearch<T>(_Items, index, count, item, comparer);
        }

        /// <summary>
        /// Search for this item within the collection.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index position of the found item, or -1 if not found.</returns>
        public int BinarySearch(T item)
        {
            return BinarySearch(0, Count, item, null);
        }

        /// <summary>
        /// Search for this item within the collection.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <param name="comparer">Comparer object.</param>
        /// <returns>Index position of the found item, or -1 if not found.</returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return BinarySearch(0, Count, item, comparer);
        }

        /// <summary>
        /// Clears the contents of List.
        /// </summary>
        public void Clear()
        {
            if (_Size > 0)
            {
                Array.Clear(_Items, 0, _Size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references. 
                _Size = 0;
            }
            _Version++;
        }

        /// <summary>
        /// Contains returns true if the specified element is in the List. 
        /// It does a linear, O(n) search.  Equality is determined by calling 
        /// item.Equals().
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>True if object is found.</returns>
        public bool Contains(T item)
        {
            if ((Object)item == null)
            {
                for (int i = 0; i < _Size; i++)
                    if ((Object)_Items[i] == null)
                        return true;
                return false;
            }
            else
            {
                var c = EqualityComparer<T>.Default;
                for (int i = 0; i < _Size; i++)
                    if (c.Equals(_Items[i], item)) return true;
                return false;
            }
        }

        /// <summary>
        /// Contains returns true if the specified element is in the List. 
        /// It does a linear, O(n) search.  Equality is determined by calling 
        /// item.Equals().
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>True if object is found.</returns>
        bool System.Collections.IList.Contains(Object item)
        {
            if (IsCompatibleObject(item))
                return Contains((T)item);
            return false;
        }

        /// <summary>
        /// Convert collection of objects into another collection of the type touput.
        /// </summary>
        /// <typeparam name="toutput">Type of objects to convert to.</typeparam>
        /// <param name="converter">Converter object.</param>
        /// <returns>Collection of type toutput.</returns>
        public GenericList<toutput> ConvertAll<toutput>(Converter<T, toutput> converter)
        {
            Validation.Argument.Assert.IsNotNull(converter, "converter");

            var list = new GenericList<toutput>(_Size);
            for (int i = 0; i < _Size; i++)
                list._Items[i] = converter(_Items[i]);

            list._Size = _Size;
            return list;
        }

        /// <summary>
        /// Copies this List into array, which must be of a
        /// compatible array type.
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Copies this List into array, which must be of a 
        /// compatible array type.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Index position within array to start at.</param>
        void System.Collections.ICollection.CopyTo(Array array, int arrayIndex)
        {
            // Don't do multi-dimensional arrays.
            Validation.Argument.Assert.IsTrue((array != null) && (array.Rank == 1), "array");

            try
            {
                // Array.Copy will check for NULL. 
                Array.Copy(_Items, 0, array, arrayIndex, _Size);
            }
            catch (ArrayTypeMismatchException)
            {
                throw;
            }
        }

        /// <summary>
        /// Copies a section of this list to the given array at the given index.
        /// The method uses the Array.Copy method to copy the elements.
        /// </summary>
        /// <param name="index">Index position within source collection.</param>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Index position within array to start at.</param>
        /// <param name="count">Number of items to copy into array.</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            Validation.Argument.Assert.MinRange(_Size - index, count, "index");
            // Delegate rest of error checking to Array.Copy.
            Array.Copy(_Items, index, array, arrayIndex, count);
        }

        /// <summary>
        /// Copies a section of this list to the given array at the given index.
        /// The method uses the Array.Copy method to copy the elements.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Index position within the array to start at.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            // Delegate rest of error checking to Array.Copy. 
            Array.Copy(_Items, 0, array, arrayIndex, _Size);
        }

        /// <summary>
        /// Ensures that the capacity of this list is at least the given minimum
        /// value. If the currect capacity of the list is less than min, the 
        /// capacity is increased to twice the current capacity or to min,
        /// whichever is larger.
        /// </summary>
        /// <param name="min">Minimum capacity allowed.</param>
        private void EnsureCapacity(int min)
        {
            if (_Items.Length < min)
            {
                int newCapacity = _Items.Length == 0 ? _DefaultCapacity : _Items.Length * 2;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        /// <summary>
        /// Determine if item exists.
        /// </summary>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>True if item exists.</returns>
        public bool Exists(Predicate<T> match)
        {
            return FindIndex(match) != -1;

        }

        /// <summary>
        /// Search for the item that matches the predicate within the collection.
        /// </summary>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Object if found, or a default object of type T.</returns>
        public T Find(Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            for (int i = 0; i < _Size; i++)
            {
                if (match(_Items[i]))
                    return _Items[i];
            }
            return default(T);
        }

        /// <summary>
        /// Search for all the items that match the predicate in the collection.
        /// </summary>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Collection of objects found, or an empty collection.</returns>
        public GenericList<T> FindAll(Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            var list = new GenericList<T>();
            for (int i = 0; i < _Size; i++)
            {
                if (match(_Items[i]))
                    list.Add(_Items[i]);
            }
            return list;
        }

        /// <summary>
        /// Find index position that matches the predicate.
        /// </summary>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Index position if found, or -1 if not found.</returns>
        public int FindIndex(Predicate<T> match)
        {
            return FindIndex(0, _Size, match);
        }

        /// <summary>
        /// Find index position that matches the predicate.
        /// </summary>
        /// <param name="startIndex">Index position to start search at.</param>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Index position if found, or -1 if not found.</returns>
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return FindIndex(startIndex, _Size - startIndex, match);
        }

        /// <summary>
        /// Find index position that matches the predicate.
        /// </summary>
        /// <param name="startIndex">Index position to start search at.</param>
        /// <param name="count">Number of items to search through.</param>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Index position if found, or -1 if not found.</returns>
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            Validation.Argument.Assert.MaxRange((uint)startIndex, (uint)_Size, "startIndex");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MinRange(_Size - count, startIndex, "startIndex");
            Validation.Argument.Assert.IsNotNull(match, "match");
            int endIndex = startIndex + count;
            for (int i = startIndex; i < endIndex; i++)
                if (match(_Items[i])) return i;
            return -1;
        }

        /// <summary>
        /// Find last object that matches the predicate in the collectition.
        /// </summary>
        /// <param name="match">Predicate expression to use.</param>
        /// <returns>Object if found, or default object if not found.</returns>
        public T FindLast(Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            for (int i = _Size - 1; i >= 0; i--)
            {
                if (match(_Items[i]))
                    return _Items[i];
            }
            return default(T);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return FindLastIndex(_Size - 1, _Size, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return FindLastIndex(startIndex, startIndex + 1, match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            if (_Size == 0)
                // Special case for 0 length List 
                Validation.Argument.Assert.Range(startIndex, -1, -1, "startIndex");
            else
                // Make sure we're not out of range 
                Validation.Argument.Assert.MaxRange((uint)startIndex, (uint)_Size, "startIndex");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            // 2nd have of this also catches when startIndex == MAXINT, so MAXINT - 0 + 1 == -1, which is < 0.
            Validation.Argument.Assert.MinRange(startIndex - count + 1, 0, "startIndex");

            int endIndex = startIndex - count;
            for (int i = startIndex; i > endIndex; i--)
            {
                if (match(_Items[i]))
                    return i;
            }
            return -1;
        }

        public void ForEach(Action<T> action)
        {
            Validation.Argument.Assert.IsNotNull(action, "action");
            for (int i = 0; i < _Size; i++)
                action(_Items[i]);
        }

        // Returns an enumerator for this list with the given
        // permission for removal of elements. If modifications made to the list 
        // while an enumeration is in progress, the MoveNext and
        // GetObject methods of the enumerator will throw an exception.
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <internalonly>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public GenericList<T> GetRange(int index, int count)
        {
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MinRange(_Size - index, count, "index");

            var list = new GenericList<T>(count);
            Array.Copy(_Items, index, list._Items, 0, count);
            list._Size = count;
            return list;
        }

        // Returns the index of the first occurrence of a given value in a range of 
        // this list. The list is searched forwards from beginning to end. 
        // The elements of the list are compared to the given value using the
        // Object.Equals method. 
        //
        // This method uses the Array.IndexOf method to perform the
        // search.
        // 
        public int IndexOf(T item)
        {
            return Array.IndexOf(_Items, item, 0, _Size);
        }

        int System.Collections.IList.IndexOf(Object item)
        {
            if (IsCompatibleObject(item))
                return IndexOf((T)item);
            return -1;
        }

        // Returns the index of the first occurrence of a given value in a range of
        // this list. The list is searched forwards, starting at index 
        // index and ending at count number of elements. The
        // elements of the list are compared to the given value using the
        // Object.Equals method.
        // 
        // This method uses the Array.IndexOf method to perform the
        // search. 
        // 
        public int IndexOf(T item, int index)
        {
            Validation.Argument.Assert.MaxRange(index, _Size, "index");
            return Array.IndexOf(_Items, item, index, _Size - index);
        }

        // Returns the index of the first occurrence of a given value in a range of
        // this list. The list is searched forwards, starting at index 
        // index and upto count number of elements. The 
        // elements of the list are compared to the given value using the
        // Object.Equals method. 
        //
        // This method uses the Array.IndexOf method to perform the
        // search.
        public int IndexOf(T item, int index, int count)
        {
            Validation.Argument.Assert.MaxRange(index, _Size, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MaxRange(index, _Size - count, "count");
            return Array.IndexOf(_Items, item, index, count);
        }

        // Inserts an element into this list at a given index. The size of the list
        // is increased by one. If required, the capacity of the list is doubled 
        // before inserting the new element. 
        public void Insert(int index, T item)
        {
            // Note that insertions at the end are legal.
            Validation.Argument.Assert.MaxRange((uint)index, (uint)_Size, "index");
            if (_Size == _Items.Length) EnsureCapacity(_Size + 1);
            if (index < _Size)
            {
                Array.Copy(_Items, index, _Items, index + 1, _Size - index);
            }
            _Items[index] = item;
            _Size++;
            _Version++;
        }

        void System.Collections.IList.Insert(int index, Object item)
        {
            VerifyValueType(item);
            Insert(index, (T)item);
        }

        // Inserts the elements of the given collection at a given index. If
        // required, the capacity of the list is increased to twice the previous
        // capacity or the new size, whichever is larger.  Ranges may be added 
        // to the end of the list by setting index to the List's size.
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            Validation.Argument.Assert.IsNotNull(collection, "collection");
            Validation.Argument.Assert.MaxRange((uint)index, (uint)_Size, "index");
            var c = collection as ICollection<T>;
            if (c != null)
            {    // if collection is ICollection<t>
                int count = c.Count;
                if (count > 0)
                {
                    EnsureCapacity(_Size + count);
                    if (index < _Size)
                    {
                        Array.Copy(_Items, index, _Items, index + count, _Size - index);
                    }
                    // If we're inserting a List into itself, we want to be able to deal with that. 
                    if (this == c)
                    {
                        // Copy first part of _Items to insert location 
                        Array.Copy(_Items, 0, _Items, index, index);
                        // Copy last part of _Items back to inserted location
                        Array.Copy(_Items, index + count, _Items, index * 2, _Size - index);
                    }
                    else
                    {
                        T[] itemsToInsert = new T[count];
                        c.CopyTo(itemsToInsert, 0);
                        itemsToInsert.CopyTo(_Items, index);
                    }
                    _Size += count;
                }
            }
            else
            {
                using (IEnumerator<T> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                        Insert(index++, en.Current);
                }
            }
            _Version++;
        }

        // Returns the index of the last occurrence of a given value in a range of
        // this list. The list is searched backwards, starting at the end 
        // and ending at the first element in the list. The elements of the list 
        // are compared to the given value using the Object.Equals method.
        // 
        // This method uses the Array.LastIndexOf method to perform the
        // search.
        public int LastIndexOf(T item)
        {
            return LastIndexOf(item, _Size - 1, _Size);
        }

        // Returns the index of the last occurrence of a given value in a range of 
        // this list. The list is searched backwards, starting at index
        // index and ending at the first element in the list. The
        // elements of the list are compared to the given value using the
        // Object.Equals method. 
        //
        // This method uses the Array.LastIndexOf method to perform the 
        // search. 
        public int LastIndexOf(T item, int index)
        {
            Validation.Argument.Assert.MaxRange(index, _Size - 1, "index");
            return LastIndexOf(item, index, index + 1);
        }

        // Returns the index of the last occurrence of a given value in a range of 
        // this list. The list is searched backwards, starting at index
        // index and upto count elements. The elements of 
        // the list are compared to the given value using the Object.Equals
        // method.
        //
        // This method uses the Array.LastIndexOf method to perform the 
        // search.
        public int LastIndexOf(T item, int index, int count)
        {
            if (_Size == 0)
                return -1;
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MaxRange(index, _Size - 1, "index");
            Validation.Argument.Assert.MaxRange(count, index + 1, "count");
            return Array.LastIndexOf(_Items, item, index, count);
        }

        // Removes the element at the given index. The size of the list is
        // decreased by one. 
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        void System.Collections.IList.Remove(Object item)
        {
            if (IsCompatibleObject(item))
                Remove((T)item);
        }

        // This method removes all items which matches the predicate. 
        // The complexity is O(n).
        public int RemoveAll(Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            int freeIndex = 0;   // the first free slot in items array
            // Find the first item which needs to be removed. 
            while (freeIndex < _Size && !match(_Items[freeIndex])) freeIndex++;
            if (freeIndex >= _Size) return 0;
            int current = freeIndex + 1;
            while (current < _Size)
            {
                // Find the first item which needs to be kept. 
                while (current < _Size && match(_Items[current])) current++;
                // copy item to the free slot.
                if (current < _Size)
                    _Items[freeIndex++] = _Items[current++];
            }
            Array.Clear(_Items, freeIndex, _Size - freeIndex);
            int result = _Size - freeIndex;
            _Size = freeIndex;
            _Version++;
            return result;
        }

        // Removes the element at the given index. The size of the list is
        // decreased by one.
        public void RemoveAt(int index)
        {
            Validation.Argument.Assert.MaxRange((uint)index, (uint)_Size - 1, "index");
            _Size--;
            if (index < _Size)
                Array.Copy(_Items, index + 1, _Items, index, _Size - index);
            _Items[_Size] = default(T);
            _Version++;
        }

        // Removes a range of elements from this list.
        public void RemoveRange(int index, int count)
        {
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MaxRange(count, _Size - index, "count");
            if (count > 0)
            {
                int i = _Size;
                _Size -= count;
                if (index < _Size)
                    Array.Copy(_Items, index + count, _Items, index, _Size - index);
                Array.Clear(_Items, _Size, count);
                _Version++;
            }
        }

        // Reverses the elements in this list.
        public void Reverse()
        {
            Reverse(0, Count);
        }

        // Reverses the elements in a range of this list. Following a call to this 
        // method, an element in the range given by index and count
        // which was previously located at index i will now be located at 
        // index index + (index + count - i - 1).
        //
        // This method uses the Array.Reverse method to reverse the
        // elements. 
        public void Reverse(int index, int count)
        {
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MaxRange(count, _Size - index, "count");
            Array.Reverse(_Items, index, count);
            _Version++;
        }

        // Sorts the elements in this list.  Uses the default comparer and
        // Array.Sort. 
        public void Sort()
        {
            Sort(0, Count, null);
        }

        // Sorts the elements in this list.  Uses Array.Sort with the 
        // provided comparer. 
        public void Sort(IComparer<T> comparer)
        {
            Sort(0, Count, comparer);
        }

        // Sorts the elements in a section of this list. The sort compares the 
        // elements to each other using the given IComparer interface. If
        // comparer is null, the elements are compared to each other using 
        // the IComparable interface, which in that case must be implemented by all 
        // elements of the list.
        // 
        // This method uses the Array.Sort method to sort the elements.
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            Validation.Argument.Assert.MinRange(index, 0, "index");
            Validation.Argument.Assert.MinRange(count, 0, "count");
            Validation.Argument.Assert.MaxRange(count, _Size - index, "count");
            Array.Sort<T>(_Items, index, count, comparer);
            _Version++;
        }

        /// <summary>
        /// Sort this collection with the specified comparison object.
        /// </summary>
        /// <param name="comparison">Comparison object of type T.</param>
        public void Sort(Comparison<T> comparison)
        {
            Validation.Argument.Assert.IsNotNull(comparison, "comparison");
            if (_Size > 0)
            {
                IComparer<T> comparer = new FunctorComparer<T>(comparison);
                Array.Sort(_Items, 0, _Size, comparer);
            }
        }

        // ToArray returns a new Object array containing the contents of the List.
        // This requires copying the List, which is an O(n) operation. 
        public T[] ToArray()
        {
            var array = new T[_Size];
            Array.Copy(_Items, 0, array, 0, _Size);
            return array;
        }

        // Sets the capacity of this list to the size of the list. This method can 
        // be used to minimize a list's memory overhead once it is known that no
        // new elements will be added to the list. To completely clear a list and 
        // release all memory referenced by the list, execute the following
        // statements:
        //
        // list.Clear(); 
        // list.TrimExcess();
        public void TrimExcess()
        {
            int threshold = (int)(((double)_Items.Length) * 0.9);
            if (_Size < threshold)
                Capacity = _Size;
        }

        public bool TrueForAll(Predicate<T> match)
        {
            Validation.Argument.Assert.IsNotNull(match, "match");
            for (int i = 0; i < _Size; i++)
            {
                if (!match(_Items[i]))
                    return false;
            }
            return true;
        } 
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion

        [Serializable()] 
        public struct Enumerator : IEnumerator<T>, System.Collections.IEnumerator
        {
            private GenericList<T> list;
            private int index; 
            private int version;
            private T current;

            internal Enumerator(GenericList<T> list) 
            {
                this.list = list; 
                index = 0;
                version = list._Version;
                current = default(T);
            } 

            public void Dispose() 
            { 
            } 

            public bool MoveNext() 
            {
                var localList = list;

                if (version == localList._Version && ((uint)index < (uint)localList._Size)) 
                {
                    current = localList._Items[index]; 
                    index++; 
                    return true;
                } 
                return MoveNextRare();
            }

            private bool MoveNextRare() 
            {
                if (version != list._Version)
                    throw new InvalidOperationException();

                index = list._Size + 1;
                current = default(T);
                return false;
            } 

            public T Current 
            { 
                get 
                { 
                    return current;
                } 
            }

            Object System.Collections.IEnumerator.Current 
            {
                get 
                { 
                    if( index == 0 || index == list._Size + 1) 
                        throw new InvalidOperationException();
                    return Current;
                } 
            }

            void System.Collections.IEnumerator.Reset() 
            {
                if (version != list._Version) 
                    throw new InvalidOperationException();

                index = 0;
                current = default(T); 
            }
        }
    } 
}