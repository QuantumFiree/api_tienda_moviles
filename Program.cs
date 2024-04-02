//======================================================

using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add service of database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Prueba de conexion DB pgsql
try
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Conexión exitosa a la base de datos.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error de conexión a la base de datos: {ex.Message}");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

