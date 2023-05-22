using CharmieAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Environment = CharmieAPI.Models.Environment;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestPut
{
    HttpClient _httpClient;

    public CharmieAPITestPut()
    {
        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();

        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutEnvironment()
    {
        int id = 1;

        Environment updatedEn = new Environment
        {
            Id = 1,
            Name = "envi",
            Length = 30,
            Width = 30,
            ClientId = 1
        };
        var response = await _httpClient.PutAsJsonAsync($"api/Environments/{id}", updatedEn);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutRobots()
    {
        int id = 1;
        Robot updatedR = new Robot
        {
            Id = 1,
            Name = "string",
            EnvironmentId = 1
        };

        var response = await _httpClient.PutAsJsonAsync($"api/Robots/{id}", updatedR);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutTasks()
    {
        var id = 1;

        var t = new CharmieAPI.Models.Task
        {
            Id = 1,
            Name = "test2",
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
                        Name = "string"
                    }
                }
            }
        };

        var response = await _httpClient.PutAsJsonAsync($"api/Tasks/{id}", t);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutWarnings()
    {
        int id = 1;
        Warning updatedW = new Warning
        {
            Id = 1,
            Message = "string2",
            HourDay = DateTime.Now,
            RobotId = 1,
            UserId = 1
        };

        var response = await _httpClient.PutAsJsonAsync($"api/Warnings/{id}", updatedW);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutMaterial()
    {
        int id = 1;
        Environment updatedM = new Environment
        {
            Id = 1,
            Name = "mat"
        };

        var response = await _httpClient.PutAsJsonAsync($"api/Materials/{id}", updatedM);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutQuantityMaterial()
    {
        var q = new List<QuantityMaterial>
        {
            new QuantityMaterial {
                Id = 1,
                Quantity = 0,
                EnvironmentId = 1,
                Material = new Material {
                    Name = "string"
                }
            }
        };

        var response = await _httpClient.PutAsJsonAsync("api/QuantityMaterials", q);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutQuantityMaterial2()
    {
        var q = new List<QuantityMaterial>
        {
            new QuantityMaterial {
                Id = 1,
                Quantity = 0,
                TaskId = 1,
                Material = new Material {
                    Name = "string"
                }
            }
        };

        var response = await _httpClient.PutAsJsonAsync("api/QuantityMaterials", q);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
