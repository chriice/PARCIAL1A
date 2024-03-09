using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public virtual ICollection<Autorlibro> Autorlibros { get; set; } = new List<Autorlibro>();
}
