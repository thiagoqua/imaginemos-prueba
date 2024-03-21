# Run the database with Docker
```bash
docker run -p 5432:5432 --name database -e POSTGRES_DB=imaginemos -e POSTGRES_USER=test -e POSTGRES_PASSWORD=admin postgres:16.2-alpine3.19
```