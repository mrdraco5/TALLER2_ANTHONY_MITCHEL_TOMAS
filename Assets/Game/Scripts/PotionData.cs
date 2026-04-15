using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public List<Pocion> pociones;
    public List<Receta> recetas;
}

[System.Serializable]
public class Pocion
{
    public string nombre;
    public int valor;
    public string iconoId;
}

[System.Serializable]
public class Receta
{
    public int id;
    public string nombre;
    public List<Objetivo> objetivos;
}

[System.Serializable]
public class Objetivo
{
    public string pocion;
    public int cantidad;
}