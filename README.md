#### About React Store

This is a full-stack e-commerce application built using: 
| Frontend   | Backend                     | Test automation  | Data Analytics |   
|------------|-----------------------------|------------------|----------------|
| React      | C#                          | NUnit            | Tensorflow.js  |   
| Redux      | DotNet 8                    | Specflow         | Regression.js  |   
| Axios      | JWT bearer tokens           | Selenium         | Recharts       |   
| MUI        | Automapper                  | FluentAssertions |                |  
| TypeScript | OpenAPI/Swashbuckle swagger | Jest             |                |   
| Dropzone   | SignalR                     | Postman          |                |   
| Toastify   | Confluent Kafka             |                  |                |   
| Moments    | Docker                      |                  |                |   
| Yup        | PostgreSQL                  |                  |                |   
|            | EntityFramework Core        |                  |                |   
  
## Features
https://github.com/RishiRajGujadhur/ReactStore/assets/17295261/f0be49df-f1fa-4dfd-856c-87438ac61431

# Getting Started

To get started, clone the repository and install the dependencies:

```git clone https://github.com/RishiRajGujadhur/ReactStore.git```
```git clone https://github.com/RishiRajGujadhur/client.git```

```cd ReactStore\Store\Client```

```npm install```


## Running the Application

To run the application, start the server and the client:
+ Run your PostgresQL server or update the configurations in API, program.cs and app.config to use a different db server
+ Run your Kafka server, docker script for kafka setup included: https://github.com/RishiRajGujadhur/ReactStore/blob/main/docker-compose.yml

```cd Store.API: dotnet run```

```cd Store.Ordering.Service: dotnet run```

```cd Client: npm start```

### API Endpoints
https://github.com/RishiRajGujadhur/ReactStore/blob/main/Store.API.postman_collection.json

## Contributing
Contributions are welcome! Please create a pull request with your changes.

## License
This project is licensed under the MIT License.
