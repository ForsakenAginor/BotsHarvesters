using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsMarkedForHarvest; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ResourceStorage>(out ResourceStorage storage))
        { 
            Destroy(transform.gameObject);
            storage.AddResource();
        }
    }
}
