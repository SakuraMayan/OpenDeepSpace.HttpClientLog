using OpenDeepSpace.HttpClientLog;
using OpenDeepSpace.HttpClientLog.Demo;
using OpenDeepSpace.HttpClientLog.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加HttpClient请求远端url
builder.Services.AddHttpClient("BaiduClient", client =>
{
    //client.DefaultRequestHeaders.Add("client-name", "namedclient");
    client.BaseAddress = new Uri("http://baidu.com");
}).SetHandlerLifetime(TimeSpan.FromMinutes(20)) //SetHandlerLifetime设置生命周期时长，刷新DNS时间
.AddHttpMessageHandler<HttpClientLogHandler>(opt =>
{



});

//统一为RestClient传递一个client
builder.Services.AddHttpClient("RestClient", client =>
{

}).AddHttpMessageHandler<HttpClientLogHandler>(opt =>
{



});

builder.Services.AddTransient(typeof(IHttpClientLogStore), typeof(SimpleHttpClientLogStore));

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
