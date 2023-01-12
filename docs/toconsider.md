# Considerations
Things that I would do, but unfortunately had no more time to do it.

### Unit test to validate dependency container
To avoid invalid configuration of the dependency container, and to avoid some runtime errors, we can create simple test for validating DI container.


Pseudocode:
```

var services = new ServiceCollection();
services.AddApi();
services.AddApplication();
services.AddInfrastructure(configuration);

var servicesToValidate = services.Where(y =>
    typeof(y.ServiceImplementation).Namespace.StartsWith("Tivix")) // Validating our services only
    
var requiredServices = servicesToValidate
    .Select(GetConstructorParametersByReflection); 
    
services.Should().Contain(requiredServices);

```



### Caching
Query caching done in MediatR pipelines.
- Attribute "Cachable" on IRequest<T>
- In MediatR pipeline, check if request has attribute "Cachable"
- Calculate key (for example id from request or just whole request)
- Get value from the cache
- Remove cache entries on updates


### E2E / Component testing
I would use [test  containers](https://github.com/testcontainers/testcontainers-dotnet) for E2E testing if necessary.

### Smart exception handling
For now, there is no exception handling other than catching permission related exception. But there should be some.

### Unfortunately, didn't accomplish all required tasks on time
- Lack of tests, because I wanted to implement API more than tests. Usually, I do Test-Driven Development
- 