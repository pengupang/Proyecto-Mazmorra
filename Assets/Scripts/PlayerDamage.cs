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
        vidaActual = Mathf.Clamp(vidaActual - dano, 0, vidaMaxima);
        audioManager.PlayEfectos(audioManager.golpePlayer);

       

         if (vidaActual > 0)
        {
            Debug.Log("auch");

        }


       else if (vidaActual <= 0)
        {
            animator.SetTrigger("muete");
            fisicas.isKinematic = true;

            // Obtén el puntaje desde el Singleton Coins
            int score = Coins.Instance != null ? Coins.Instance.ObtenerPuntuacion() : 0;

            // Configura la pantalla de Game Over
            gameoverScreen.Setup(score);
            
            hudCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true;

            

            Debug.Log($"Game Over con puntaje: {score}");
        }



          
        }
    

    private void Muerte()
    {
        estaMuerto = true; 
        animator.SetTrigger("muete");

       
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
