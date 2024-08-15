using MagicCommander.Api.CrossCutting;
using MagicCommander.Api.Helpers;
using MagicCommander.Application.Auth.Sigin;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards;
using MagicCommander.Domain.Decks;
using MagicCommander.Domain.DecksImports;
using MagicCommander.Domain.Users;
using MagicCommander.Infra.Data.Database;
using MagicCommander.Infra.Data.Database._Shared;
using MagicCommander.Infra.Data.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
	Environment.SetEnvironmentVariable("JwtSecret", builder.Configuration.GetValue<string>("JwtSecet"));
}

builder.Services.AddDbContext<DbContext, MagicContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("PostgresqlConnection");
	options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDecksRepository, DecksRepository>();
builder.Services.AddScoped<ICardsRepository, CardsRepository>();
builder.Services.AddScoped<IDeckImportsRepository, DeckImportsRepository>();

builder.Services.AddScoped<JwtTokenHelper>();
builder.Services.AddScoped<JwtMiddleware>();

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssemblyContaining<Program>();
	cfg.RegisterServicesFromAssemblyContaining<SigninRequest>();
});

builder.Services.AddMvc(config =>
{
	var policy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
	config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret"))!),
		ValidateIssuer = true,
		ValidateAudience = false
	};
});

builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = new AuthorizationPolicyBuilder()
			.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
			.RequireAuthenticatedUser()
			.Build();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Magic Commander Deck", Version = "v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please ener a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
