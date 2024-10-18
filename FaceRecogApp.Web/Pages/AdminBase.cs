using FaceRecogApp.Web.Dto;
using FaceRecogApp.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FaceRecogApp.Web.Pages
{
    public class AdminBase : ComponentBase
    {
        [Inject]
        IEmployeeService EmployeeService { get; set; }

        [Inject]
        IFaceService FaceService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<EmployeeDto> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetEmployees();
        }

        protected async Task<bool> CallCreateEmployee(EmployeeDto employeeDto, int jobTitleId, string personGroupId)
        {
            return await EmployeeService.CreateEmployee(employeeDto, jobTitleId, personGroupId);
        }

        protected async Task<bool> CallDeleteEmployee(int employeeId, string personGroupId)
        {
            return await EmployeeService.DeleteEmployee(employeeId, personGroupId);
        }

        protected async Task<bool> CallAddFaceToEmployee(string personGroupId, int employeeId, string imageString)
        {
            return await FaceService.AddFaceToEmployee(personGroupId, employeeId, imageString);
        }

        protected async Task<bool> CallTrainPersonGroup(string personGroupId)
        {
            return await FaceService.TrainPersonGroup(personGroupId);
        }

        protected void RefreshPage()
        {
            NavigationManager.NavigateTo("/admin", true);
        }
    }
}

