using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;
    private Animator animator;
    private bool estaMuerto = false;
    public float VidaActual => vidaActual;

    private AudioManager audioManager;
    [SerializeField] private gameoverScreen gameoverScreen;
    [SerializeField] private GameObject hudCanvas;

    private void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void RecibirDano(float dano)
    {
        vidaActual -= dano;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); 
        audioManager.PlayEfectos(audioManager.golpePlayer);

        if (vidaActual <= 0)
        {
            Muerte();
        }

        
    }

    private void Muerte()
    {
        estaMuerto = true; 
        animator.SetTrigger("muete");

        int score = Coins.Instance != null ? Coins.Instance.ObtenerPuntuacion() : 0;

            // Configura la pantalla de Game Over
        gameoverScreen.Setup(score);
            
        hudCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        Cursor.visible = true;

            

        Debug.Log($"Game Over con puntaje: {score}");
       
        DetenerMovimiento();

        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero; 
            rb.isKinematic = true;      
        }

        
        this.enabled = false;
        
    }

    private void DetenerMovimiento()
    {
        
        animator.SetFloat("Xspeed", 0);
        animator.SetFloat("Yspeed", 0);
    }

    

}
