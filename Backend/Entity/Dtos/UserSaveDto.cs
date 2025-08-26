namespace CoreLayer.Dtos
{
    public class UserSaveDto
    {
        public string UserName { get; set; }
        public string EmpName { get; set; }
        public string NatId { get; set; }
        public int DeptId { get; set; }
        public bool ExtClctr { get; set; }
        public bool Stopped { get; set; }
        public List<int> WorkGroupIds { get; set; }
    }
}
