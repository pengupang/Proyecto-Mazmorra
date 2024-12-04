using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Transform cameraTransform;
    private Rigidbody rb;


    [SerializeField] private float velocidad;
    [SerializeField] private float sensibilidad = 100f;
    [SerializeField] private float limiteInferior = 20f; 
    [SerializeField] private float limiteSuperior = 30f; 
    [SerializeField] private float fuerzaSalto = 10f; 
    [SerializeField] private float tiempoRecargaAtaque = 1f; // Tiempo de recarga entre ataques

    private float tiempoSiguienteAtaque = 0f;

    private bool enSuelo; 
    AudioManager audioManager;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; 
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {

        float ejeX = Input.GetAxis("Horizontal");
        float ejeY = Input.GetAxis("Vertical");

        Vector3 vectorMovimiento = new Vector3(ejeX, 0, ejeY).normalized * Time.deltaTime * velocidad;

        transform.Translate(vectorMovimiento, Space.Self);
        
        float smoothX = Mathf.Lerp(animator.GetFloat("Xspeed"), ejeX, Time.deltaTime * 10f);  
        float smoothY = Mathf.Lerp(animator.GetFloat("Yspeed"), ejeY, Time.deltaTime * 10f);  

        animator.SetFloat("Xspeed", smoothX);
        animator.SetFloat("Yspeed", smoothY);


        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        float rotacionX = Mathf.Clamp(mouseY, limiteInferior, limiteSuperior);
        cameraTransform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            Saltar();
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoSiguienteAtaque)
        {
            Atacar();
            tiempoSiguienteAtaque = Time.time + tiempoRecargaAtaque; // Reinicia el contador de recarga
        }
    }


    private void Saltar()
        {

            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            animator.SetTrigger("saltar");
            audioManager.PlayEfectos(audioManager.salto);
        
        }

    private void Atacar()
    {
        animator.SetTrigger("atacar");
        

        
    }


    private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("suelos"))
                {
                    enSuelo = true;
                    
                }
        }
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("suelos"))
                {
                    enSuelo = true;
                    audioManager.PlayEfectos(audioManager.aterrizar);
                }
        }

    private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("suelos"))
            {
                enSuelo = false;
            }
        }

}
