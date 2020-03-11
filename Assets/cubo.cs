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
    private int bombasCerca;
    private bool muestraInterior;   //bandera que al activarse, permite que se muestre el nro de bombas cerca.
    private GUIStyle guiStyle = new GUIStyle(); //Creo el estilo para los numeros de los cubos.
    private float tamañoLetra;

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


        muestraInterior = false;
    }

    void Update()
    {

    }
    void OnGUI()
    {
        if (muestraInterior)
        {
            string text = bombasCerca.ToString();
            var position = Camera.main.WorldToScreenPoint(gameObject.GetComponent<Renderer>().bounds.center);
            var textSize = GUI.skin.label.CalcSize(new GUIContent(text));

            //Debug.Log("tamaño letra " +tamañoLetra.ToString()[0].ToString());
            guiStyle.fontSize = (int)Mathf.Floor(tamañoLetra);
            // guiStyle.fontSize = 25;

            position.y = position.y + guiStyle.fontSize / 1.8f;
            position.x = position.x - guiStyle.fontSize / 2.5f;


            GUI.Label(new Rect(position.x, Screen.height - position.y, textSize.x, textSize.y), text, guiStyle);
        }

    }

    //Cuando se aprieta el click.
    void OnMouseDown()
    {

        if (tieneBomba)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            Revelar(matrizX, matrizY);
        }
    }

    void Revelar(int x, int y)
    {
        /*NOTA: Este metodo se sacó afuera porque cuando un casillero está vacio, vuelve a llamar 
        a este metodo con todos lo de al rededor. 
        */

        bombasCerca = CalculaBombasCerca(matrizX, matrizY);
        calculaNumerosEnPantalla();
        if (bombasCerca != 0)
            muestraInterior = true;
        else
        {
            Debug.Log("UN CERO");
            RevelaBloquesCercanos(matrizX, matrizY);

            CambiaColor(this.gameObject, new Color(0.7372549f, 0.6980392f, 0.6980392f, 1f));  //HEXA BCB2B2;
        }
    }

    void calculaNumerosEnPantalla() //calcula tamaño y color de los nros en pantalla.
    {


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

    int CalculaBombasCerca(int x, int y)
    {
        //NOTA: Se fija sobre si mismo tambien, pero no pasa nada porque si tuviese bomba, no hubiese llegado acá.


        int cantx = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnX;
        int canty = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnY;
        int bombasCerca = -1;



        if (x != 0 && y != 0 && x != cantx - 1 && y != canty - 1)
        {
            bombasCerca = recorridoDeBombasCercanas(x - 1, y - 1, x + 1, y + 1);
        }
        else if (x == 0)
        {
            if (y != 0 && y != canty - 1) // caso donde x=0 pero y no.
            {
                bombasCerca = recorridoDeBombasCercanas(x, y - 1, x + 1, y + 1);
            }
            else if (y == 0)
            {
                bombasCerca = recorridoDeBombasCercanas(x, y, x + 1, y + 1);
            }
            else if (y == canty - 1)
            {
                bombasCerca = recorridoDeBombasCercanas(x, y - 1, x + 1, y);
            }
        }
        else if (y == 0)
        {
            if (x != cantx - 1)
            {
                bombasCerca = recorridoDeBombasCercanas(x - 1, y, x + 1, y + 1);
            }
            else if (x == cantx - 1)
            {
                bombasCerca = recorridoDeBombasCercanas(x - 1, y, x, y + 1);
            }
        }
        else if (x == cantx - 1)
        {
            if (y != canty - 1)
            {
                bombasCerca = recorridoDeBombasCercanas(x - 1, y - 1, x, y + 1);
            }
            else if (y == canty - 1)
            {
                bombasCerca = recorridoDeBombasCercanas(x - 1, y - 1, x, y);
            }
        }
        else if (y == canty - 1)
        {
            if (x != cantx)
            {
                bombasCerca = recorridoDeBombasCercanas(x - 1, y - 1, x + 1, y);
            }
        }

        //devuelve -1 si no encontro. (debug.)
        return bombasCerca;
    }

    int recorridoDeBombasCercanas(int desdex, int desdey, int hastax, int hastay)
    {
        /*  Este metodo recorre desde que x y desde que y se empieza
        hasta que x e y se termina.
        Pensarlo en forma matricial.
        Por ejemplo, si clickeo en el [2,2] mando por parametros (1,1,3,3).
        Excepto que sea uno de los bordes, por el cual justamente se programó este metodo 
        que brinda la capacidad de cambio sin tener que hacer siempre los mismos loops.
        */

        GameObject[,] listaCubos = GameObject.Find("Mapa").GetComponent<Mapa>().listaCubos;
        int bombasCerca = 0;

        for (int i = desdex; i <= hastax; i++)
        {
            for (int j = desdey; j <= hastay; j++)
            {
                if (listaCubos[i, j].gameObject.GetComponent<cubo>().tieneBomba)
                {
                    bombasCerca++;
                }
            }
        }

        return bombasCerca;
    }

    void RevelaBloquesCercanos(int x, int y)
    {
        int cantx = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnX;
        int canty = GameObject.Find("Mapa").GetComponent<Mapa>().cantidadEnY;

        if (x != 0 && y != 0 && x != cantx - 1 && y != canty - 1)
        {
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

    void pintadoBombasCercanas(int desdex, int desdey, int hastax, int hastay)
    {
        GameObject[,] listaCubos = GameObject.Find("Mapa").GetComponent<Mapa>().listaCubos;

        for (int i = desdex; i <= hastax; i++)
        {
            for (int j = desdey; j <= hastay; j++)
            {
                Revelar(i, j);
                Debug.Log("REVELAR --->" + i.ToString() + "," + j);
            }
        }
    }

    void CambiaColor(GameObject cubo, Color color)
    {
        cubo.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
