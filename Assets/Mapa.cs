﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mapa : MonoBehaviour
{
    public GameObject prefabCubo;

    public int cantidadEnX, cantidadEnY;
    private float anchoCamara, altoCamara;

    public bool actualizar;
    public bool coloresRandoms;


    public GameObject[,] listaCubos;

    private float posicionInicialX, posicionInicialY, posx, posy;
    public Color colorCubos;

    void Start()
    {
        actualizar = true;
        listaCubos = new GameObject[cantidadEnX, cantidadEnY];

        posx = 0; posy = 0;

        this.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        // Debugiman();


        if (actualizar) // acá adentro voy a actualizar los cubos.
        {
            purgaHijos();

            actualizar = false;

            anchoCamara = GameObject.Find("Main Camera").gameObject.GetComponent<Camara>().Ancho;
            altoCamara = GameObject.Find("Main Camera").gameObject.GetComponent<Camara>().Alto;

            listaCubos = new GameObject[cantidadEnX, cantidadEnY];


            //calculo la posicion inicial de los bloques.
            posicionInicialX = -anchoCamara / 2 + (anchoCamara / cantidadEnX) / 2;
            posicionInicialY = altoCamara / 2 - (altoCamara / cantidadEnY) / 2;

            //rellena el arreglo listacubos con los cubos con su correspondiente tamaño
            for (int x = 0; x <= cantidadEnX - 1; x++)
            {
                for (int y = 0; y <= cantidadEnY - 1; y++)
                {
                    //calculo posicion en x,y del objeto cubo a instanciar.
                    posx = posicionInicialX + x * anchoCamara / cantidadEnX;
                    posy = posicionInicialY - y * altoCamara / cantidadEnY;


                    GameObject cubo = GameObject.Instantiate(prefabCubo, new Vector3(posx, posy, 0), Quaternion.identity) as GameObject; //

                    cubo.transform.parent = this.gameObject.transform;

                    cubo.GetComponent<cubo>().ancho = anchoCamara / cantidadEnX;
                    cubo.GetComponent<cubo>().alto = altoCamara / cantidadEnY;

                    cubo.GetComponent<cubo>().matrizX = x;
                    cubo.GetComponent<cubo>().matrizY = y;

                    cubo.transform.localScale = new Vector3(anchoCamara / cantidadEnX, altoCamara / cantidadEnY, 1);

                    if (coloresRandoms)
                    {
                        cubo.GetComponent<cubo>().color = generarColorRandom();
                    }
                    else
                    {
                        cubo.GetComponent<cubo>().color = colorCubos;
                    }


                    cubo.name = "[" + (x + 1) + ", " + (y + 1) + "]";
                    listaCubos[x, y] = cubo;


                }
            }


        }
    }



    private void purgaHijos()
    {
        if (this.gameObject.transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(this.gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void Debugiman()
    {
        Debug.Log("La longitud del arreglo es de: " + listaCubos.Length);

        int i = 0;
        foreach (GameObject cubito in listaCubos)
        {
            Debug.Log("Cubo " + i++);
        }
    }
    public Color generarColorRandom()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        Color c = new Color(r, g, b, Random.Range(0.7f, 1f));
        return c;
    }
}


