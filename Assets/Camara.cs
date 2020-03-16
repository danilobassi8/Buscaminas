using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float Alto, Ancho, centro;

    void Start()
    {
        Alto = this.gameObject.GetComponent<Camera>().orthographicSize * 2;
        Ancho = Alto * this.gameObject.GetComponent<Camera>().aspect;
    }

    void Update()
    {
        Alto = this.gameObject.GetComponent<Camera>().orthographicSize * 2;
        Ancho = Alto * this.gameObject.GetComponent<Camera>().aspect;

    }
}
