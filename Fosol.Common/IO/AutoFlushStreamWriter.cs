using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fosol.Common.IO
{
    /// <summary>
    /// The AutoFlushStreamWriter adds additional behavior to the StreamWriter.
    /// It provides a way to auto flush the buffer once it reaches or exceeds the AutoFlushSize.
    /// * Not thread safe *
    /// </summary>
    public sealed class AutoFlushStreamWriter
        : System.IO.StreamWriter
    {
        #region Variables
        private int _BufferSize;
        private int _AutoFlushSize;
        private int _BufferUsed;

        public delegate void FlushEventHandler(object sender, EventArgs e);
        public event FlushEventHandler Flushed;
        #endregion

        #region Properties
        /// <summary>
        /// get - Number of bytes in the buffer.
        /// </summary>
        public int BufferUsed
        {
            get { return _BufferSize; }
        }

        /// <summary>
        /// get/set - Controls when the stream should be flushed.
        /// </summary>
        public int AutoFlushSize
        {
            get { return _AutoFlushSize; }
            set { _AutoFlushSize = value; }
        }
        #endregion

        #region Constructors
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using UTF-8 encoding and the default buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     stream is not writable.
        //
        //   System.ArgumentNullException:
        //     stream is null.
        public AutoFlushStreamWriter(Stream stream)
            : base(stream)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     file by using the default encoding and buffer size.
        //
        // Parameters:
        //   path:
        //     The complete file path to write to. path can be a file name.
        //
        // Exceptions:
        //   System.UnauthorizedAccessException:
        //     Access is denied.
        //
        //   System.ArgumentException:
        //     path is an empty string (""). -or-path contains the name of a system device
        //     (com1, com2, and so on).
        //
        //   System.ArgumentNullException:
        //     path is null.
        //
        //   System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must not exceed 248
        //     characters, and file names must not exceed 260 characters.
        //
        //   System.IO.IOException:
        //     path includes an incorrect or invalid syntax for file name, directory name,
        //     or volume label syntax.
        //
        //   System.Security.SecurityException:
        //     The caller does not have the required permission.
        public AutoFlushStreamWriter(string path)
            : base(path)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using the specified encoding and the default buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     stream or encoding is null.
        //
        //   System.ArgumentException:
        //     stream is not writable.
        public AutoFlushStreamWriter(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     file by using the default encoding and buffer size. If the file exists, it
        //     can be either overwritten or appended to. If the file does not exist, this
        //     constructor creates a new file.
        //
        // Parameters:
        //   path:
        //     The complete file path to write to.
        //
        //   append:
        //     true to append data to the file; false to overwrite the file. If the specified
        //     file does not exist, this parameter has no effect, and the constructor creates
        //     a new file.
        //
        // Exceptions:
        //   System.UnauthorizedAccessException:
        //     Access is denied.
        //
        //   System.ArgumentException:
        //     path is empty. -or-path contains the name of a system device (com1, com2,
        //     and so on).
        //
        //   System.ArgumentNullException:
        //     path is null.
        //
        //   System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   System.IO.IOException:
        //     path includes an incorrect or invalid syntax for file name, directory name,
        //     or volume label syntax.
        //
        //   System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must not exceed 248
        //     characters, and file names must not exceed 260 characters.
        //
        //   System.Security.SecurityException:
        //     The caller does not have the required permission.
        public AutoFlushStreamWriter(string path, bool append)
            : base(path, append)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using the specified encoding and buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   bufferSize:
        //     The buffer size, in bytes.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     stream or encoding is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     bufferSize is negative.
        //
        //   System.ArgumentException:
        //     stream is not writable.
        public AutoFlushStreamWriter(Stream stream, Encoding encoding, int bufferSize)
            : base(stream, encoding, bufferSize)
        {
            _BufferSize = bufferSize;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     file by using the specified encoding and default buffer size. If the file
        //     exists, it can be either overwritten or appended to. If the file does not
        //     exist, this constructor creates a new file.
        //
        // Parameters:
        //   path:
        //     The complete file path to write to.
        //
        //   append:
        //     true to append data to the file; false to overwrite the file. If the specified
        //     file does not exist, this parameter has no effect, and the constructor creates
        //     a new file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   System.UnauthorizedAccessException:
        //     Access is denied.
        //
        //   System.ArgumentException:
        //     path is empty. -or-path contains the name of a system device (com1, com2,
        //     and so on).
        //
        //   System.ArgumentNullException:
        //     path is null.
        //
        //   System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   System.IO.IOException:
        //     path includes an incorrect or invalid syntax for file name, directory name,
        //     or volume label syntax.
        //
        //   System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must not exceed 248
        //     characters, and file names must not exceed 260 characters.
        //
        //   System.Security.SecurityException:
        //     The caller does not have the required permission.
        public AutoFlushStreamWriter(string path, bool append, Encoding encoding)
            : base(path, append, encoding)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using the specified encoding and buffer size, and optionally leaves
        //     the stream open.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   bufferSize:
        //     The buffer size, in bytes.
        //
        //   leaveOpen:
        //     true to leave the stream open after the System.IO.StreamWriter object is
        //     disposed; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     stream or encoding is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     bufferSize is negative.
        //
        //   System.ArgumentException:
        //     stream is not writable.
        public AutoFlushStreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
            : base(stream, encoding, bufferSize, leaveOpen)
        {
            _BufferSize = bufferSize;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     file on the specified path, using the specified encoding and buffer size.
        //     If the file exists, it can be either overwritten or appended to. If the file
        //     does not exist, this constructor creates a new file.
        //
        // Parameters:
        //   path:
        //     The complete file path to write to.
        //
        //   append:
        //     true to append data to the file; false to overwrite the file. If the specified
        //     file does not exist, this parameter has no effect, and the constructor creates
        //     a new file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   bufferSize:
        //     The buffer size, in bytes.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     path is an empty string (""). -or-path contains the name of a system device
        //     (com1, com2, and so on).
        //
        //   System.ArgumentNullException:
        //     path or encoding is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     bufferSize is negative.
        //
        //   System.IO.IOException:
        //     path includes an incorrect or invalid syntax for file name, directory name,
        //     or volume label syntax.
        //
        //   System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   System.UnauthorizedAccessException:
        //     Access is denied.
        //
        //   System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must not exceed 248
        //     characters, and file names must not exceed 260 characters.
        [SecuritySafeCritical]
        public AutoFlushStreamWriter(string path, bool append, Encoding encoding, int bufferSize)
            : base(path, append, encoding, bufferSize)
        {
            _BufferSize = bufferSize;
        }
        #endregion

        #region Methods 
        /// <summary>
        ///     Clears all buffers for the current writer and causes any buffered data to
        ///     be written to the underlying stream.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">The current writer is closed.</exception>
        /// <exception cref="System.IO.IOException">An I/O error has occurred.</exception>
        /// <exception cref="System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
        public override void Flush()
        {
            base.Flush();

            if (Flushed != null)
                Flushed(this, new EventArgs());

            _BufferUsed = 0;
        }

        /// <summary>
        /// Clears all buffers for this stream asynchronously and causes any buffered
        /// data to be written to the underlying device.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">The stream has been disposed.</exception>
        /// <returns>A task that represents the asynchronous flush operation.</returns>
        [ComVisible(false)]
        public override Task FlushAsync()
        {
            var task = base.FlushAsync();

            if (Flushed != null)
                Flushed(this, new EventArgs());

            _BufferUsed = 0;
            return task;
        }

        //
        // Summary:
        //     Writes a character to the stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the stream.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        public override void Write(char value)
        {
            base.Write(value);
            CheckToFlush(this.Encoding.GetByteCount(new [] { value }));
        }

        //
        // Summary:
        //     Writes a character array to the stream.
        //
        // Parameters:
        //   buffer:
        //     A character array containing the data to write. If buffer is null, nothing
        //     is written.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        public override void Write(char[] buffer)
        {
            base.Write(buffer);
            CheckToFlush(this.Encoding.GetByteCount(buffer));
        }

        //
        // Summary:
        //     Writes a string to the stream.
        //
        // Parameters:
        //   value:
        //     The string to write to the stream. If value is null, nothing is written.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        //
        //   System.IO.IOException:
        //     An I/O error occurs.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        public override void Write(string value)
        {
            base.Write(value);
            CheckToFlush(this.Encoding.GetByteCount(value));
        }

        //
        // Summary:
        //     Writes a subarray of characters to the stream.
        //
        // Parameters:
        //   buffer:
        //     A character array that contains the data to write.
        //
        //   index:
        //     The character position in the buffer at which to start reading data.
        //
        //   count:
        //     The maximum number of characters to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        public override void Write(char[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
            CheckToFlush(this.Encoding.GetByteCount(buffer.Skip(index).Take(count).ToArray()));
        }

        //
        // Summary:
        //     Writes a character to the stream asynchronously.
        //
        // Parameters:
        //   value:
        //     The character to write to the stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteAsync(char value)
        {
            var task = base.WriteAsync(value);
            CheckToFlush(this.Encoding.GetByteCount(new [] { value }));
            return task;
        }

        //
        // Summary:
        //     Writes a string to the stream asynchronously.
        //
        // Parameters:
        //   value:
        //     The string to write to the stream. If value is null, nothing is written.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteAsync(string value)
        {
            var task = base.WriteAsync(value);
            CheckToFlush(this.Encoding.GetByteCount(value));
            return task;
        }

        //
        // Summary:
        //     Writes a subarray of characters to the stream asynchronously.
        //
        // Parameters:
        //   buffer:
        //     A character array that contains the data to write.
        //
        //   index:
        //     The character position in the buffer at which to begin reading data.
        //
        //   count:
        //     The maximum number of characters to write.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The index plus count is greater than the buffer length.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            var task = base.WriteAsync(buffer, index, count);
            CheckToFlush(this.Encoding.GetByteCount(buffer.Skip(index).Take(count).ToArray()));
            return task;
        }

        //
        // Summary:
        //     Writes a line terminator asynchronously to the stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteLineAsync()
        {
            var task = base.WriteLineAsync();
            CheckToFlush(this.Encoding.GetByteCount(this.CoreNewLine));
            return task;
        }

        //
        // Summary:
        //     Writes a character followed by a line terminator asynchronously to the stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteLineAsync(char value)
        {
            var task = base.WriteLineAsync(value);
            CheckToFlush(this.Encoding.GetByteCount(new [] { value }));
            return task;
        }

        //
        // Summary:
        //     Writes a string followed by a line terminator asynchronously to the stream.
        //
        // Parameters:
        //   value:
        //     The string to write. If the value is null, only a line terminator is written.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteLineAsync(string value)
        {
            var task = base.WriteLineAsync(value);
            CheckToFlush(this.Encoding.GetByteCount(value));
            return task;
        }

        //
        // Summary:
        //     Writes a subarray of characters followed by a line terminator asynchronously
        //     to the stream.
        //
        // Parameters:
        //   buffer:
        //     The character array to write data from.
        //
        //   index:
        //     The character position in the buffer at which to start reading data.
        //
        //   count:
        //     The maximum number of characters to write.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The index plus count is greater than the buffer length.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   System.ObjectDisposedException:
        //     The stream writer is disposed.
        //
        //   System.InvalidOperationException:
        //     The stream writer is currently in use by a previous write operation.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns></returns>
        [ComVisible(false)]
        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            var task = base.WriteLineAsync(buffer, index, count);
            CheckToFlush(this.Encoding.GetByteCount(buffer.Skip(index).Take(count).ToArray()));
            return task;
        }

        /// <summary>
        /// Check if BufferUsed has reached the AutoFlushSize.
        /// If it has call the Flush method
        /// </summary>
        /// <param name="bytes">Number of bytes written to the buffer.</param>
        private void CheckToFlush(int bytes)
        {
            if (_AutoFlushSize > 0 && (_BufferUsed + bytes) >= _AutoFlushSize)
                this.Flush();
            else
                _BufferUsed += bytes;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
