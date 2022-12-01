using StudyBuddy.SignalR;
using StudyBuddy.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalRServices(builder.Configuration);
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7196",";http://localhost:5196","https://localhost:7042").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();;
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(enpoints =>
{
    enpoints.MapHub<ClassroomHub>("/ClassroomHub");
    enpoints.MapControllers();
});

app.Run();