using System.Net.WebSockets;
using System.Net;
using System.Text;
using Cast;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//SQL Server Connection
builder.Services.AddDbContext<CastDBContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:dbConnection"]));
builder.WebHost.UseUrls("http://localhost:6969");
var app = builder.Build();
app.UseWebSockets();

app.MapControllers();

await app.RunAsync();
