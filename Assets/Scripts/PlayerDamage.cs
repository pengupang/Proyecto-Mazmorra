using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private Rigidbody fisicas;  // Rigidbody 3D
    [SerializeField] private float vidaMaxima = 100f;
    
    private float vidaActual;
    private Animator animator;
    private bool estaMuerto = false;
    public float VidaActual => vidaActual;

    [SerializeField] private Transform[] respawnPoints;
    private Transform respawnPointActual;
    public float fuerzaRebote = 10f;
    private AudioManager audioManager;
    [SerializeField] private gameoverScreen gameoverScreen;
    [SerializeField] private GameObject hudCanvas;

    private void Awake()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        fisicas = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (respawnPoints.Length > 0)
            respawnPointActual = respawnPoints[0];
    }

    public void Daño(float dano)
    {
        vidaActual -= dano;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        audioManager.PlayEfectos(audioManager.golpePlayer);

        if (vidaActual > 0)
        {
            Debug.Log("auch");
        }
        else if (vidaActual <= 0 && !estaMuerto)  // Aseguramos que solo se ejecute una vez
        {
            estaMuerto = true;  // Evitar que se ejecute varias veces
            Muerte();  // Llamada al método Muerte() para detener el movimiento y deshabilitar colisiones

            // Obtén el puntaje desde el Singleton Coins
            int score = Coins.Instance != null ? Coins.Instance.ObtenerPuntuacion() : 0;

            // Llama a la corrutina para mostrar Game Over después de un tiempo
            StartCoroutine(EsperarYMostrarGameOver(score));

            Debug.Log($"Game Over con puntaje: {score}");
        }
    }

    private IEnumerator EsperarYMostrarGameOver(int score)
    {
        // Espera el tiempo de duración de la animación de muerte (ajusta el tiempo según tu animación)
        yield return new WaitForSeconds(2f); // Ajusta el tiempo según la duración de tu animación de muerte (2 segundos en este ejemplo)

        // Esconde el HUD y desbloquea el cursor
        hudCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Muestra la pantalla de Game Over
        gameoverScreen.Setup(score);
    }

    private void Muerte()
    {
        // Detener movimiento
        DetenerMovimiento();

        // Desactivar colisiones y físicas
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero; 
            rb.isKinematic = true;      
        }

        // Ejecuta la animación de muerte
        animator.SetTrigger("muete");

        this.enabled = false;  // Deshabilitar este script
    }

    private void DetenerMovimiento()
    {
        animator.SetFloat("Xspeed", 0);
        animator.SetFloat("Yspeed", 0);
    }

    public void masVida(float valor)
    {
        vidaActual = Mathf.Clamp(vidaActual + valor, 0, vidaMaxima);
        audioManager.PlayEfectos(audioManager.vida);

        Debug.Log($"Vida aumentada: {valor}, Vida actual: {VidaActual}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tecla E presionada");
            Daño(10);
        }
    }
}
