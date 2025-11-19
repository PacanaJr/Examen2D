using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- UI ---")]
    public TMP_Text textoPuntos;

    public TMP_Text textoSetas;

    [Header("--- Monedas ---")]
    public Transform grupoMonedas;

    [Header("--- Setas ---")]
    public Transform grupoSetas;

    private int puntos;
    private int puntosSetas;

    private int totalMonedas;
    private int totalSetas;

    public bool TodasMonedasRecogidas => puntos >= totalMonedas;
    public bool TodasSetasRecogidas => puntosSetas >= totalSetas;

    void Start()
    {
        if (grupoMonedas == null)
        {
            GameObject go = GameObject.Find("Moneda");
            if (go != null) grupoMonedas = go.transform;
        }

        if (grupoSetas == null)
        {
            GameObject go = GameObject.Find("Seta");
            if (go != null) grupoSetas = go.transform;
        }

        totalMonedas = (grupoMonedas != null) ? grupoMonedas.childCount : 0;
        puntos = 0;
        ActualizaUI();

        totalSetas = (grupoSetas != null) ? grupoSetas.childCount : 0;
        puntosSetas = 0;
        ActualizaUI();
    }

    public int Puntos => puntos;  

    public void sumaPuntos()
    {
        
        puntos += 1;
        ActualizaUI();
    }

    public void sumaSetas()
    {

        puntosSetas += 1;
        ActualizaUI();
    }

    private void ActualizaUI()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = puntos.ToString();
        }

        if (textoSetas != null)
        {
            textoSetas.text = puntosSetas.ToString();
        }
    }
}

