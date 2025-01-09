using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildDispositor : MonoBehaviour
{
    public GameObject BuildPlanner;
    public BuildSteps CurrentBuildStep 
    { 
        get { return _currentBuildStep; }
        private set 
        {
            _currentBuildStep = value;
            Debug.Log($"Current build step: {value}");
        }
    }

    //public int ConstructionProgress;

    private BuildSteps _currentBuildStep;
    private IConstruction _currentBuilding;
    private Vector3 _buildPosition;
    private GameObject _processingMaterial;
    private Queue<BuildMaterial> _currentMaterialsQueue = new Queue<BuildMaterial>();
    private ResourcesPlanner _resourcesPlanner;

    // Start is called before the first frame update
    void Start()
    {
        _resourcesPlanner = BuildPlanner.GetComponent<ResourcesPlanner>();

        CurrentBuildStep = BuildSteps.Resting;
    }

    // Update is called once per frame
    void Update()
    {
        GoToStart();

        if (CurrentBuildStep != BuildSteps.Resting)
        {
            PlanBuild();
            TakeMaterial();
            BringMaterial();
            Assemble();
            Inspect(); 
        }
    }

    private void GoToStart()
    {
        if (CurrentBuildStep == BuildSteps.GoingToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, _buildPosition, Time.deltaTime * 5);
            if (transform.position == _buildPosition)
            {
                CurrentBuildStep = BuildSteps.Planning;
            }
        }
    }

    private void PlanBuild()
    {
        if (CurrentBuildStep == BuildSteps.Planning)
        {
            if (!_currentMaterialsQueue.Any())
            {
                // TODO: Get it from building
                _currentMaterialsQueue = _resourcesPlanner.GetAllNeededMaterials(BuildingType.WoodCutter);
            }
            string materialName = _currentMaterialsQueue.Dequeue().ToString();
            _processingMaterial = GameObject.Find(materialName);
            Debug.Log($"Planning with material: {materialName}.");
            //await Task.Delay(TimeSpan.FromSeconds(1));
            CurrentBuildStep = BuildSteps.TakingMaterial;
        }
    }

    private void TakeMaterial()
    {
        if (CurrentBuildStep == BuildSteps.TakingMaterial && _processingMaterial != null)
        {
            Vector3 bringingMaterialPosition = _processingMaterial.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, bringingMaterialPosition, Time.deltaTime * 5);
            if (transform.position == bringingMaterialPosition)
            {
                CurrentBuildStep = BuildSteps.BringingMaterial;
            }
        }
    }

    private void BringMaterial()
    {
        if (CurrentBuildStep == BuildSteps.BringingMaterial)
        {
            transform.position = Vector3.MoveTowards(transform.position, _buildPosition, Time.deltaTime * 5);
            _processingMaterial.transform.position = transform.position;
            if (_processingMaterial.transform.position == _buildPosition)
            {
                CurrentBuildStep = BuildSteps.Assembling;
            }
        }
    }

    private void Assemble()
    {
        if (CurrentBuildStep == BuildSteps.Assembling)
        {
            // Start animation
            _currentBuilding.AssembleMaterial();
            // Takes some time

            Destroy(_processingMaterial);
            CurrentBuildStep = BuildSteps.Inspecting;
        }
    }

    private void Inspect()
    {
        if (CurrentBuildStep == BuildSteps.Inspecting)
        {
            if (_currentMaterialsQueue.Any())
            {
                CurrentBuildStep = BuildSteps.Planning;
                return;
            }
            Debug.Log("Building is finished.");

            CurrentBuildStep = BuildSteps.Resting;
        }
    }

    public void Initialize(IConstruction building)
    {
        _currentBuilding = building;

        _buildPosition = building.Localization;
        _buildPosition.z = 5f;
        CurrentBuildStep = BuildSteps.GoingToStart;
    }

    public enum BuildSteps
    {
        Resting,
        GoingToStart,
        Planning,
        GroundPreparing,
        TakingMaterial,
        BringingMaterial,
        Assembling,
        Inspecting
    }
}
