using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IOpportunityService, OpportunityService>() ;
builder.Services.AddTransient<IUserService, UserService>() ;
builder.Services.AddTransient<IApplicantService, ApplicantService>() ;
builder.Services.AddTransient<IInterviewRoundService, InterviewRoundService>() ;
builder.Services.AddTransient<IInterviewSlotService, InterviewSlotService>() ;
builder.Services.AddTransient<IFeedbackService,FeedbackService>() ;
builder.Services.AddTransient<IInterviewAssignmentService,InterviewAssignmentService>() ;
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));  //dependcy injection of db context alos the builer conf retrives the getconnection string from the appsetings.json
      //This is a lambda expression that configures the DbContext to use SQL Server as its database provider. It sets up the DbContext with the necessary configurations to connect to a SQL Server database
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") //front end localhost
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>

{

    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme

    {

        In = ParameterLocation.Header,

        Description = "Please enter token",

        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        BearerFormat = "JWT",

        Scheme = "bearer"


    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement

    {

        {

            new OpenApiSecurityScheme

            {

                Reference = new OpenApiReference

                {

                    Type=ReferenceType.SecurityScheme,

                    Id="Bearer"

                }

            },

            new string[]{}

        }

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
