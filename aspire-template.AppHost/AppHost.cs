var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
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