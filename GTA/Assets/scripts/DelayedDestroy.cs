using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float Time = 5f;

	void Start ()
    {
        Destroy(gameObject, Time);
	}
}
