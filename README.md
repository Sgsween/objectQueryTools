Object Query Tools
==============

objectQueryTools.js: JavaScript Library for searching an array.  
Extends the array object to include methods for searching the array.

Examples:
* AnyMatch
	- Returns a boolean
		true - A match found
		false - No match found
	- Stops executing the search once the first match is found
	- Parameters
		obj - the exact object being searched for, or the object that will be used by the callback to search
		conditions - function to execute search conditions.
			Parameters - element in the array, obj passed into the AnyMatch function

1. Search for any object in the array with a matching property
var found = employees.AnyMatch(employee, function(l,o) {
  return l.department === o.department
});



2. Search for a string in an  array of strings
var department = 'IT'; 
var departments = ['HR','IT','Accounting'];
var found = departments.AnyMatch(department, function(l,o) {return l === o});

objQueryToolsTests.cs: C# test class.  Uses the MSTest framework and the JSTest library for testing JavaScript

http://jsperf.com/in-array-speed/2: performance tests compared to jQuery grep, mootools filter, and native JavaScript
