# Solidatus MongoDb Configuration Extensions

This package enables you to add MongoDB as a configuration source for your application, and provides an interface for setting and updating those configuration values in the database.

## Installation

Install the package from NuGet.org using your preferred method.

## Usage

### Adding configuration source

Once installed you can add MongoDB as a configuration source as you add other configuration sources.

```{C#}
MongoClientSettings mongoSettings = new();
string database = "";
string collection = "Config";

Host
  .CreateDefaultBuilder(args)
  .ConfigureAppConfiguration((hostingContext, config) =>
  {
    config.AddMongoDb(mongoSettings, database, collection);
  });
```

You likely want to use the existing configuration to load up the appropriate values for the mongo settings though. You can accomplish that by extending the above code's app configuration section like so, using your own process for converting your application's configuration into the appropriate `MongoClientSettings` class.

```{C#}
.ConfigureAppConfiguration((hostingContext, config) =>
{
  config.AddJsonFile("config.jsonc");
  config.AddEnvironmentVariables();
  config.AddCommandLine(args);

  var mongoConfig = config.Build().GetSection("Mongo").Get<MongoConfig>();
  var mongoClientSettings = MongoUtils.GetMongoClientSettings(mongoConfig);

  config.AddMongoDb(mongoSettings, mongoConfig.Database, "Configuration");
}
```

### Updating configuration values

The package provides functions for setting/updating and removing stored configuration values.

```{C#}
await MongoConfigurationManager.ClearValue("Config:Key");

await MongoConfigurationManager.SetValue("Config:Key", "Value");
```

If you are running in a multi-node deployment and thus want to trigger configuration reloads on all nodes when updated, you can add the `MongoConfigurationWatcher` background service to your application's startup `ConfigureServices` method.

```{C#}
services.AddHostedService<MongoConfigurationWatcher>();
```

Alternatively you can use an existing change stream or other update method and manually call the method to reload the configuration values.

```{C#}
MongoConfigurationManager.ReloadValues();
```

## Mongo version support

The package internally uses version `3.8.0` of the MongoDB C# driver. This supports MongoDB versions 4.2 or later, as shown in the [MongoDB compatibility docs](https://www.mongodb.com/docs/drivers/csharp/current/compatibility/).
