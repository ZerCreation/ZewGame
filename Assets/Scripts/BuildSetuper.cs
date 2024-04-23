using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class BuildSetuper : MonoBehaviour
{
    public BuildSteps CurrentBuildStep 
    { 
        get { return _currentBuildStep; }
        private set 
        {
            _currentBuildStep = value;
            Debug.Log($"Current build step: {value}");
        }
    }

    private BuildSteps _currentBuildStep { get; set; }
    private Vector3 _buildPosition;
    private GameObject _bringingMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _buildPosition = transform.position;
        CurrentBuildStep = BuildSteps.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBuildStep == BuildSteps.GoindToBuild)
        {
            transform.position = Vector3.MoveTowards(transform.position, _buildPosition, Time.deltaTime * 5);
            if (transform.position == _buildPosition)
            {
                CurrentBuildStep = BuildSteps.Planning;
            }
        }

        if (CurrentBuildStep == BuildSteps.Planning)
        {
            //await Task.Delay(TimeSpan.FromSeconds(1));
            CurrentBuildStep = BuildSteps.TakingMaterial;
        }

        if (CurrentBuildStep == BuildSteps.TakingMaterial)
        {
            _bringingMaterial = GameObject.Find("Wood Plank");
            //_targetPosition = _bringingMaterial.transform.position;
            Vector3 bringingMaterialPosition = _bringingMaterial.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, bringingMaterialPosition, Time.deltaTime * 5);
            if (transform.position == bringingMaterialPosition)
            {
                CurrentBuildStep = BuildSteps.BringingMaterial;
            }
        }

        if (CurrentBuildStep == BuildSteps.BringingMaterial)
        {
            transform.position = Vector3.MoveTowards(transform.position, _buildPosition, Time.deltaTime * 5);
            _bringingMaterial.transform.position = transform.position;
        }
    }

    public void StartBuild(Vector3 buildPosition)
    {
        _buildPosition = buildPosition;
        _buildPosition.z = 5f;
        CurrentBuildStep = BuildSteps.GoindToBuild;
    }

    public async Task Organize()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));

        GatherResources();
    }

    private void GatherResources()
    {
        GameObject woodPlank = GameObject.Find("Wood Plank");
        GetComponent<Move>().MoveFinished.AddListener(() =>
        {
            Debug.Log("Take wood");
            GetComponent<Move>().MoveFinished.RemoveAllListeners();
        });
        GetComponent<Move>().MoveToTarget(woodPlank.transform.position);
    }

    public enum BuildSteps
    {
        Waiting,
        GoindToBuild,
        Planning,
        GroundPreparing,
        TakingMaterial,
        BringingMaterial,
        Building,
        Finished
    }
}
