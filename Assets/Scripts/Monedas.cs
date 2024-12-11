using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monedas : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {

            audioManager.PlayEfectos(audioManager.coleccionable);
            Debug.Log($"El jugador ha recogido una moneda: {gameObject.name}");

            // Llama al sistema global para incrementar el contador
            if (Coins.Instance != null)
            {
                Coins.Instance.AgregarMoneda();
            }
            else
            {
                Debug.LogError("No se encontró el sistema global Coins. Asegúrate de tenerlo en la escena.");
            }

            // Destruye la moneda tras ser recogida
            Destroy(gameObject);
        }
    }
}