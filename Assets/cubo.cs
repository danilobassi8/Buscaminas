using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour
{

    public float alto, ancho;
    public int matrizX, matrizY;
    public Color color;

    //relacion de aspecto con el borde
    [Range(0.0f, 1f)]
    public float relacionBorde;

    public float ProbabilidadDeBomba;
    public bool tieneBomba;

    private bool firstTime = true;

    public bool muestraInterior;   //bandera que al activarse, permite que se muestre el nro de bombas cerca.
    private GUIStyle guiStyle = new GUIStyle(); //Creo el estilo para los numeros de los cubos.
    private float tamañoLetra;
    private GameObject ControladorJuego;
    public bool clicked;

    private int bombasCerca;


    void Start()
    {
        //cambia el Alfa de los colores.
        color.a = 1f;

        //le pongo color al borde y a el cubo en si.


        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;

        // this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;

        //ajusto la escala del hijo a un 5% del padre.
        this.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(relacionBorde, relacionBorde, 1);

        if (Random.Range(0f, 1f) <= ProbabilidadDeBomba)
        {
            tieneBomba = true;
        }

        ControladorJuego = GameObject.Find("ControladorJuego");
        muestraInterior = false;
    }

    void Update()
    {

    }
    void OnGUI()
    {
        if (muestraInterior)
        {
            var texto = bombasCerca.ToString();
            var position = Camera.main.WorldToScreenPoint(gameObject.GetComponent<Renderer>().bounds.center);
            var textSize = GUI.skin.label.CalcSize(new GUIContent(texto));

            guiStyle.fontSize = (int)Mathf.Floor(tamañoLetra);

            position.y = position.y + guiStyle.fontSize / 1.8f;
            position.x = position.x - guiStyle.fontSize / 2.5f;


            GUI.Label(new Rect(position.x, Screen.height - position.y, textSize.x, textSize.y), texto, guiStyle);
        }

    }

    //Cuando se aprieta el click.
    void OnMouseDown()
    {
        if (clicked == false)
        {
            clicked = true;
            if (tieneBomba)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Revelar(matrizX, matrizY);
            }
        }
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) //click derecho
        {
            Debug.Log("Right click on this object:  " + matrizX + ", " + matrizY);
        }
        if (Input.GetMouseButtonDown(2)) //rueda del medio
        {
            Debug.Log("Middle click on this object:  " + matrizX + ", " + matrizY);
        }
    }

    public void Revelar(int x, int y)
    {
        ControladorJuego.GetComponent<controlador_script>().Revelar(matrizX, matrizY);

    }

    public void previaMuestraEnPantalla(int _bombasCerca) //calcula tamaño y color de los nros en pantalla.
    {
        this.bombasCerca = _bombasCerca;

        //tamaño.
        tamañoLetra = 1;
        if (ancho >= 0.3557872 && alto >= 0.3557872)
        {
            tamañoLetra = 11;
        }
        if (ancho >= 0.444734 && alto >= 0.444734)
        {
            tamañoLetra = 15;
        }
        if (ancho >= 0.889468 && alto >= 0.889468)
        {
            tamañoLetra = 25;
        }
        if (ancho >= 2.964893 && alto >= 2.964893)
        {
            tamañoLetra = 35;
        }
        if (ancho <= 0.3557872 || alto <= 0.3557872)
        {
            tamañoLetra = 10;
        }
        if (ancho <= 0.3067131 || alto <= 0.3067131)
        {
            tamañoLetra = 7;
        }

        //color.

    }

    public void cambiarColor(Color color)
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }


}
