using System;
using System.Collections.Generic;

namespace BlazorApp.Models;

public partial class Schulzimmer
{
    public int ZimmerId { get; set; }

    public string ZimmerBezeichnung { get; set; } = null!;

    public virtual ICollection<Stundenplan> Stundenplans { get; set; } = new List<Stundenplan>();
}
