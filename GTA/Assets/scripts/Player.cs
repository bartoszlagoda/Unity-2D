using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Entity))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float WalkingSpeed = 2f;

    [SerializeField]
    float RunningSpeed = 3f;

    Rigidbody2D Rigidbody;
    Crosshair Crosshair;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Crosshair = FindObjectOfType<Crosshair>();
    }

    void Start ()
    {
        GetComponent<Entity>().OnKilled += () => Application.Quit();
	}

	void Update ()
    {
        UpdateMovement();
        UpdateRotation();
    }

    void UpdateMovement()
    {
        var WalkingDirection = Vector3.zero;

        WalkingDirection += Vector3.up * Input.GetAxis("Vertical");
        WalkingDirection += Vector3.right * Input.GetAxis("Horizontal");

        WalkingDirection = WalkingDirection.normalized;
        WalkingDirection *= Input.GetKey(KeyCode.LeftShift) ? RunningSpeed : WalkingSpeed;

        Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, WalkingDirection, Time.deltaTime * 4f);
    }

    void UpdateRotation()
    {
        var delta = Crosshair.transform.position - transform.position;
        var targetRotation = (Vector2)delta;
        transform.right = Vector3.Lerp(transform.right, targetRotation, Time.deltaTime * 10f);
    }
}
