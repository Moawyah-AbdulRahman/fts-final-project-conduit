using conduit.api.Dtos.Article;
using conduit.api.Dtos.Comment;
using conduit.api.Dtos.User;
using conduit.api.Filters;
using conduit.api.Validators;
using conduit.api.Validators.Article_Validators;
using conduit.api.Validators.Comment_Validators;
using conduit.domain.services.interfaces;
using conduit.infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//filters
builder.Services.AddControllers(opt => opt.Filters.Add<ValidateRequestFilter>());

builder.Services.AddScoped(
    serviceProvider => new CheckLoginMiddleware(
        serviceProvider.GetRequiredService<ILoginService>(),
        new List<string> { "/api/login", "/api/logout" })
    );

//persistance related services
builder.Services.AddConduit(
    builder.Configuration.GetSection("DataBase:ConnectionString").Value,
    builder.Configuration.GetSection("RedisDb:ConnectionString").Value,
    builder.Configuration.GetSection("JwtTokenValues:SecertKey").Value,
    Convert.ToInt32(builder.Configuration.GetSection("JwtTokenValues:MinutesToLive").Value));

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey( 
                Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvxyz")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<CheckLoginMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
