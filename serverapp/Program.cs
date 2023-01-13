using AspWebApp.Data;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:3000");
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET React Tutorial", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "ASP.NET React Tutorial";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API serving a very simple Post model.");
    swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");



app.MapPut("/update-employe", async (User employeToUpdate) =>
{
    bool updateSuccessful = await UsersRepository.UpdateUserAsync(employeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Employes Endpoints");

app.MapDelete("/delete-user-by-id/{Id}", async (int Id) =>
{
    bool deleteSuccessful = await UsersRepository.DeleteUserAsync(Id);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Users Endpoints");

app.MapGet("/get-all-users", async () => await UsersRepository.GetUsersAsync())
    .WithTags("Users Endpoints");

app.MapGet("/get-user-by-id/{Id}", async (int Id) =>
{
    User userToReturn = await UsersRepository.GetUserByIdAsync(Id);

    if (userToReturn != null)
    {
        return Results.Ok(userToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Users Endpoints");


app.MapPost("/create-user", async (User userToCreate) =>
{
    bool createSuccessful = await UsersRepository.CreateUserAsync(userToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Users Endpoints");


app.Run();