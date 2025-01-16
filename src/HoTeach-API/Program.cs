using System.Reflection;
using HoTeach.API.Infrastructure.Data.Configuration;
using HoTeach.API.Infrastructure.Data.Extensions;
using HoTeach.API.Infrastructure.Logging;
using HoTeach.API.Infrastructure.Validation;
using HoTeach.API.Infrastructure.Validation.Middlewares;
using HoTeach.API.Infrastructure.Web;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("Mongo").Get<MongoSettings>();

builder.Services
    .AddWeb(builder.Configuration)
    .AddSerilogLogging(builder.Configuration)
    .AddFluentValidation()
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddMongoDatabase(p =>
    {
        p.WithConnectionString(mongoSettings.Url);
        p.WithDatabaseName(mongoSettings.Database);
        p.WithSoftDeletes(o =>
        {
            o.Enabled(mongoSettings.SoftDeleteEnabled);
            o.HardDeleteAfter(TimeSpan.FromDays(mongoSettings.SoftDeleteRetentionInDays));
        });
        p.RepresentEnumValuesAs(BsonType.String);
        p.WithIgnoreIfDefaultConvention(false);
        p.WithIgnoreIfNullConvention(true);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseValidationExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(opts =>
{
    opts.AllowAnyOrigin();
    opts.AllowAnyHeader();
    opts.AllowAnyMethod();
    opts.WithExposedHeaders("X-Pagination");
}); 
app.UseAuthorization();
app.MapControllers();

app.Run();