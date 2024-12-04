using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    public int danoArma;
    private Animator animator;
    //private AudioManager audioManager;
    private Collider collider; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        collider = GetComponent<Collider>(); 
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "armaImpacto")
        {
            if(animator != null)
            {
                animator.Play("danoEnemigo");
            }

            vida -= danoArma;
        }

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
            collider.enabled = false;

            // Desactivar el script para que no reciba más daño ni interacciones
            
        }
    }

    private void Muerte()
    {
        
        collider.enabled = false; 
    }
}
