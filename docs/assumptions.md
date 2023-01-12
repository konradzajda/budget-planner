## Development assumptions

- HTTPS is not handled by application itself, I assume it's done on infrastructure layer (for example, k8s)
- Entity Framework used as ORM, even if I think Dapper would be better choice here. I wanted to simplify migrations to reduce time to complete this task.
- I disabled implicit usings, because .NET 7 SDK is not fully supported yet in Jetbrains Rider
- `/build.cmd` is just for quick reloading local instance of the service
- Code Coverage is not high as I intended to only show testing competences in different scenarios. Most of the code can actually lack of proper testing.
- Implemented with simple clean architecture with mapping between infrastructure and application layer
- No volume mounted at Postgres due to hardware issues, but you can use if you like.
```
postgres:
    volumes:
      - /private/var/lib/postgresql:/var/lib/postgresql
```