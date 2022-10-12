using conduit.api.Dtos;
using conduit.api.Filters;
using conduit.api.Validators;
using conduit.db;
using conduit.db.repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//filters
builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>());

//persistance related services
builder.Services.AddScoped<ConduitDbContext>();
builder.Services.AddScoped<IUserRepository, UserDbRepository>();

//mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

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
