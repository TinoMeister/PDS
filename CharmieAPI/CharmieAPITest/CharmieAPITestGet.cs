using CharmieAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
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
        var envid = 1;
        var response = await _httpClient.GetAsync($"api/QuantityMaterials/Environment/{envid}");

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestGetTaskQuantityMaterials()
    {
        var taskId = 1;
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
        var envid = 1;

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
        var robotId = 1;

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
