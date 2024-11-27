using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private float radioDeteccion; 
    [SerializeField] private float velocidad;     
    [SerializeField] private Transform posicionJugador; 
    [SerializeField] private LayerMask layerJugador;    
    [SerializeField] private float distanciaParaAtacar = 1.5f; 

    private bool detectado;  
    private Animator animator; 
    private bool estaAtacando;

    private float velocidadSuavizadaX;
    private float velocidadSuavizadaY;
    private float suavizadoFactor = 10f;  

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        detectado = Physics.CheckSphere(transform.position, radioDeteccion, layerJugador);

        if (!detectado)
        {
            SmoothTransition(0, 0); 
            return;
        }

        float distancia = Vector3.Distance(transform.position, posicionJugador.position);

        if (estaAtacando)
        {
            return;
        }

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

        transform.position = Vector3.MoveTowards(transform.position, posicionJugador.position, velocidad * Time.deltaTime);

        SmoothTransition(direccion.x, direccion.z);
    }

    private void SmoothTransition(float targetX, float targetY)
    {
        velocidadSuavizadaX = Mathf.Lerp(velocidadSuavizadaX, targetX, Time.deltaTime * suavizadoFactor);
        velocidadSuavizadaY = Mathf.Lerp(velocidadSuavizadaY, targetY, Time.deltaTime * suavizadoFactor);

        animator.SetFloat("Xeje", velocidadSuavizadaX);
        animator.SetFloat("Yeje", velocidadSuavizadaY);
    }

    private void Atacar()
    {
        animator.SetTrigger("attack");

        SmoothTransition(0, 0);

        estaAtacando = true;

        StartCoroutine(EsperarAtaque());
    }

    private IEnumerator EsperarAtaque()
    {
        yield return new WaitForSeconds(1.5f); 

        estaAtacando = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaParaAtacar);
    }
}
