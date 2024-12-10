using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Coins Instance { get; private set; }

    [SerializeField] TMP_Text hudText;
    private int contador = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Asegura que solo hay una instancia
        }
        else
        {
            Instance = this;
        }
    }

    public void AgregarMoneda()
    {
        contador++;
        if (hudText != null)
        {
            hudText.text = $"{contador}";
        }
    }

    public int ObtenerPuntuacion()
    {
        return contador;
    }
}
