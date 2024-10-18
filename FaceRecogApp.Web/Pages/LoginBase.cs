using FaceRecogApp.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FaceRecogApp.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IFaceService FaceService { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task<IList<Guid>> CallIdentifyEmployee(String ImageString)
        {
            var employeeGuids = await FaceService.IdentifyEmployee(ImageString);
            return employeeGuids;
        }

        protected async Task<bool> CallUpdateAttendance(Guid guid)
        {
            var updateResult = await EmployeeService.UpdateEmployeeAttendance(guid);

            if (updateResult)
            {
                NavigationManager.NavigateTo("/");

            }

            return updateResult;
        }

        protected void GoToIndex()
        {  
        NavigationManager.NavigateTo("/");
   
        }

    }
}
