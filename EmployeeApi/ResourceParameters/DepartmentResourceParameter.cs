namespace EmployeeApi.ResourceParameters
{
    public class DepartmentResourceParameter : ResourcesParameters
    {
        public string Headquarter { get; set; }

        public override string OrderBy { get; set; } = "departmentName";
    }
}