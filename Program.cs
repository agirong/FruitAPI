using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FruitDb>(opt => opt.UseInMemoryDatabase("FruitList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fruit API",
        Description = "API for managing a list of fruit and their stock status.",        
    });
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme{
        Name="Authorization",
        Type=SecuritySchemeType.Http,
        Scheme="basic",
        In=ParameterLocation.Header,
        Description = "Ingrese usuario y contrasena"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="basic"       
                }
            },
            new string []{}
        }
    });
});
var app = builder.Build();

app.Use(async (context,next)=>
{
    var path = context.Request.Path;
    if (path.StartsWithSegments("/swagger") || path.StartsWithSegments("/swagger/v1/swagger.json"))
    {
        await next.Invoke();
        return;
    }

    //Comprobar que la solicitud tiene el encabezado de autorizacion
    if(!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Authorization header missing");
        return;
    }

    var authHeader= AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
    var username = credentials[0];
    var password = credentials[1];

    //Credenciales predeterminadas
    var validUsername = "username";
    var validPassword = "Aaron&Kelly";
    
    if(username== validUsername && password==validPassword){
        await next.Invoke();
    }else{
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.WriteAsync("Invalid Credentials");
    }
    
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<FruitDb>();
    dbContext.Database.EnsureCreated();
}

 app.MapGet("/fruitlist", async (FruitDb db, int page = 1, int pageSize = 10) =>
{
    // Calcular el número total de elementos
    var totalItems = await db.Fruits.CountAsync();

    // Calcular el número total de páginas
    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

    // Obtener los elementos paginados
    var fruits = await db.Fruits
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    // Retornar los elementos junto con la información de paginación
    var result = new
    {
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems,
        TotalPages = totalPages,
        Items = fruits
    };

    return Results.Ok(result);
})
.WithTags("Get all fruit");


app.MapGet("/fruitlist/instock", async (FruitDb db) =>
    await db.Fruits.Where(t => t.Instock).ToListAsync())
    .WithTags("Get all fruit that is in stock");

app.MapGet("/fruitlist/{id}", async (int id, FruitDb db) =>
    await db.Fruits.FindAsync(id)
        is Fruit fruit
            ? Results.Ok(fruit)
            : Results.NotFound())
    .WithTags("Get fruit by Id");

app.MapPost("/fruitlist", async (Fruit fruit, FruitDb db) =>
{
    db.Fruits.Add(fruit);
    await db.SaveChangesAsync();

    return Results.Created($"/fruitlist/{fruit.Id}", fruit);
})
    .WithTags("Add fruit to list");

app.MapPut("/fruitlist/{id}", async (int id, Fruit inputFruit, FruitDb db) =>
{
    var fruit = await db.Fruits.FindAsync(id);

    if (fruit is null) return Results.NotFound();

    fruit.Name = inputFruit.Name;
    fruit.Instock = inputFruit.Instock;
    fruit.Price = inputFruit.Price;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
    .WithTags("Update fruit by Id");

app.MapDelete("/fruitlist/{id}", async (int id, FruitDb db) =>
{
    if (await db.Fruits.FindAsync(id) is Fruit fruit)
    {
        db.Fruits.Remove(fruit);
        await db.SaveChangesAsync();
        return Results.Ok(fruit);
    }

    return Results.NotFound();
})
    .WithTags("Delete fruit by Id");


app.UseSwagger();
app.UseSwaggerUI();


app.Run();