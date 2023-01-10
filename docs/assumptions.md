## Development assumptions

- HTTPS is not handled by application itself, I assume it's done on infrastructure layer (for example, k8s)
- Entity Framework used as ORM, even if I think Dapper would be better choice here. I wanted to simplify migrations to reduce time to complete this task.
- I disabled implicit usings, because .NET 7 SDK is not fully supported yet in Jetbrains Rider
- `/build.cmd` is just for quick reloading local instance of the service
- No CORS has been configured