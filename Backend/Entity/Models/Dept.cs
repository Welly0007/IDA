using CoreLayer.Interfaces;

namespace CoreLayer.Models;

public partial class Dept   :IentityBase
{
    public string Name { get; set; } = null!;
}
