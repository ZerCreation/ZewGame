using System;
using UnityEngine;

public interface IConstruction
{
    BuildingType Type { get; set; }
    Vector3 Localization { get; set; }
    double Progress { get; set; }
    DateTime EstimatedFinish { get; set; }

    void AssembleMaterial();
}
