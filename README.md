# Description of the application

Solution to the requested task to count words in a (large) file.

## Environment

* .NET 6
* C# 10

### Packages

#### Productive code

* [Microsoft.Community.Toolkit](https://github.com/CommunityToolkit/WindowsCommunityToolkit)

#### Tests

* [xUnit](https://xunit.net/)
* [Moq](https://github.com/moq/moq4)
* [Fluent Assertion](https://fluentassertions.com/)
* [Coverlet](https://github.com/coverlet-coverage/coverlet)

## Questions for Product Owner (PO)

When the operation to count all words in a text is canceled how should the system behave?  

* a) The system should return all words with their count up until the point the operation was canceled  
* b) The system should return nothing

PO decision
To keep the system simple we will return nothing and the client which uses the system has to decide what to do.
