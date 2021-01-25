using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }




    private Rigidbody rb;
    public float moveSpeed;

    private enum CurrentDirection
    {
        up, left
    }

    private CurrentDirection currentDirection;

    public bool isPlayerDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentDirection = CurrentDirection.left;
        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
        {
            ShootRay();
            if (Input.GetMouseButtonDown(0))
            {
                StopPlayer();
                ChangeDirection();
            }
        }
        else
        {
            return;
        }
        
    }

    private void ShootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit))
        {
            MovePlayer();
        }
        else
        {
            StopPlayer();
            isPlayerDead = true;
            VFX.singleton.InstantiatePlayerDeathParticleEffect(transform, 2);
            Destroy(gameObject);
        }
    }

    private void ChangeDirection()
    {
        MovePlayer();
        if (currentDirection==CurrentDirection.up)
        {
            currentDirection = CurrentDirection.left;
        }
        else if (currentDirection==CurrentDirection.left)
        {
            currentDirection = CurrentDirection.up;
        }
    }

    private void MovePlayer()
    {
        if (currentDirection ==CurrentDirection.up)
        {
            rb.AddForce((Vector3.forward).normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (currentDirection ==CurrentDirection.left)
        {
            rb.AddForce((Vector3.right).normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void StopPlayer()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            VFX.singleton.InstantiateParticleEffect(other,5f);
            Destroy(other.gameObject);
        }
    }
}
