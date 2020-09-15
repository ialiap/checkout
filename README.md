
# checkout material test microservice

this source code is only a test project for a software developer position

## Installation

Using Manual build requires a raven database connection for tests to pass and for services to function. database connection can be change in application settings.

```bash
dotnet restore
dotnet test
dotnet Challenge.Services.Payment.dll
```
Since the service is docker enabled another option is to use docker-compose.

```bash
docker-compose up --force-recreate
```
for rebuild images in case of changes
```bash
docker-compose build
```
## Usage

The whole source code is very simple and self-explanatory and you can find structural documentation and API client in the link below

```python
https://[localhost:8060]/swagger
```

## Contributing
Pull requests are welcome. feel free to add comments and improvement request and I will change them ASAP.

Please make sure to update the tests as appropriate.



## License
[MIT](https://choosealicense.com/licenses/mit/)
