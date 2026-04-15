using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject prefabPocion;
    public Transform[] puntosSpawn;

    void Start()
    {
        SpawnDesdeJSON();
    }

    void SpawnDesdeJSON()
    {
        if (GameDataLoader.data == null)
        {
            Debug.LogError("No hay datos cargados");
            return;
        }

        List<Pocion> listaSpawn = new List<Pocion>();

        foreach (var receta in GameDataLoader.data.recetas)
        {
            foreach (var obj in receta.objetivos)
            {
                Pocion p = ObtenerPorIcono(obj.pocion);

                for (int i = 0; i < obj.cantidad; i++)
                {
                    listaSpawn.Add(p);
                }
            }
        }

        while (listaSpawn.Count < puntosSpawn.Length)
        {
            int randomIndex = Random.Range(0, GameDataLoader.data.pociones.Count);
            listaSpawn.Add(GameDataLoader.data.pociones[randomIndex]);
        }

        MezclarLista(listaSpawn);

        for (int i = 0; i < puntosSpawn.Length; i++)
        {
            Pocion p = listaSpawn[i];

            GameObject obj = Instantiate(prefabPocion, puntosSpawn[i].position, Quaternion.identity);

            ItemRecolectable item = obj.GetComponent<ItemRecolectable>();
            item.nombrePocion = p.nombre;

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>("Potions/" + p.iconoId);

            if (sprite != null)
                sr.sprite = sprite;
        }
    }

    Pocion ObtenerPorIcono(string icono)
    {
        foreach (var p in GameDataLoader.data.pociones)
        {
            if (p.iconoId == icono)
                return p;
        }

        return null;
    }

    void MezclarLista(List<Pocion> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);
            Pocion temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }
}