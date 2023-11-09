using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private List<Harvester> _harvesters;
    private List<Resource> _harvestableResources;
    private int _resourceCount;

    public void AddResource()
    {
        _resourceCount++;
    }

    private void Awake()
    {
        _harvesters = FindObjectsByType<Harvester>(FindObjectsSortMode.None).ToList();        
    }

    private void Update()
    {
        FindResourcesForHarvest();

        foreach (Harvester harvester in _harvesters)
        {
            if (harvester.IsBusy == false && _harvestableResources.Count > 0)
            {
                harvester.SetTarget(_harvestableResources[0].transform);
                _harvestableResources[0].IsMarkedForHarvest = true;
                _harvestableResources.RemoveAt(0);
            }
        }
    }

    private void FindResourcesForHarvest()
    {
        _harvestableResources = FindObjectsByType<Resource>(FindObjectsSortMode.None).Where(resource => resource.IsMarkedForHarvest == false).ToList();
    }
}
