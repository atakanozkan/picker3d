using Models.Managers;
using Objects;
using Objects.Poolings;
using UnityEngine;
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

    private void DropInsideBall(GameState state)
    {
        if (isInsidePlayer && state.HasFlag(GameState.Dropping))
        {
            Throw();
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
        Vector3 aimVector = transform.position + new Vector3(_rigidBody.position.x + 10f,
            _rigidBody.position.y + 10f, _rigidBody.position.z);
        
        Vector3 forceDirection = aimVector.normalized;
        _rigidBody.AddForce(forceDirection*forceRate);
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
