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

    public void Revelar(int x, int y)
    {
        /*NOTA: Este metodo se sacó afuera porque cuando un casillero está vacio, vuelve a llamar 
        a este metodo con todos lo de al rededor. 
        */
        ControladorJuego.GetComponent<controlador_script>().Revelar(matrizX, matrizY);
        /*
        bombasCerca = CalculaBombasCerca(matrizX, matrizY);
        calculaNumerosEnPantalla();
        if (bombasCerca != 0)
        {
            muestraInterior = true;
        }
        else
        {
            
            CambiaColor(this.gameObject, new Color(0.7372549f, 0.6980392f, 0.6980392f, 1f));  //HEXA BCB2B2;
            RevelaBloquesCercanos(matrizX, matrizY);
        }
        */


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





    public void RevelaBloquesCercanos(int x, int y)
    {
        int cantx = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnX;
        int canty = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnY;

        if (x != 0 && y != 0 && x != cantx - 1 && y != canty - 1)
        {
            Debug.Log("ACA PASE");
            pintadoBombasCercanas(x - 1, y - 1, x + 1, y + 1);
        }
        else if (x == 0)
        {
            if (y != 0 && y != canty - 1) // caso donde x=0 pero y no.
            {
                pintadoBombasCercanas(x, y - 1, x + 1, y + 1);
            }
            else if (y == 0)
            {
                pintadoBombasCercanas(x, y, x + 1, y + 1);
            }
            else if (y == canty - 1)
            {
                pintadoBombasCercanas(x, y - 1, x + 1, y);
            }
        }
        else if (y == 0)
        {
            if (x != cantx - 1)
            {
                pintadoBombasCercanas(x - 1, y, x + 1, y + 1);
            }
            else if (x == cantx - 1)
            {
                pintadoBombasCercanas(x - 1, y, x, y + 1);
            }
        }
        else if (x == cantx - 1)
        {
            if (y != canty - 1)
            {
                pintadoBombasCercanas(x - 1, y - 1, x, y + 1);
            }
            else if (y == canty - 1)
            {
                pintadoBombasCercanas(x - 1, y - 1, x, y);
            }
        }
        else if (y == canty - 1)
        {
            if (x != cantx)
            {
                pintadoBombasCercanas(x - 1, y - 1, x + 1, y);
            }
        }
    }

    public void pintadoBombasCercanas(int desdex, int desdey, int hastax, int hastay)
    {
        GameObject[,] listaCubos = GameObject.Find("Mapa").GetComponent<Mapa>().listaCubos;

        for (int i = desdex; i <= hastax; i++)
        {
            for (int j = desdey; j <= hastay; j++)
            {
                if (i != matrizX && j != matrizY) //para evitar recursividad infinita.
                {
                    Debug.Log("REVELAR --->" + i.ToString() + "," + j);
                    Revelar(i, j);
                }

            }
        }
    }

    public void cambiarColor(Color color)
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }


}
