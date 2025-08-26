using CoreLayer.Interfaces;

namespace CoreLayer.Models;

public partial class Emp : IentityBase
{
    public int CtznId { get; set; }

    public int DeptId { get; set; }

    public virtual Citizen Ctzn { get; set; } = null!;

    public virtual Dept Dept { get; set; } = null!;

}
