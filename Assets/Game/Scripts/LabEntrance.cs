using UnityEngine;
using UnityEngine.SceneManagement;

public class LabEntrance : MonoBehaviour
{
    public string nombreEscenaLab = "Laboratorio";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PuedeEntrar())
            {
                SceneManager.LoadScene(nombreEscenaLab);
            }
            else
            {
                Debug.Log("<color=orange>BLOQUEADO: No tienes todas las pociones requeridas.</color>");
            }
        }
    }

    bool PuedeEntrar()
    {
        if (GameDataLoader.data == null) return false;

        foreach (var p in GameDataLoader.data.pociones)
        {
            int requeridoTotal = 0;
            foreach (var r in GameDataLoader.data.recetas)
            {
                foreach (var obj in r.objetivos)
                {
                    if (obj.pocion == p.iconoId) requeridoTotal += obj.cantidad;
                }
            }

            if (GameManager.instance.ObtenerCantidad(p.nombre) < requeridoTotal)
            {
                return false;
            }
        }
        return true;
    }
}