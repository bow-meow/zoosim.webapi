using zoosim.core.Enums;
using zoosim.core.Models;
using zoosim.webapi.Services;
namespace zoosim.webapi.Controllers;

public static class ZooController
{
    public static void MapZooEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/zoo", GetAllAnimals);
        builder.MapPost("/api/zoo/feed", FeedAnimals);
        builder.MapPost("/api/zoo/revive", ReviveAnimals);
        builder.MapPost("/api/zoo/hour", AddHour);
    }

    public static IResult GetAllAnimals(HttpContext context, IZooService zooService)
    {
        if (!TryGetTabId(context, out var tabId, out var errorResult))
            return errorResult!;

        return GetAllAnimalsInternal(tabId, zooService);
    }

    private static IResult GetAllAnimalsInternal(Guid tabId, IZooService zooService) =>
        Results.Ok(GroupAnimalsByType(zooService.GetAllAnimals(tabId)));

    public static IResult FeedAnimals(HttpContext context, IZooService zooService)
    {
        if (!TryGetTabId(context, out var tabId, out var errorResult))
            return errorResult!;

        zooService.FeedAnimals(tabId);
        return GetAllAnimalsInternal(tabId, zooService);
    }
    public static IResult ReviveAnimals(HttpContext context, IZooService zooService)
    {
        if (!TryGetTabId(context, out var tabId, out var errorResult))
            return errorResult!;

        zooService.ReviveAnimals(tabId);
        return GetAllAnimalsInternal(tabId, zooService);
    }
    public static IResult AddHour(HttpContext context, IZooService zooService)
    {
        if (!TryGetTabId(context, out var tabId, out var errorResult))
            return errorResult!;

        zooService.CycleTimeBy1Hour(tabId);
        return GetAllAnimalsInternal(tabId, zooService);
    }

    private static bool TryGetTabId(HttpContext context, out Guid tabId, out IResult errorResult)
    {
        tabId = Guid.Empty;
        var tabIdStr = context.Request.Headers["Tab-ID"].ToString();
        if (string.IsNullOrEmpty(tabIdStr) || !Guid.TryParse(tabIdStr, out tabId))
        {
            errorResult = Results.BadRequest("id is invalid");
            return false;
        }

        errorResult = null;
        return true;
    }

    private static object GroupAnimalsByType(IEnumerable<IAnimal> allAnimals)
    {
        return new
        {
            monkeys = allAnimals.Where(a => a.AnimalType == AnimalType.Monkey),
            giraffes = allAnimals.Where(a => a.AnimalType == AnimalType.Giraffe),
            elephants = allAnimals.Where(a => a.AnimalType == AnimalType.Elephant)
        };
    }
}
