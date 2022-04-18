using Signalr_poc;
using Signalr_poc.Repository;
using Signalr_poc.WebRTC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();


builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPeerConnectionManager, PeerConnectionManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(c =>
    c.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins("https://localhost:4200");
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.AllowCredentials();
        builder.SetIsOriginAllowed((host) => true);
    }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapHub<HubConnection>("/hub");
    endpoints.MapControllers();
});

app.Run();
