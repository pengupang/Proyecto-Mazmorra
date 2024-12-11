using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameoverScreen : MonoBehaviour
{
    public TMP_Text points;  // Referencia al componente de texto para mostrar el puntaje
    public GameObject gameOverPanel;  // Panel de Game Over, si lo tienes

    private void Awake()
    {
        gameObject.SetActive(false);  // Asegúrate de que empieza desactivado
    }

    public void Setup(int score)
    {
        Debug.Log($"Activando pantalla de Game Over con puntuación: {score}");
        gameObject.SetActive(true);  // Activa el panel de Game Over
        points.text = score.ToString() + " Puntos";  // Muestra el puntaje

        // Si tienes un panel de Game Over, asegúrate de activarlo aquí también
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("SampleScene");  // Reinicia el nivel
    }
}
