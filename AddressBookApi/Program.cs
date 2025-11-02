// Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// KÖK ADRESÝ /swagger'a yönlendir
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
