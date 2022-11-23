using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using StudyBuddy.WebUi.CustomException;
using StudyBuddy.WebUi.Models;
using StudyBuddy.WebUi.Utils;
using StudyBuddy.WebUi.Wrappers;

namespace StudyBuddy.WebUi.Pages.PageProcess;

public class UserListProcess_razor:ComponentBase
{
    [Inject]
    public HttpClient Client { get; set; }

    [Inject] 
    ModalManager ModalManager { get; set; }
    
    protected List<UserDto> userList = new List<UserDto>();

    protected  override async Task OnInitializedAsync()
    {
        await LoadList();
    }

    protected async Task LoadList()
    {
       
        try
        {
           userList = (await Client.GetServiceResponseAsync<List<UserDto>>("https://localhost:7042/api/user/all/100/1", true)).Data;

        }
        catch (ApiException e)
        {

           await ModalManager.ShowMessageAsync("Api Exception", e.Message);
        }
        catch (Exception e)
        {

            await ModalManager.ShowMessageAsync("Exception", e.Message);
        }
    }
}