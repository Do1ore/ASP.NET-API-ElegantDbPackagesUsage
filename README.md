
# Asp.Net Core Web Minimal Api (.Net 8)




 ### CRUD endpoints to work with database most popular ways:

 - [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
 - [Dapper](https://www.learndapper.com/)
 - [Ado.Net](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview)

 ##Kibana to analyse logs from application.
 ##Redis caching to raise app perfomance
 ##Seilog to log data to elastic search


## API Reference

#### Get all items

```http
  GET /api/v1/photos
```


#### Get all photos

```http
  GET /api/items/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of item to fetch |




## Api Features

- Get all 
- Get by Id
- Update 
- Delete
- Create
- Chech for existing
- Health check


## Tech Stack

**Api** 
- Docker
- EntityFrameworkCore.PostgreSQL
- Dapper
- Ado.Net 
- MediatR 
- FluentValidation
- LanguageExt.Core
- Swagger
- Elasticsearch
- Kibana
- Redis
- Redis commander
- Serilog

