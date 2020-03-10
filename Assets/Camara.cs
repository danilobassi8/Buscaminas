using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float Alto, Ancho;

    void Start()
    {
        Alto = 2 * Camera.main.orthographicSize;
        Ancho = Alto * Camera.main.aspect;

		
    }

    void Update()
    {

        Alto = 2 * Camera.main.orthographicSize;
        Ancho = Alto * Camera.main.aspect;
    }
}
