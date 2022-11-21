
using Blazored.Modal;
using Blazored.Modal.Services;
using StudyBuddy.WebUi.CustomComponents;

namespace StudyBuddy.WebUi.Utils;

public class ModalManager
{
    private readonly IModalService modalService;

    public ModalManager(IModalService ModalService)
    {
        modalService = ModalService;
    }
    public async Task ShowMessageAsync(String Title, String Message)
    {
        ModalParameters mParams= new ModalParameters() ;
        mParams.Add("Message", Message);
        
        var modalref= modalService.Show<ShowMessagePopupComponent>(Title,mParams);

        await modalref.Result;
    }
}