using CharmieAPI.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Environment = CharmieAPI.Models.Environment;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using NuGet.Common;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestPost
{
    HttpClient _httpClient;

    public CharmieAPITestPost()
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

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
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
            EnvironmentId = 2
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
                    RobotId = 2,
                    Robot = new Robot
                    {
                        Id = 2,
                        Name = "robot",
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
            RobotId = 2
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
            Name = "teste2",
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
                EnvironmentId = 2,
                Material = new Material {
                    Name = "test2"
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
                TaskId = 13,
                Material = new Material {
                    Name = "test2"
                }
            }
        };
        var response = await _httpClient.PostAsJsonAsync("api/QuantityMaterials", q);

        await response.Content.ReadFromJsonAsync<List<QuantityMaterial>>();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

}
