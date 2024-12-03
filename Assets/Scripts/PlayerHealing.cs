using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealing : MonoBehaviour
{
   [SerializeField] private float valorVida;
   private void OnTriggerEnter(Collider other)
{
    Debug.Log("Objeto con el que colisiona: " + other.tag); // Muestra qué objeto está colisionando.
    
    if (other.CompareTag("Player")) // Asegúrate de que el tag es "Player"
    {
        PlayerDamage playerDamage = other.GetComponent<PlayerDamage>();
        if (playerDamage != null)
        {
            Debug.Log("Curando al jugador...");
            playerDamage.masVida(valorVida);
            Destroy(gameObject); // Destruye el objeto de curación
        }
        else
        {
            Debug.LogWarning("El jugador no tiene el script PlayerDamage.");
        }
    }
}

}