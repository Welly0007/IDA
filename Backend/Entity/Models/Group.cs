using CoreLayer.Interfaces;

namespace CoreLayer.Models;

public partial class Group : IentityBase
{
    public string? Name { get; set; }

    public int DeptId { get; set; }

    public virtual Dept Dept { get; set; } = null!;

    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
}
