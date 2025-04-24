using api.data;
using api.interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(
    
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(opttions => {
    opttions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/openapi/v1.json", "api | v1"));
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

