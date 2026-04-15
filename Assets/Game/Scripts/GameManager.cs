using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<string, int> inventario = new Dictionary<string, int>();
    public int recetaActual = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AgregarItem(string nombre)
    {
        if (inventario.ContainsKey(nombre))
            inventario[nombre]++;
        else
            inventario[nombre] = 1;
        Debug.Log("Recolectado: " + ObtenerTextoColor(nombre));
    }

    public string ObtenerTextoColor(string nombreEnInventario)
    {
        string colorHex = "white";
        if (GameDataLoader.data != null)
        {
            foreach (var p in GameDataLoader.data.pociones)
            {
                if (p.nombre == nombreEnInventario)
                {
                    if (p.iconoId == "rojo") colorHex = "red";
                    else if (p.iconoId == "azul") colorHex = "blue";
                    else if (p.iconoId == "verde") colorHex = "green";
                    else if (p.iconoId == "amarillo") colorHex = "yellow";
                    break;
                }
            }
        }
        return "<color=" + colorHex + ">" + nombreEnInventario + "</color>";
    }

    public int ObtenerCantidad(string nombre)
    {
        if (inventario.ContainsKey(nombre))
            return inventario[nombre];
        return 0;
    }

    public void QuitarItem(string nombre)
    {
        if (inventario.ContainsKey(nombre) && inventario[nombre] > 0)
            inventario[nombre]--;
    }

    public void RecetaCompletada()
    {
        string nombreR = ObtenerNombreReceta(recetaActual);
        if (recetaActual == 1)
        {
            Debug.Log("<color=red>Felicidades, hiciste la pocion: " + nombreR + "</color>");
            UIManager.instance.MostrarReceta1Completada();
            recetaActual = 2;
        }
        else if (recetaActual == 2)
        {
            Debug.Log("<color=green>Felicidades, hiciste la pocion: " + nombreR + "</color>");
            UIManager.instance.MostrarReceta2Completada();
        }
    }

    string ObtenerNombreReceta(int id)
    {
        if (GameDataLoader.data == null) return "Receta";
        foreach (var r in GameDataLoader.data.recetas)
        {
            if (r.id == id) return r.nombre;
        }
        return "Receta";
    }
}