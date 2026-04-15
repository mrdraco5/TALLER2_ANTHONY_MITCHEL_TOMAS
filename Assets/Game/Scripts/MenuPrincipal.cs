using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelInstrucciones;

    void Start()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("RuinasAntiguas");
    }

    public void MostrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}