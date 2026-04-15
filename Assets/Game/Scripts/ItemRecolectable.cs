using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public string nombrePocion;
    public AudioClip sonidoRojo;
    public AudioClip sonidoAzul;
    public AudioClip sonidoVerde;
    public AudioClip sonidoAmarillo;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string idColor = "";
            if (GameDataLoader.data != null)
            {
                foreach (var p in GameDataLoader.data.pociones)
                {
                    if (p.nombre == nombrePocion)
                    {
                        idColor = p.iconoId;
                        break;
                    }
                }
            }

            AudioClip clip = ObtenerSonidoPorColor(idColor);

            if (clip != null)
            {
                AudioSource playerAudio = other.GetComponent<AudioSource>();
                if (playerAudio != null)
                {
                    playerAudio.PlayOneShot(clip);
                }
            }

            GameManager.instance.AgregarItem(nombrePocion);
            Destroy(gameObject);
        }
    }

    AudioClip ObtenerSonidoPorColor(string color)
    {
        if (color == "rojo") return sonidoRojo;
        if (color == "azul") return sonidoAzul;
        if (color == "verde") return sonidoVerde;
        if (color == "amarillo") return sonidoAmarillo;
        return null;
    }
}