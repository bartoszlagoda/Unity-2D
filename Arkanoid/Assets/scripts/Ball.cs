using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    float InitialSpeed = 5f;

	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * InitialSpeed;
	}
}
