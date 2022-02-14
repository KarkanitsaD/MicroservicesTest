namespace EmployeesService.Data
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}