using System;
using System.Collections;
using System.Collections.Generic;
using Helpers.Enums;
using Models.Managers;
using Objects;
using Objects.Poolings;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : Collectable
{
    public float forceRate = 10000f;
    
    private bool isInsidePlayer = false;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private PoolItem item;
    public void SetInside(bool inside)
    {
        isInsidePlayer = inside;
    }

    public void DropInsideBall(GameState state)
    {
        if (isInsidePlayer && state.HasFlag(GameState.Dropping))
        {
            Debug.Log("Ball is ready to drop");
            //Throw();
        }

    }

    public void Explode()
    {
        GameObject particle = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.Play();
        PoolManager.instance.ResetPoolItem(item);
    }

    public void Throw()
    {
        Vector3 aimVector = transform.position + new Vector3(transform.position.x + 10f,
            transform.position.y + 10f, transform.position.z);
        
        Vector3 forceDirection = aimVector.normalized;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.AddForce(forceDirection*forceRate);
        Debug.Log("forced");
        isInsidePlayer = false;
    }
    
    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += DropInsideBall;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= DropInsideBall;
    }
}