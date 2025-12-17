using System;
using System.Collections.Generic;

namespace BlazorApp.Models;

public partial class Lehrer
{
    public int LehrerId { get; set; }

    public string LehrerName { get; set; } = null!;

    public virtual ICollection<Stundenplan> Stundenplans { get; set; } = new List<Stundenplan>();
}
