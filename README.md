Fosol.Common
============

The Fosol.Common library contains functions, methods, extensions and custom classes to help build software solutions.

Parameter Validation
====================

Most functions and methods should enforce valid parameter values.  
They also should throw appropriate exceptions when invalid parameter values are specified.  
The Validation.Parameter methods provide a common, clean syntax that are easily readable and understandable.
Each assertion method ensures the parameter value is valid.  If the value is not valid it will throw the appropriate exception.

<ul>
	<li>AssertIsNotNull</li>
	<li>AssertIsNotNullOrEmpty</li>
	<li>AssertIsValue</li>
	<li>AssertIsNotValue</li>
	<li>AssertMinRange</li>
	<li>AssertMaxRange</li>
	<li>AssertRange</li>
	<li>AssertStartsWith</li>
	<li>AssertEndsWith</li>
	<li>AssertHasAttribute</li>
</ul>

There are also a number of format validation methods.
These methods are used to confirm a value matches a specified format.

<ul>
	<li>IsEmail</li>
	<li>IsNumber</li>
	<li>IsUri</li>
	<li>IsPostalCode</li>
	<li>IsFSA</li>
	<li>IsLDU</li>
</ul>