using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Model.Responses;
using Service.Interfaces;

namespace API.Controllers;

public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Function(nameof(GetUsers))]
    [OpenApiOperation(nameof(GetUsers), tags: "Users", Description = "Get all users")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(UserResponse[]))]
    public async Task<HttpResponseData> GetUsers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "users")] HttpRequestData req)
    {
        ICollection<UserResponse> users = await _userService.GetUsers();
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(users);

        return resp;
    }

    [Function(nameof(GetUser))]
    [OpenApiOperation(nameof(GetUser), tags: "Users", Description = "Get a user")]
    [OpenApiParameter("userId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(UserResponse))]
    public async Task<HttpResponseData> GetUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "users/{userId}")] HttpRequestData req,
        Guid userId)
    {
        UserResponse user = await _userService.GetUser(userId);
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(user);

        return resp;
    }
}
