using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI txtRojo, txtAzul, txtVerde, txtAmarillo;
    public GameObject receta1UI, receta2UI, panelFelicidades1, panelFelicidades2, panelFinJuego;

    void Awake() { instance = this; }

    void Start()
    {
        ActualizarVisibilidadRecetas();
        if (panelFelicidades1 != null) panelFelicidades1.SetActive(false);
        if (panelFelicidades2 != null) panelFelicidades2.SetActive(false);
        if (panelFinJuego != null) panelFinJuego.SetActive(false);
    }

    public void ActualizarVisibilidadRecetas()
    {
        int actual = GameManager.instance.recetaActual;
        if (receta1UI != null) receta1UI.SetActive(actual == 1);
        if (receta2UI != null) receta2UI.SetActive(actual == 2);
    }

    void Update()
    {
        ActualizarTexto(txtRojo, "rojo");
        ActualizarTexto(txtAzul, "azul");
        ActualizarTexto(txtVerde, "verde");
        ActualizarTexto(txtAmarillo, "amarillo");
    }

    void ActualizarTexto(TextMeshProUGUI texto, string idIcono)
    {
        if (texto == null || GameDataLoader.data == null) return;

        string nombreReal = "";
        foreach (var p in GameDataLoader.data.pociones)
        {
            if (p.iconoId == idIcono)
            {
                nombreReal = p.nombre;
                break;
            }
        }

        int requerido = 0;

        bool enLaboratorio = RecipeManager.instance != null;

        if (enLaboratorio)
        {
            foreach (var r in GameDataLoader.data.recetas)
            {
                if (r.id == GameManager.instance.recetaActual)
                {
                    foreach (var obj in r.objetivos)
                    {
                        if (obj.pocion == idIcono)
                            requerido = obj.cantidad;
                    }
                }
            }

            int inventario = GameManager.instance.ObtenerCantidad(nombreReal);
            texto.text = inventario + "/" + requerido;
        }
        else
        {
            foreach (var r in GameDataLoader.data.recetas)
            {
                foreach (var obj in r.objetivos)
                {
                    if (obj.pocion == idIcono)
                        requerido += obj.cantidad;
                }
            }

            int inventario = GameManager.instance.ObtenerCantidad(nombreReal);
            texto.text = inventario + "/" + requerido;
        }
    }

    public void MostrarReceta1Completada() { StartCoroutine(MostrarPanel1()); }

    IEnumerator MostrarPanel1()
    {
        if (panelFelicidades1 != null) panelFelicidades1.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (panelFelicidades1 != null) panelFelicidades1.SetActive(false);
        ActualizarVisibilidadRecetas();
    }

    public void MostrarReceta2Completada() { StartCoroutine(MostrarFinal()); }

    IEnumerator MostrarFinal()
    {
        if (panelFelicidades2 != null) panelFelicidades2.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (panelFelicidades2 != null) panelFelicidades2.SetActive(false);
        if (panelFinJuego != null) panelFinJuego.SetActive(true);
        Time.timeScale = 0f;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}