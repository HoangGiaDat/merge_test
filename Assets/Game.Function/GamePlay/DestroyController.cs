using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    public ParticleSystem partical;

    private void OnEnable()
    {
        partical.Play();
        Invoke("DestroyAfterU", 2f);
    }

    private void DestroyAfterU()
    {
        SmartPool.Instance.Despawn(this.gameObject);
    }
}
