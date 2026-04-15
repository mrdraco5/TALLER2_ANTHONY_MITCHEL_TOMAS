using UnityEngine;
using System.IO;

public class GameDataLoader : MonoBehaviour
{
    public static PotionData data;

    void Awake()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "PotionData.json");

        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            data = JsonUtility.FromJson<PotionData>(json);
            Debug.Log("JSON cargado correctamente");
        }
        else
        {
            Debug.LogError("No se encontro el JSON");
        }
    }
}