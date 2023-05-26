using CharmieAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestDel
{
    HttpClient _httpClient;

    public CharmieAPITestDel()
    {
        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();

        _httpClient = webAppFactory.CreateDefaultClient();
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
    public async System.Threading.Tasks.Task TestDeleteQuantityMaterials()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/QuantityMaterials/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestDeleteMaterials()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/Materials/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestDeleteRobots()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/Robots/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestDeleteEnvironment()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/Environments/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestDeleteTasks()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/Tasks/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestDeleteWarnings()
    {
        int id = 1;
        var response = await _httpClient.DeleteAsync($"api/Warnings/{id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
