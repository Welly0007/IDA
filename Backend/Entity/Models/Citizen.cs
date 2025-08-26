using CoreLayer.Interfaces;

namespace CoreLayer.Models;

public partial class Citizen : IentityBase
{
    public string Name { get; set; } = null!;

    public string NatId { get; set; } = null!;
}
