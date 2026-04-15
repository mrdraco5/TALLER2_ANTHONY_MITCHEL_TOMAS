using UnityEngine;
using System.Collections.Generic;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    private bool puedeCocinar = false;
    private Animator anim;
    public Dictionary<string, int> entregadoEnCaldero = new Dictionary<string, int>();

    void Awake() { instance = this; }

    void Start()
    {
        anim = GetComponent<Animator>();
        ResetearEntregas();
    }

    public void SetModoCrafteo(bool estado)
    {
        puedeCocinar = estado;
        if (anim != null) anim.SetBool("isCharging", estado);
    }

    void ResetearEntregas()
    {
        entregadoEnCaldero.Clear();

        foreach (var p in GameDataLoader.data.pociones)
        {
            entregadoEnCaldero[p.iconoId] = 0;
        }
    }

    void Update()
    {
        if (!puedeCocinar) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) AgregarPorIcono("rojo");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AgregarPorIcono("azul");
        if (Input.GetKeyDown(KeyCode.Alpha3)) AgregarPorIcono("amarillo");
        if (Input.GetKeyDown(KeyCode.Alpha4)) AgregarPorIcono("verde");
    }

    public void AgregarPorIcono(string idIcono)
    {
        if (GameDataLoader.data == null) return;

        Pocion pocion = null;

        foreach (var p in GameDataLoader.data.pociones)
        {
            if (p.iconoId == idIcono)
            {
                pocion = p;
                break;
            }
        }

        if (pocion == null) return;

        int requerido = 0;

        foreach (var r in GameDataLoader.data.recetas)
        {
            if (r.id == GameManager.instance.recetaActual)
            {
                foreach (var obj in r.objetivos)
                {
                    if (obj.pocion == idIcono)
                    {
                        requerido = obj.cantidad;
                        break;
                    }
                }
            }
        }

        int entregado = entregadoEnCaldero.ContainsKey(idIcono) ? entregadoEnCaldero[idIcono] : 0;

        if (entregado >= requerido)
        {
            Debug.Log("Ya no necesitas mas pociones de este tipo");
            return;
        }

        int cantidad = GameManager.instance.ObtenerCantidad(pocion.nombre);

        if (cantidad <= 0) return;

        GameManager.instance.QuitarItem(pocion.nombre);

        entregadoEnCaldero[idIcono]++;

        Debug.Log("Has agregado: " + GameManager.instance.ObtenerTextoColor(pocion.nombre));

        VerificarProgreso();
    }

    void VerificarProgreso()
    {
        int recetaID = GameManager.instance.recetaActual;
        Receta receta = null;

        foreach (var r in GameDataLoader.data.recetas)
        {
            if (r.id == recetaID)
            {
                receta = r;
                break;
            }
        }

        if (receta == null) return;

        bool completa = true;

        foreach (var obj in receta.objetivos)
        {
            if (!entregadoEnCaldero.ContainsKey(obj.pocion) || entregadoEnCaldero[obj.pocion] < obj.cantidad)
                completa = false;
        }

        if (completa)
        {
            GameManager.instance.RecetaCompletada();
            ResetearEntregas();
        }
    }
}