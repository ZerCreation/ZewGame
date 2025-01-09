using System;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterHouse : IConstruction
{
	public BuildingType Type { get; set; }
    public Vector3 Localization { get; set; }
	public double Progress { get; set; }
	public DateTime EstimatedFinish { get; set; }
	public Queue<BuildMaterial> BuildingQueue { get; private set; }

	// TODO: Make objects with BuildMaterials
	private readonly List<string> _constructionLevels = new List<string> { "Fundaments", "Floor", "Walls", "Roof", "Interior" };

	public WoodCutterHouse(BuildingType buildingType, Vector3 localization)
	{
		Type = buildingType;
		Localization = localization;

		BuildingQueue = new Queue<BuildMaterial>(
			new BuildMaterial[]
			{
				BuildMaterial.BricksCube, BuildMaterial.WoodPlank, BuildMaterial.BricksCube, BuildMaterial.WoodPlank, BuildMaterial.WoodPlank
			});
	}

	public void AssembleMaterial()
	{
		Progress += (((double)1 / _constructionLevels.Count)) * 100;
		BuildingQueue.Dequeue();
	}
}
