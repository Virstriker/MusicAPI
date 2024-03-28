using MusicAPI.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IArtistRepo,ArtistRepo>();
//builder.Services.AddScoped<IArtistRepo,ArtistRepo>();
builder.Services.AddSingleton<ISongRepo,SongRepo>();
builder.Services.AddSingleton<IPlaylistRepo,PlaylistRepo>();
builder.Services.AddSingleton<IUserRepo,UserRepo>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddSingleton<IDelete,SongRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
