using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n0scope_script : MonoBehaviour
{
    public bool destroy;
    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    void Update()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
