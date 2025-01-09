using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPlanner : MonoBehaviour
{
    public Queue<BuildMaterial> GetAllNeededMaterials(BuildingType buildingType)
    {
        // Use dynamic resources
        switch (buildingType)
        {
            case BuildingType.WoodCutter:
                return new Queue<BuildMaterial>(
                    new BuildMaterial[]
                    {
                        BuildMaterial.BricksCube, BuildMaterial.WoodPlank, BuildMaterial.BricksCube, BuildMaterial.WoodPlank, BuildMaterial.WoodPlank
                    });
            default:
                throw new InvalidOperationException($"Wrong {nameof(buildingType)}: {buildingType}.");
        }
    }
}
