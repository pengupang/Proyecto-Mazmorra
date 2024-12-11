using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloMortal : MonoBehaviour
{
    [SerializeField] private string tagJugador = "Player"; // Tag del jugador para identificarlo

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene el tag del jugador
        if (collision.gameObject.CompareTag(tagJugador))
        {
            // Intenta obtener el componente PlayerDamage del objeto colisionado
            PlayerDamage playerDamage = collision.gameObject.GetComponent<PlayerDamage>();

            if (playerDamage != null)
            {
                // Mata al jugador directamente
                playerDamage.Daño(playerDamage.VidaActual); // Reduce la vida a cero
                Debug.Log("Jugador ha sido eliminado por el suelo mortal.");
            }
            else
            {
                Debug.LogWarning("El objeto no tiene un componente PlayerDamage.");
            }
        }
    }
}
