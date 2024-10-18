using FaceRecogApp.Web.Dto;
using FaceRecogApp.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FaceRecogApp.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public List<EmployeeDto> EmployeeList {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeService.GetEmployeesByTime();
        } 
    }
}
