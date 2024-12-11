using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;              // Vida del enemigo
    public int danoArma;                              // Daño recibido
    [SerializeField] private float radioDeteccion;    // Rango de detección
    [SerializeField] private float velocidad;         // Velocidad de movimiento
    [SerializeField] private Transform posicionJugador; // Referencia al jugador
    [SerializeField] private LayerMask layerJugador;  // Capa del jugador
    [SerializeField] private float distanciaParaAtacar = 1.5f; // Distancia para atacar

    [SerializeField] private LayerMask Pared;  // Capa para detectar paredes u obstáculos
    [SerializeField] private float distanciaRaycast = 1.0f;

    private bool detectado;
    private bool estaAtacando = false;
    private bool estaMuerto = false; // Bandera para verificar si está muerto
    private Animator animator;
    private Collider collider;

    private AudioManager audioManager;

    private PlayerDamage takeDamage; 

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


        if (posicionJugador != null)
        {
            takeDamage = posicionJugador.GetComponent<PlayerDamage>();
        }
    }

    void Update()
    {
        if (estaMuerto) return; // Si está muerto, no hacer nada

        detectado = Physics.CheckSphere(transform.position, radioDeteccion, layerJugador);

        if (!detectado)
        {
            DetenerMovimiento();
            return;
        }

        float distancia = Vector3.Distance(transform.position, posicionJugador.position);

        if (estaAtacando) return;

        if (distancia <= distanciaParaAtacar)
        {
            Atacar();
        }
        else
        {
            MoverHaciaJugador();
        }
    }

    private void MoverHaciaJugador()
    {
        transform.LookAt(posicionJugador);

        Vector3 direccion = (posicionJugador.position - transform.position).normalized;
        
        if (!HayObstaculo(direccion))
        {
            transform.position += direccion * velocidad * Time.deltaTime;
        }

        animator.SetFloat("Xeje", direccion.x);
        animator.SetFloat("Yeje", direccion.z);
    }

    private bool HayObstaculo(Vector3 direccion)
    {
        // Usar un Raycast para detectar obstáculos
        Ray ray = new Ray(transform.position, direccion);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaRaycast, Pared))
        {
            // Si detecta un obstáculo, evitar moverse en esa dirección
            return true;
        }
        return false;
    }
    private void Atacar()
    {
        animator.SetTrigger("attack");
        estaAtacando = true;
        audioManager.PlayEfectos(audioManager.EnemigoGolpe);

        StartCoroutine(EsperarAtaque());
    }

    private IEnumerator EsperarAtaque()
    {
         yield return new WaitForSeconds(1.5f); // Tiempo para terminar la animación de ataque

        if (takeDamage != null)
        {
            float distancia = Vector3.Distance(transform.position, posicionJugador.position);
            if (distancia <= distanciaParaAtacar)
            {
                takeDamage.Daño(danoArma);
            }
        }

        estaAtacando = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estaMuerto) return; // No recibir daño si ya está muerto

        if (other.gameObject.tag == "armaImpacto")
        {
            vida -= danoArma;

            if (animator != null)
            {
                animator.Play("danoEnemigo");
                audioManager.PlayEfectos(audioManager.EnemigoDaño);
            }

            if (vida <= 0)
            {
                Muerte();
            }
        }
    }

    private void Muerte()
    {
        estaMuerto = true; 
        animator.SetTrigger("Muerte");
        audioManager.PlayEfectos(audioManager.EnemigoMuerte);

       
        DetenerMovimiento();

        if (collider != null)
        {
            collider.enabled = false;
        }

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero; // Detener movimiento
            rb.isKinematic = true;      // Desactivar física
        }

        
        this.enabled = false;
    }

    private void DetenerMovimiento()
    {
        // Detener las animaciones de movimiento
        animator.SetFloat("Xeje", 0);
        animator.SetFloat("Yeje", 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaParaAtacar);
    }
}
