using UnityEngine;

public class ItemFlotante : MonoBehaviour
{
    public float velocidad = 2f;
    public float altura = 0.25f;

    float yInicial;

    void Start()
    {
        yInicial = transform.position.y;
    }

    void Update()
    {
        float nuevaY = yInicial + Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = new Vector2(transform.position.x, nuevaY);
    }
}