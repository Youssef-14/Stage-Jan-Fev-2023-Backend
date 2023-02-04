using AspWebApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using serverapp.Data;
using serverapp.Security;
using serverapp.Services;
using System.Text;

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

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET", Version = "v1" });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret......."))
                    };
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

//Department Controller
//Department Controller
DemandeService DemandeService = new DemandeService(new AppDBContext());

app.MapGet("/get-all-demandes", async () => await DemandeService.GetDemandesAsync())
    .WithTags("Demands Endpoints");
app.MapGet("/get-demandes-filtered-number/{type}/{status}", async (string type, string status) => await DemandeService.GetDemandesFilteredNumber(type, status))
    .WithTags("Demands Endpoints");
app.MapGet("/get-all-demandes-by-user/{Id}", async (int Id) => await DemandeService.GetDemandsByUserIdAsync(Id))
    .WithTags("Demands Endpoints");

app.MapGet("/get-all-accepted-demands", async () => await DemandeService.GetAcceptedDemandesAsync())
    .WithTags("Demands Endpoints");

app.MapGet("/get-all-rejected-demands", async () => await DemandeService.GetRefusedDemandesAsync())
    .WithTags("Demands Endpoints");
app.MapGet("/get-all-pending-demands", async () => await DemandeService.GetPendingDemandesAsync())
    .WithTags("Demands Endpoints");

app.MapGet("/get-filtered-demands/{type}/{status}/{begin}/{end}", async (string type, string status, int begin, int end) =>
     await DemandeService.GetFilteredDemandesAsync(type, status, begin, end))
    .WithTags("Demands Endpoints");

app.MapGet("/get-demande-by-id/{Id}", async (int Id) =>
{
    Demande demandeToReturn = await DemandeService.GetDemandeByIdAsync(Id);

    if (demandeToReturn != null)
    {
        return Results.Ok(demandeToReturn);
    }
    else
    {
        return Results.NotFound("File not found");
    }
}).WithTags("Demands Endpoints");

app.MapPost("/create-demande", async (Demande demandeToCreate) =>
{
    bool createSuccessful = await DemandeService.CreateDemandeAsync(demandeToCreate);

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
    bool updateSuccessful = await DemandeService.UpdateDemandeAsync(demandeToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-accepted/{id}", async (int id) =>
{
    bool updateSuccessful = await DemandeService.SetDemandeToAcceptedAsync(id);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-refused/{id}", async (int id) =>
{
    bool updateSuccessful = await DemandeService.SetDemandeToRefusedAsync(id);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.MapPut("/set-demande-to-pending/{id}", async (int id) =>
{
    bool updateSuccessful = await DemandeService.SetDemandeToPendingAsync(id);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

    app.MapPut("/set-demande-to-be-corrected/{id}", async (int id) =>
    {
        bool updateSuccessful = await DemandeService.SetDemandeToBeCorrectedAsync(id);

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
    bool deleteSuccessful = await DemandeService.DeleteDemandeAsync(Id);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Demands Endpoints");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Run();