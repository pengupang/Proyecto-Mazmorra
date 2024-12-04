
using UnityEngine;
using UnityEngine.Android;

public class AudioManager : MonoBehaviour
{
    [Header ("-------AudioSource--------")]
   [SerializeField] AudioSource fuenteMusica;
   [SerializeField] AudioSource fuenteEfectos;
   [SerializeField] AudioSource fuentePasos;
    

    
    [Header ("-------AudioClip--------")]
    // Aqui van los audioClip es decir un espacio para poner el audio y poder seleccionarlo por nombre
   public AudioClip fondo;
   [SerializeField]public AudioClip[] caminar;
   public AudioClip playerAtaque;
   public AudioClip coleccionable;

   public AudioClip vida;
   public AudioClip salto;
   public AudioClip aterrizar;
   public AudioClip golpePlayer;
   public AudioClip muertePlayer;
  
   public AudioClip respawn;
   [Header ("-------Enemigos--------")]
   public AudioClip EnemigoPasos;
   public AudioClip EnemigoMuerte;
   public AudioClip EnemigoDaño;


   
   private void Start(){
    //Esto es basicamente que el fuenteMusica tome el clip de fondo y se inicie la musica en cuanto 
    //se inicie el juego
    fuenteMusica.clip=fondo;
    fuenteMusica.Play();
   }
    // Play efectos es el que se llama y utiliza en cada cosa que haga ruidos del juego
    //por ejemplo: recoger algo,golpear, etc.
    public void PlayEfectos (AudioClip clip){
        fuenteEfectos.PlayOneShot(clip);
    }

    public void Pasos()
    {
        if (!fuentePasos.isPlaying){
        fuentePasos.clip = caminar[0];
        fuentePasos.loop = true;
        fuentePasos.Play();
    }
    }

    public void paraPasos(){
        if (fuentePasos.isPlaying)
        {
            fuentePasos.Stop();
        }
    }
    
    

  
   

}
