using CharmieAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Environment = CharmieAPI.Models.Environment;
using System.Net.Http.Headers;

namespace CharmieAPITest;

[TestClass]
public class CharmieAPITestPut
{
    HttpClient _httpClient;

    public CharmieAPITestPut()
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
    public async System.Threading.Tasks.Task TestPutEnvironment()
    {
        int id = 2;

        Environment updatedEn = new Environment
        {
            Id = 2,
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
        int id = 2;
        Robot updatedR = new Robot
        {
            Id = 2,
            Name = "string",
            EnvironmentId = 3
        };

        var response = await _httpClient.PutAsJsonAsync($"api/Robots/{id}", updatedR);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task TestPutTasks()
    {
        var id = 13;

        var t = new CharmieAPI.Models.Task
        {
            Id = 13,
            Name = "test2",
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
            RobotId = 2,
            IdentityId = "1b9dd5e4-0d0e-4535-9864-db41aa21d58e"
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
                Id = 5,
                Quantity = 0,
                EnvironmentId = 3,
                Material = new Material {
                    Name = "test"
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
                Id = 7,
                Quantity = 0,
                TaskId = 4,
                Material = new Material {
                    Name = "test"
                }
            }
        };

        var response = await _httpClient.PutAsJsonAsync("api/QuantityMaterials", q);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
