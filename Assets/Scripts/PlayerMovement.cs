using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] float velocidad;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float ejeX = Input.GetAxis("Horizontal");
        float ejeY = Input.GetAxis("Vertical");
        // Al crear un Vector3() con .normalized lo que logramos es que el personaje no se mueva
        // más rápido al usar los ejes diagonales.
        var vectorMovimiento = new Vector3(ejeX, 0, ejeY).normalized * Time.deltaTime;
        transform.Translate(vectorMovimiento * velocidad);

        animator.SetFloat("x", ejeX);
        animator.SetFloat("y", ejeY);

        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("saltar");
        }
    }
}
