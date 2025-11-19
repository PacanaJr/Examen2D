using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- UI ---")]
    public TMP_Text textoPuntos;

    [Header("--- Monedas ---")]
    public Transform grupoMonedas;

    private int puntos;
    private int totalMonedas;

    public bool TodasFrutasRecogidas => puntos >= totalMonedas;

    void Start()
    {
        if (grupoMonedas == null)
        {
            GameObject go = GameObject.Find("Moneda");
            if (go != null) grupoMonedas = go.transform;
        }

        totalMonedas = (grupoMonedas != null) ? grupoMonedas.childCount : 0;
        puntos = 0;
        ActualizaUI();
    }

    public int Puntos => puntos;  

    public void sumaPuntos()
    {
        
        puntos += 1;
        ActualizaUI();
    }

    private void ActualizaUI()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = puntos.ToString();
        }
    }
}

