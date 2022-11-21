using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using StudyBuddy.WebUi.Models;
using StudyBuddy.WebUi.Utils;
using StudyBuddy.WebUi.Wrappers;

namespace StudyBuddy.WebUi.Pages.PageProcess;

public class UserListProcess_razor:ComponentBase
{
    [Inject]
    public HttpClient Client { get; set; }

    protected List<UserDto> userList = new List<UserDto>();

    protected  override async Task OnInitializedAsync()
    {
        await LoadList();
    }

    protected async Task LoadList()
    {
        var serviceResponse = await Client.GetServiceResponseAsync<List<UserDto>>("https://localhost:7042/api/user/all");
        if (serviceResponse.StatusCode==200)
        {
            userList = serviceResponse.Data;
            

        }
    }
}