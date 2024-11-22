using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    //para  cambiar el tamaño del radio de deteccion
    [SerializeField] private float radioDeteccion;
    //para mover el bicho al detectar
    [SerializeField]private float velocidad;
    //para que el bicho persiga al player
    [SerializeField] private Transform posicionJugador;
    //layer asignado al jugador
    [SerializeField] private LayerMask layerJugador;
    //booleano se pondrá True cada vez que Checksphere detecte al jugador
    private bool detectado;


    void Update()
    {
        detectado = Physics.CheckSphere(transform.position, radioDeteccion,layerJugador);
        //Si no se ha detectado al player dentro de la esfera termina el codigo
        if (!detectado) return;
        // si lo detecta sigue leyendo lo de arriba
        transform.LookAt(posicionJugador);//para que mire hacia el jugador

        var PositionPlayer = posicionJugador.position;
        var VectorPosicionPlayer = new Vector3 (posicionJugador.position.x,posicionJugador.position.y,posicionJugador.position.z);
        transform.position =Vector3.MoveTowards(transform.position,VectorPosicionPlayer,velocidad*Time.deltaTime);

        
    }
}
