using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docente
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
}

public class Aula
{
    public string Id { get; set; }
    public string Nombre { get; set; }
}

public class Hora
{
    public string H { get; set; }
    public string M { get; set; }
}

public class Materia
{
    public string Nombre { get; set; }
}

public class Clase
{
    public Docente Docente { get; set; }
    public Aula Aula { get; set; }
    public Materia Materia { get; set; }
    public Hora HoraDesde { get; set; }
    public Hora HoraHasta { get; set; }

    public override string ToString()
    {
        return $"Docente:{Docente?.Nombre} {Docente?.Apellido}\n" +
               $"Materia:{Materia?.Nombre}\n" +
               $"Horario:{HoraDesde?.H}:{HoraDesde?.M} - {HoraHasta?.H}:{HoraHasta?.M}\n";
    }
    public string getAula()
    {
        return Aula?.Nombre;
    }
}