using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    
    [SerializeField] private float vidaInicial;
    public float VidaActual { get; private set; }
    [SerializeField] private Rigidbody fisicas;  // Rigidbody 3D

    private Animator animator;

    [SerializeField] private Transform[] respawnPoints;
    private Transform respawnPointActual;
    public float fuerzaRebote = 10f;
    private AudioManager audioManager;
    [SerializeField] private gameoverScreen gameoverScreen;
    [SerializeField] private GameObject hudCanvas;

    private void Awake()
    {
        VidaActual = vidaInicial;
        animator = GetComponent<Animator>();
        fisicas = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        

        if (respawnPoints.Length > 0)
            respawnPointActual = respawnPoints[0];
        
       
    }

    

    public void Daño(float _damage)
    {   
        Debug.Log("Daño recibido: " + _damage);
        VidaActual = Mathf.Clamp(VidaActual - _damage, 0, vidaInicial);
        audioManager.PlayEfectos(audioManager.golpePlayer);

       

         if (VidaActual > 0)
        {
            Debug.Log("auch");

        }


       else if (VidaActual <= 0)
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
    

    // Para que lance al jugador al aire el daño
    //public void Choque(Vector3 direccion)
    //{
    //    Vector3 rebote = (transform.position - direccion).normalized; // Cambié a Vector3
    //    fisicas.AddForce(rebote * fuerzaRebote, ForceMode.Impulse);  // ForceMode.Impulse en 3D
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Se necesitan poner los respawn tanto en mapa como en la lista
    //    if (other.CompareTag("Respawn"))
    //    {
    //        for (int i = 0; i < respawnPoints.Length; i++)
   //         {
    //            {
    //                respawnPointActual = respawnPoints[i];
    //                break;
    //            }
   //         }
    //    }
    //    // Funciona con poner el gameobject con el tag "Caida"
    //    else if (other.CompareTag("Caida"))
    //    {
    //        Invoke("ReiniciarPosicion", 0.1f);  // Se supone que demora 0.1s en activarse
    //    }
    //}

//
   // private void ReiniciarPosicion()
    //{
   //    transform.position = respawnPointActual.position;
   //     VidaActual = vidaInicial;
  //      fisicas.isKinematic = false;  // Habilita la física para el jugador
   // }

    public void masVida(float valor)
{
    VidaActual = Mathf.Clamp(VidaActual + valor, 0, vidaInicial);
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
