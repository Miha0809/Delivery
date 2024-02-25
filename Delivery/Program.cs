using System.Reflection;
using Delivery.Models;
using Delivery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Zhnyvo",
        Version = "v1",
        Description = "An API to zhnyvo for TeamChallenger",
        
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();
builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseLazyLoadingProxies()
           .UseNpgsql(builder.Configuration.GetConnectionString("ElephantSQL")); // ElephantSQL Localhost
});
builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>(options =>
    {
        
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DeliveryDbContext>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins(
            builder.Configuration.GetSection("FRONT_END_URLs:Host").Value!,
            builder.Configuration.GetSection("FRONT_END_URLs:Local").Value!)
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Zhnyvo V1");
    });
// }

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var roles = new[]
//     {
//         nameof(Roles.Admin),
//         nameof(Roles.Seller),
//         nameof(Roles.Customer),
//         nameof(Roles.Moderator)
//     };
//
//     foreach (var role in roles)
//     {
//         if (!await roleManager.RoleExistsAsync(role))
//         {
//             await roleManager.CreateAsync(new IdentityRole(role));
//         }
//     }
// }
//
// using (var scope = app.Services.CreateScope())
// {
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//     var email = "admin@admin.com";
//     var password = "Test1234,";
//
//     if (await userManager.FindByEmailAsync(email) is null)
//     {
//         var user = new User();
//         user.UserName = email;
//         user.Email = email;
//
//         await userManager.CreateAsync(user, password);
//         await userManager.AddToRoleAsync(user, nameof(Roles.Admin));
//     }
// }

app.Run();
