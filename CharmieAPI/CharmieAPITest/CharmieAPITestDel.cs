using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

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
