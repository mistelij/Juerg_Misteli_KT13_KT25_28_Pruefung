using System;
using System.Collections.Generic;

namespace BlazorApp.Models;

public partial class Stundenplan
{
    public int StundenplanId { get; set; }

    public int KlassenId { get; set; }

    public int LehrerId { get; set; }

    public int ZimmerId { get; set; }

    public DateOnly Datum { get; set; }

    public TimeOnly Uhrzeit { get; set; }

    public virtual Klassen Klassen { get; set; } = null!;

    public virtual Lehrer Lehrer { get; set; } = null!;

    public virtual Schulzimmer Zimmer { get; set; } = null!;
}
