using System.Collections.Generic;
using UnityEngine;

public class BuildLocalizator : MonoBehaviour
{
    public GameObject ConstructionSite;
    public float Speed = 5f;

    private GameObject _freeHuman;
    private Queue<IConstruction> _buildingQueue = new Queue<IConstruction>();

    // Start is called before the first frame update
    void Start()
    {
        _freeHuman = GameObject.Find("Human");
        //_moveToConstructionTargetPosition = _freeHuman.transform.position;
        //_targetPosition = _freeHuman.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("New build localization selected.");
            MarkConstructionPlace();

            Vector3 moveToConstructionTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newConstruction = new WoodCutterHouse(BuildingType.WoodCutter, moveToConstructionTargetPosition);
            _buildingQueue.Enqueue(newConstruction);

            Debug.Log(newConstruction.Progress);
            ConstructionSite.GetComponent<ConstructionProgress>().Construction = newConstruction;

            BuildDispositor freeHumanBuildDispositor = _freeHuman.GetComponent<BuildDispositor>();
            if (freeHumanBuildDispositor.CurrentBuildStep == BuildDispositor.BuildSteps.Resting)
            {
                freeHumanBuildDispositor.Initialize(newConstruction);
            }
        }
    }

    private void MarkConstructionPlace()
    {
        var targetPosition = Input.mousePosition;
        targetPosition.z = 1.0f;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(targetPosition);

        // TODO: Assign implementation of IConstruction to the ConstructionSite GameObject

        ConstructionSite = Instantiate(ConstructionSite, objectPos, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
}
