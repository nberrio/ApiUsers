using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaInterminedio;
using PruebaTecnicaInterminedio.Data;
using PruebaTecnicaInterminedio.Repository;
using PruebaTecnicaInterminedio.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

//AddConection
builder.Services.AddDbContext<PruebaJuniorContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("Conexionsql"));
});

//Add DataSet
builder.Services.AddScoped<IUsersRepository, UserRepository>();
//add Mapper
builder.Services.AddAutoMapper(typeof(UsersMapper));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



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
