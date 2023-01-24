using AspWebApp.Data;
using Microsoft.OpenApi.Models;
using serverapp.Data;
using serverapp.Services;

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


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET", Version = "v1" });
});

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "ASP.NET";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API serving a very simple Post model.");
    swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");


// User Controller 
// User Controller 

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

app.MapGet("/get-user-by-email-and-password/{email}/{password}", async (string email, string password) =>
{
    User userToReturn = await UsersRepository.GetUserByEmailAndPasswordAsync(email, password);

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

app.MapPost("/create-admin", async (User userToCreate) =>
{
    bool createSuccessful = await UsersRepository.CreateAdminAsync(userToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Users Endpoints");

app.MapPut("/update-user", async (User employeToUpdate) =>
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
}).WithTags("Users Endpoints");

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

//Department Controller
//Department Controller

app.MapGet("/get-all-demandes", async () => await DemandeRepository.GetDemandesAsync())
    .WithTags("Demands Endpoints");
app.MapGet("/get-all-demandes-by-user/{Id}", async (int Id) => await DemandeRepository.GetDemandsByUserIdAsync(Id))
    .WithTags("Demands Endpoints");

app.MapGet("/get-all-accepted-demands", async () => await DemandeRepository.GetAcceptedDemandesAsync())
    .WithTags("Demands Endpoints");

app.MapGet("/get-all-rejected-demands", async () => await DemandeRepository.GetRefusedDemandesAsync())
    .WithTags("Demands Endpoints");
app.MapGet("/get-all-pending-demands", async () => await DemandeRepository.GetPendingDemandesAsync())
    .WithTags("Demands Endpoints");

app.MapGet("/get-filtered-demands/{type}/{status}/{begin}/{end}", async (string type, string status, int begin, int end) =>
     await DemandeRepository.GetFilteredDemandesAsync(type, status, begin, end))
    .WithTags("Demands Endpoints");

app.MapGet("/get-demande-by-id/{Id}", async (int Id) =>
{
    Demande demandeToReturn = await DemandeRepository.GetDemandeByIdAsync(Id);

    if (demandeToReturn != null)
    {
        return Results.Ok(demandeToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPost("/create-demande", async (Demande demandeToCreate) =>
{
    bool createSuccessful = await DemandeRepository.CreateDemandeAsync(demandeToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/update-demande", async (Demande demandeToUpdate) =>
{
    bool updateSuccessful = await DemandeRepository.UpdateDemandeAsync(demandeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-accepted", async (Demande demandeToUpdate) =>
{
    bool updateSuccessful = await DemandeRepository.SetDemandeToAcceptedAsync(demandeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-refused", async (Demande demandeToUpdate) =>
{
    bool updateSuccessful = await DemandeRepository.SetDemandeToRefusedAsync(demandeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-pending", async (Demande demandeToUpdate) =>
{
    bool updateSuccessful = await DemandeRepository.SetDemandeToPendingAsync(demandeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

    app.MapPut("/set-demande-to-be-corrected", async (Demande demandeToUpdate) =>
    {
        bool updateSuccessful = await DemandeRepository.SetDemandeToBeCorrectedAsync(demandeToUpdate);

        if (updateSuccessful)
        {
            return Results.Ok("Update successful.");
        }
        else
        {
            return Results.BadRequest();
        }
    }).WithTags("Demands Endpoints");


app.MapDelete("/delete-demande-by-id/{Id}", async (int Id) =>
{
    bool deleteSuccessful = await DemandeRepository.DeleteDemandeAsync(Id);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");


app.Run();