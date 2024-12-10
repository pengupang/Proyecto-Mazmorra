using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameoverScreen : MonoBehaviour
{
    public TMP_Text points;
    private void Awake()
{
    gameObject.SetActive(false); // Asegúrate de que empieza desactivado
}
    public void Setup(int score){
        Debug.Log($"Activando pantalla de Game Over con puntuación: {score}");
        gameObject.SetActive(true);
        points.text = score.ToString() + " Puntos";
    }

    public void RestartBtn (){
        SceneManager.LoadScene("SampleScene");
    }
}
