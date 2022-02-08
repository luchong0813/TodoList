using Microsoft.AspNetCore.Mvc;

using TodoList.Application.Common.Interfaces;
using TodoList.Application.TodoItems.Specs;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IRepository<TodoItem> _Repository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,IRepository<TodoItem> repository)
    {
        _logger = logger;
        _Repository = repository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("这是我们自己引入的第三方日志框架...");

        var spec = new TodoItemSpec(true, Domain.Enums.PriorityLevel.High);
        var items = _Repository.GetAsync(spec).Result;

        foreach (var item in items)
        {
            _logger.LogInformation($"item:{item.Id} - {item.Title} - {item.Priority}");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
