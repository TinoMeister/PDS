using CharmieAPI.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Environment = CharmieAPI.Models.Environment;
using System.Net.Http.Json;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestPost
{
    HttpClient _httpClient;

    public CharmieAPITestPost()
    {
        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();

        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostEnvironments()
    {
        var en = new Environment
        {
            Name = "teste",
            Length = 20,
            Width = 20,
            ClientId = 1
        };
        var response = await _httpClient.PostAsJsonAsync("api/Environments", en);

        await response.Content.ReadFromJsonAsync<Environment>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostRobots()
    {
        var r = new Robot
        {
            Name = "robot",
            EnvironmentId = 1
        };

        var response = await _httpClient.PostAsJsonAsync("api/Robots", r);

        await response.Content.ReadFromJsonAsync<Robot>();

        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostTasks()
    {
        var t = new CharmieAPI.Models.Task
        {
            Name = "test",
            InitHour = "08:00:00",
            EndHour = "09:00:00",
            WeekDays = "seg",
            TasksRobots = new List<TaskRobot>
            {
                new TaskRobot
                {
                    RobotId = 1,
                    Robot = new Robot
                    {
                        Id = 1,
                        Name = "string",
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("api/Tasks", t);

        await response.Content.ReadFromJsonAsync<CharmieAPI.Models.Task>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostWarnings()
    {
        var w = new Warning
        {
            Message = "string",
            HourDay = DateTime.Now,
            RobotId = 1
        };
        var response = await _httpClient.PostAsJsonAsync("api/Warnings", w);

        await response.Content.ReadFromJsonAsync<Warning>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostMaterial()
    {
        var m = new Material
        {
            Name = "teste",
        };

        var response = await _httpClient.PostAsJsonAsync("api/Materials", m);

        await response.Content.ReadFromJsonAsync<Material>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostQuantityMaterial()
    {
        var q = new List<QuantityMaterial>
        {
            new QuantityMaterial {
                Quantity = 0,
                EnvironmentId = 1,
                Material = new Material {
                    Name = "string"
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("api/QuantityMaterials", q);

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPostQuantityMaterial2()
    {
        var q = new List<QuantityMaterial>
        {
            new QuantityMaterial {
                Quantity = 0,
                TaskId = 1,
                Material = new Material {
                    Name = "string"
                }
            }
        };
        var response = await _httpClient.PostAsJsonAsync("api/QuantityMaterials", q);

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

}
