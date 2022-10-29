using conduit.api.Dtos.Article;
using conduit.api.Dtos.Comment;
using conduit.api.Dtos.User;
using conduit.api.Filters;
using conduit.api.Validators;
using conduit.api.Validators.Article_Validators;
using conduit.api.Validators.Comment_Validators;
using conduit.db;
using conduit.db.repositories;
using conduit.domain.repositories;
using conduit.infrastructure;
using conduit.infrastructure.profiles;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//filters
builder.Services.AddControllers(opt => opt.Filters.Add<ValidateRequestFilter>());

//persistance related services
builder.Services.AddConduit();

//mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSingleton<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddSingleton<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
builder.Services.AddSingleton<IValidator<CreateArticleDto>, CreateArticleDtoValidator>();
builder.Services.AddSingleton<IValidator<CreateCommentDto>, CreateCommentDtoValidator>();

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
