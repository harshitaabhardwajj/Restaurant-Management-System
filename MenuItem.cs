using System;
using System.Collections.Generic;

namespace ManagementSystem1.Models;

public partial class MenuItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }
}
