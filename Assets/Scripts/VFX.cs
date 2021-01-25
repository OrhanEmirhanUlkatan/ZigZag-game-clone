using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public static VFX singleton;

    [Header("VFX Contols")]
    public bool enableRotation ;

    [Header("Collectable Variables")]
    public bool enableCollectionVFX ;
    public GameObject[] collectionVFX;
    public GameObject collectionParticleEffect;
    public string collecetableTag;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAmount;

    [Header("Death VFX")]
    public GameObject playerDeathParticleEffect;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        SetTags();
        FindObjects(collecetableTag);
    }
    private void SetTags()
    {
        collecetableTag = "Collectable";
    }

    private void FindObjects(string tag)
    {
        if (tag == collecetableTag) 
        {
            collectionVFX = GameObject.FindGameObjectsWithTag(collecetableTag);
        }
    }

    private void Update()
    {
        if (enableCollectionVFX && enableRotation)
        {
            Rotate();
        }
     
    }

    // VFX Area
    public void Rotate()
    {
        foreach (var item in collectionVFX)
        {
            if (item != null)
            {
                item.transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * rotationSpeed, rotationAmount),
                Mathf.PingPong(Time.time * rotationSpeed, rotationAmount),
                Mathf.PingPong(Time.time * rotationSpeed, rotationAmount));
            }
           
        }
        
       

    }

    public void InstantiateParticleEffect(Collider collider, float time)
    {
       GameObject temp = Instantiate(collectionParticleEffect, collider.transform.position, Quaternion.identity);
        Destroy(temp, time);
    }

    public void InstantiatePlayerDeathParticleEffect(Transform playerTransform, float time)
    {
        GameObject temp = Instantiate(playerDeathParticleEffect, playerTransform.position, Quaternion.identity);
        Destroy(temp, time);
    }
}
