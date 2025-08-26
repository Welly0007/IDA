namespace CoreLayer.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string NatId { get; set; } = string.Empty;
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public bool ExtClctr { get; set; }
        public bool Stopped { get; set; } = false;
        public List<int> WorkGroupIds { get; set; } = new List<int>();
        public List<string> WorkGroups { get; set; } = new List<string>();
    }
}
