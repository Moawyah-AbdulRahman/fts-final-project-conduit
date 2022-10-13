using conduit.api.Dtos.Article;
using conduit.api.Dtos.User;
using conduit.api.Filters;
using conduit.api.Validators;
using conduit.api.Validators.Article_Validators;
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
builder.Services.AddScoped<IArticleRepository, ArticleDbRepository>();

//mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateArticleDto>, CreateArticleDtoValidator>();

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
