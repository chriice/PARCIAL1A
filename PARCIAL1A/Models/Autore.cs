using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Autore
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Autorlibro> Autorlibros { get; set; } = new List<Autorlibro>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
