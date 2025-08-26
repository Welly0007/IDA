using CoreLayer.Interfaces;

namespace CoreLayer.Models;

public partial class User : IentityBase
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public int EmpId { get; set; }

    public bool ExtClctr { get; set; }

    public bool Stopped { get; set; }

    public virtual Emp Emp { get; set; } = null!;

    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
}
