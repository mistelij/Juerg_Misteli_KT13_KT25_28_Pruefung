using System;
using System.Collections.Generic;

namespace BlazorApp.Models;

public partial class Klassen
{
    public int KlassenId { get; set; }

    public string KlassenName { get; set; } = null!;

    public virtual ICollection<Stundenplan> Stundenplans { get; set; } = new List<Stundenplan>();
}
