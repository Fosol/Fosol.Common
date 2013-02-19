Fosol.Common
============

The Fosol.Common library contains functions, methods, extensions and custom classes to help build software solutions.

Parameter Validation
====================

Most functions and methods should enforce valid parameter values.  
They also should throw appropriate exceptions when invalid parameter values are specified.  
The Validation.Parameter methods provide a common, clean syntax that are easily readable and understandable.
Each assertion method ensures the parameter value is valid.  If the value is not valid it will throw the appropriate exception.

- Assert.IsNotNull
- Assert.IsNotNullOrEmpty
- Assert.IsValue
- Assert.IsNotValue
- Assert.IsTrue
- Assert.IsFalse
- Assert.MinRange
- Assert.MaxRange
- Assert.Range
- Assert.StartsWith
- Assert.EndsWith
- Assert.HasAttribute

There are also a number of format validation methods.
These methods are used to confirm a value matches a specified format.

- AssertFormat.IsEmail
- AssertFormat.IsNumber
- AssertFormat.IsUri
- AssertFormat.IsPostalCode
- AssertFormat.IsFSA
- AssertFormat.IsLDU