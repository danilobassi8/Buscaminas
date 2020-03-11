using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador_script : MonoBehaviour
{
    private GameObject Mapa;
    private int bombasCerca;


    // Use this for initialization
    void Start()
    {
        Mapa = GameObject.Find("Mapa");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Revelar(int x, int y)
    {
        GameObject cuboARevelar = GameObject.Find(Mapa.GetComponent<Mapa>().listaCubos[x, y].name);
        cuboARevelar.GetComponent<cubo>().clicked = true;


        bombasCerca = CalculaBombasCerca(x, y);

        if (bombasCerca != 0)
        {
            cuboARevelar.GetComponent<cubo>().previaMuestraEnPantalla(bombasCerca);
            cuboARevelar.GetComponent<cubo>().muestraInterior = true;
        }
        else
        {
            cuboARevelar.GetComponent<cubo>().cambiarColor(new Color(0.7372549f, 0.6980392f, 0.6980392f, 1f)); //HEXA BCB2B2

            RevelaBloquesCercanos(x, y);
        }



    }

    public void Revelar(GameObject cuboARevelar, int x, int y)
    {

        if (cuboARevelar != null)
        {
            Debug.Log("REVELANDO LOS CERCANOS AL OBJETO: " + x + ", " + y);
            Revelar(cuboARevelar.GetComponent<cubo>().matrizX, cuboARevelar.GetComponent<cubo>().matrizY);
        }
        else
        {
            Debug.Log("Se quiso revelar un cubo fuera de la matriz. Pero no pasa nada");
        }
    }

    public int CalculaBombasCerca(int x, int y)
    {
        //NOTA: Se fija sobre si mismo tambien, pero no pasa nada porque si tuviese bomba, no hubiese llegado acá.


        int cantx = Mapa.GetComponent<Mapa>().cantidadEnX;
        int canty = Mapa.GetComponent<Mapa>().cantidadEnY;
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

    public int recorridoDeBombasCercanas(int desdex, int desdey, int hastax, int hastay)
    {
        /*  Este metodo recorre desde que x y desde que y se empieza
        hasta que x e y se termina.
        Pensarlo en forma matricial.
        Por ejemplo, si clickeo en el [2,2] mando por parametros (1,1,3,3).
        Excepto que sea uno de los bordes, por el cual justamente se programó este metodo 
        que brinda la capacidad de cambio sin tener que hacer siempre los mismos loops.
        */

        GameObject[,] listaCubos = Mapa.GetComponent<Mapa>().listaCubos;
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

    public void RevelaBloquesCercanos(int x, int y)
    {
        GameObject g1 = new GameObject();
        GameObject g2 = new GameObject();
        GameObject g3 = new GameObject();
        GameObject g4 = new GameObject();
        GameObject g5 = new GameObject();
        GameObject g6 = new GameObject();
        GameObject g7 = new GameObject();
        GameObject g8 = new GameObject();

        GameObject MapaActual = GameObject.Find("Mapa");
        x++; y++;

        if (MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y - 1) + "]") != null)
        {
            g1 = MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y - 1) + "]").gameObject;
            if (g1.GetComponent<cubo>().clicked == false)
                Revelar(g1, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y) + "]") != null)
        {
            g2 = MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y) + "]").gameObject;
            if (g2.GetComponent<cubo>().clicked == false)
                Revelar(g2, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y + 1) + "]") != null)
        {
            g3 = MapaActual.gameObject.transform.Find("[" + (x - 1) + ", " + (y + 1) + "]").gameObject;
            if (g3.GetComponent<cubo>().clicked == false)
                Revelar(g3, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + x + ", " + (y + 1) + "]") != null)
        {
            g4 = MapaActual.gameObject.transform.Find("[" + x + ", " + (y + 1) + "]").gameObject;
            if (g4.GetComponent<cubo>().clicked == false)
                Revelar(g4, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + x + ", " + (y - 1) + "]") != null)
        {
            g5 = MapaActual.gameObject.transform.Find("[" + x + ", " + (y - 1) + "]").gameObject;
            if (g5.GetComponent<cubo>().clicked == false)
                Revelar(g5, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y - 1) + "]") != null)
        {
            g6 = MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y - 1) + "]").gameObject;
            if (g6.GetComponent<cubo>().clicked == false)
                Revelar(g6, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y) + "]") != null)
        {
            g7 = MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y) + "]").gameObject;
            if (g7.GetComponent<cubo>().clicked == false)
                Revelar(g7, x, y);
        }
        if (MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y + 1) + "]"))
        {
            g8 = MapaActual.gameObject.transform.Find("[" + (x + 1) + ", " + (y + 1) + "]").gameObject;
            if (g8.GetComponent<cubo>().clicked == false)
                Revelar(g8, x, y);
        }

        if (g1 == null)
        {
            Debug.Log("g1 es null");
        }
        if (g2 == null)
        {
            Debug.Log("g2 es null");
        }
        if (g3 == null)
        {
            Debug.Log("g3 es null");
        }
        if (g4 == null)
        {
            Debug.Log("g4 es null");
        }
        if (g5 == null)
        {
            Debug.Log("g5 es null");
        }
        if (g6 == null)
        {
            Debug.Log("g6 es null");
        }
        if (g7 == null)
        {
            Debug.Log("g7 es null");
        }
        if (g8 == null)
        {
            Debug.Log("g8 es null");
        }

    }



}
