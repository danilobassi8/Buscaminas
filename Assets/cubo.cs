using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour
{

    public float alto, ancho;
    public int matrizX, matrizY;
    public Color color;

    public float ProbabilidadDeBomba;
    public bool tieneBomba;

    private bool firstTime = true;


    void Start()
    {
        //cambia el Alfa de los colores.
        color.a = 1f;

        //le pongo color al borde y a el cubo en si.


        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;

        this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;

        //ajusto la escala del hijo a un 5% del padre.
        this.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.95f, 0.95f, 1);

        if (Random.Range(0f, 1f) <= ProbabilidadDeBomba)
        {
            tieneBomba = true;
        }
    }

    void Update()
    {

    }
    void OnGUI()
    {
        GUI.Label(new Rect(new Vector2(0, 0), new Vector2(100, 100)), 5.ToString());
    }
    void OnMouseDown()
    {

        if (tieneBomba)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            Debug.Log("La bomba: [" + matrizX + "," + matrizY + "] tiene: " + CalculaBombasCerca(matrizX, matrizY) + " bombas");
        }
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

}
