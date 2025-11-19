using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    private float movimientoX;
    public float velocidad = 2f;
    private Rigidbody2D rb2d;
    private Vector3 puntoInicio;

    [Header("--- Vidas ---")]
    public int vidasMaximas = 2;
    private int vidasActuales;

    [Header("--- Salto ---")]
    public float fuerzaSalto = 5f;

    [Header("--- CompruebaSuelo ---")]
    public LayerMask layerSuelo;
    public Transform compruebaSuelo;
    public float radioCompruebaSuelo = 0.2f;
    private bool estaEnSuelo;

    [Header("--- Efectos ---")]
    public AudioSource audioSource;
    public AudioClip clipMoneda;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        puntoInicio = transform.position;
        vidasActuales = vidasMaximas;
    }

    void Update()
    {
        estaEnSuelo = Physics2D.OverlapCircle(
            compruebaSuelo.position,
            radioCompruebaSuelo,
            layerSuelo
        );
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(
            movimientoX * velocidad,
            rb2d.linearVelocity.y
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Moneda: suma puntos y destruye
        if (collision.CompareTag("Moneda"))
        {
            FindAnyObjectByType<GameManager>()?.sumaPuntos();
            if (audioSource && clipMoneda) audioSource.PlayOneShot(clipMoneda);
            Destroy(collision.gameObject);
            return;
        }

        // SueloMuerte: solo vuelve al punto de inicio (sin perder vida)
        if (collision.CompareTag("SueloMuerte"))
        {
            rb2d.linearVelocity = Vector2.zero;
            transform.position = puntoInicio;
            return;
        }

        // Enemigo: resta vida, si quedan vuelve al inicio, si no, carga escena 3
        if (collision.CompareTag("Enemigo"))
        {
            vidasActuales--;

            if (vidasActuales > 0)
            {
                rb2d.linearVelocity = Vector2.zero;
                transform.position = puntoInicio;
            }
            else
            {
                SceneManager.LoadScene(3);
            }

            return;
        }

        // Castillo (victoria) -> lo dejamos igual que lo tenías
        if (collision.CompareTag("Castillo"))
        {
            var gm = FindAnyObjectByType<GameManager>();
            if (gm != null && gm.TodasMonedasRecogidas)
            {
                Destroy(collision.gameObject);

                int next = SceneManager.GetActiveScene().buildIndex + 1;
            }

        }
    }

}