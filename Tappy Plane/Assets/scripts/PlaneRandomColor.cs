using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRandomColor : MonoBehaviour
{
    public string[] Animations;

    void Start ()
    {
        var index = Random.Range(0, Animations.Length);
        GetComponent<Animator>().Play(Animations[index]);
    }
}
