﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Zombie : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    Vector2 TargetPosition;
    Player TargetPlayer;

    [SerializeField]
    float Speed = 1f;

    [SerializeField]
    float AttackDistance = 2f;

    [SerializeField]
    float AttackDamage = 1f;

	void Start ()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(ChangeTargetPositionCoroutine());
	}

    IEnumerator ChangeTargetPositionCoroutine()
    {
        while(true)
        {
            TargetPosition = (Vector2)transform.position + Random.insideUnitCircle * 10f;
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

	void Update ()
    {
        UpdateMovement();
        UpdateAttack();
    }

    void UpdateMovement()
    {
        var targetSpeed = Speed;

        if (TargetPlayer != null)
        {
            TargetPosition = TargetPlayer.transform.position;
            targetSpeed *= 2f;
        }

        var direction = (Vector3)TargetPosition - transform.position;
        var targetVelocity = direction.normalized * targetSpeed;

        Rigidbody.velocity = Vector3.Lerp(
            Rigidbody.velocity,
            targetVelocity,
            Time.deltaTime / 2f);

        transform.right = (Vector2)direction;
    }

    void UpdateAttack()
    {
        if (TargetPlayer == null)
            return;

        var distance = (TargetPlayer.transform.position - transform.position).magnitude;

        if (distance > AttackDistance)
            return;

        TargetPlayer.GetComponent<Entity>().Health -= AttackDamage * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            TargetPlayer = player;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            TargetPlayer = null;
    }
}
