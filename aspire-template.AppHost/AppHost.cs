var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgres-password", "yourstaticpassword", secret: true);

var postgres = builder.AddPostgres("postgres", password: postgresPassword, port: 5432)
    .WithDataVolume()
    .WithPgAdmin();
var db = postgres.AddDatabase("appdb");


var redis = builder.AddRedis("redis")
    .WithRedisInsight();

var apiService = builder.AddProject<Projects.aspire_template_ApiService>("apiservice")
    .WithReference(db)
    .WithReference(redis)
    .WaitFor(db)
    .WaitFor(redis)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.aspire_template_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();