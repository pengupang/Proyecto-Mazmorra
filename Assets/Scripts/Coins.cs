using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Coins Instance { get; private set; }

    [SerializeField] private TMP_Text hudText; // Texto que muestra las monedas recogidas
    private int contador = 0; // Contador de monedas global

    private void Awake()
    {
        // Configuración del Singleton para evitar duplicados
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantén este objeto al cambiar de escena
        }
    }

    public void AgregarMoneda()
    {
        contador++;
        if (hudText != null)
        {
            hudText.text = $"{contador}"; // Actualiza el HUD con el contador
            Debug.Log($"Moneda recogida. Total: {contador}");
        }
        else
        {
            Debug.LogWarning("HUD no asignado en Coins.");
        }
    }

    public int ObtenerPuntuacion()
    {
        return contador;
    }
}
