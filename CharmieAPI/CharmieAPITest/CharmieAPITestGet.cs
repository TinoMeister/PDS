using CharmieAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Environment = CharmieAPI.Models.Environment;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestGet
{
    HttpClient _httpClient;

    public CharmieAPITestGet()
    {
        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();

        _httpClient = webAppFactory.CreateDefaultClient();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1QgZm9yIENoYXJtaWVBUEkucHQiLCJqdGkiOiIwZGY5NjAzNi05MzkzLTRmYmYtYmQ0Ny04MDVkMTJmZjA1ZjQiLCJpYXQiOiI1LzI2LzIwMjMgMTI6NDQ6MTEgUE0iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjU4NDRlNDgzLTI3ZjMtNDhkYS1hMDAzLTk5ZTQ4NmYxMTY5OSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0aW5vMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InRpbm8yQGdtYWlsLmNvbSIsImV4cCI6MTY4NTExMjI1MSwiaXNzIjoiQ2hhcm1pZUFQSS5wdCIsImF1ZCI6IkNoYXJtaWVBUEkucHQifQ.J38uQlelL9qzzZyAY2Lp89B878uhJvXHnHvPGt0ssSI");
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetUser()
    {
        var us = new User
        {
            UserName = "tino2",
            Password = "abcd1234"
        };

        var response = await _httpClient.PostAsJsonAsync("api/Users/BearerToken", us);

        var token = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (token is null) return;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.Token);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetEnvironments()
    {
        var clientid = 1;

        var response = await _httpClient.GetAsync("api/Environments/" + clientid);

        await response.Content.ReadFromJsonAsync<List<Environment>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetNameMaterials()
    {
        string name = "string";

        var response = await _httpClient.GetAsync("api/Materials/" + name);

        await response.Content.ReadFromJsonAsync<Material>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetEnvQuantityMaterials()
    {
        var envid = 2;
        var response = await _httpClient.GetAsync($"api/QuantityMaterials/Environment/{envid}");

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetTaskQuantityMaterials()
    {
        var taskId = 13;
        var response = await _httpClient.GetAsync($"api/QuantityMaterials/Task/{taskId}");

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetClientRobots()
    {
        var clientid = 1;

        var response = await _httpClient.GetAsync("api/Robots/Client/" + clientid);

        List<Robot>? robots = await response.Content.ReadFromJsonAsync<List<Robot>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetEnvRobots()
    {
        var envid = 3;

        var response = await _httpClient.GetAsync("api/Robots/Environment/" + envid);

        await response.Content.ReadFromJsonAsync<List<Robot>>();

        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetClientTasks()
    {
        var clientid = 1;

        var response = await _httpClient.GetAsync("api/Tasks/Client/" + clientid);

        await response.Content.ReadFromJsonAsync<List<CharmieAPI.Models.Task>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetRoboTasks()
    {
        var robotId = 2;

        var response = await _httpClient.GetAsync("api/Tasks/Robot/" + robotId);

        await response.Content.ReadFromJsonAsync<List<CharmieAPI.Models.Task>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetRobotWarnings()
    {
        int robotid = 2;

        var response = await _httpClient.GetAsync("api/Warnings/" + robotid);

        await response.Content.ReadFromJsonAsync<List<Warning>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
