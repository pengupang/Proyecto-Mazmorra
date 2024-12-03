
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    [SerializeField]private PlayerDamage VidaPlayer;
    [SerializeField]private Image vidaTotal;
    [SerializeField]private Image vidaTotalActual;
    private float vidaMaxima;


    private void Start()
    {
        vidaMaxima=VidaPlayer.VidaActual;
        vidaTotal.fillAmount = 1f;
    }

    private void Update()
    {
        vidaTotalActual.fillAmount = VidaPlayer.VidaActual / vidaMaxima;
    }
}