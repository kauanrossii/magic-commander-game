using MagicCommander.Application.Auth.Sigin;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards;
using MagicCommander.Domain.Decks;
using MagicCommander.Domain.Users;
using MagicCommander.Infra.Data.Database;
using MagicCommander.Infra.Data.Database._Shared;
using MagicCommander.Infra.Data.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext, MagicContext>(options => {
	var connectionString = builder.Configuration.GetConnectionString("PostgresqlConnection"); 
	options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDecksRepository, DecksRepository>();
builder.Services.AddScoped<ICardsRepository, CardsRepository>();

builder.Services.AddMediatR(cfg => {
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

var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSecret"]);

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
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidateAudience = false
	};
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
