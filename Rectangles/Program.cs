// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RectanglesBusiness.Controllers;
using RectanglesBusiness.Interfaces;
using RectanglesBusiness.Repositories;
using RectanglesBusiness.Services;
using RectanglesDataAccess.Models;
using RectanglesDataAccess.Repositories.Interfaces;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
        .AddScoped<IRectanglesRepository, RectanglesRepository>()
        .AddScoped<IRectanglesService, RectanglesService>()
        .AddSingleton<RectanglesController>();
    })
    ;

var app = host.Build();
var rectanglesController = app.Services.GetService<RectanglesController>();
var createGridResult = rectanglesController!.CreateGrid(new Size(15, 15));
if (!string.IsNullOrEmpty(createGridResult))
    Console.WriteLine(createGridResult);

var createRectangleResult = rectanglesController!.CreateRectangle(new Size(5, 5), new Position(5, 5));
if (!string.IsNullOrEmpty(createRectangleResult))
    Console.WriteLine(createRectangleResult);

var createSecondRectangleResult = rectanglesController!.CreateRectangle(new Size(5, 5), new Position(2, 2));
if (!string.IsNullOrEmpty(createSecondRectangleResult))
    Console.WriteLine(createSecondRectangleResult);
