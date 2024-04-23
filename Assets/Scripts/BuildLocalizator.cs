using System;
using System.Threading.Tasks;
using UnityEngine;

public class BuildLocalizator : MonoBehaviour
{
    public GameObject ConstructionSite;
    public float Speed = 5f;

    private GameObject _freeHuman;
    private bool _buildStarted = false;
    //private Vector3 _moveToConstructionTargetPosition;
    //private Vector3 _targetPosition;

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
        Vector3 moveToConstructionTargetPosition = Vector3.zero;
        //Move freeHumanMove = _freeHuman.GetComponent<Move>();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            BuildConstructionPlace();

            moveToConstructionTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _freeHuman.GetComponent<BuildSetuper>().StartBuild(moveToConstructionTargetPosition);
            return;

            //freeHumanMove.MoveFinished.AddListener(async () =>
            //{
            //    Debug.Log("Human is in place");
            //    freeHumanMove.MoveFinished.RemoveAllListeners();
            //    await _freeHuman.GetComponent<BuildSetuper>().Organize();
            //});
            //freeHumanMove.MoveToTarget(moveToConstructionTargetPosition);
            
            //Debug.Log("Going");
            //EventManager.Instance.TriggerMoveEvent(moveToConstructionTargetPosition);
            _buildStarted = true;
        }

        //if (!_buildStarted) return;

        //if (!freeHumanMove.IsMoving)
        //{
        //    //Debug.Log("Human is in place");
        //    //GameObject woodPlank = GameObject.Find("Wood Plank");
        //    //freeHumanMove.MoveToTarget(woodPlank.transform.position);
        //    //GameObject.FindGameObjectsWithTag("material-wood");
        //}

        // TODO: Enqueue moves
        // first target
        // freeHumanMove.MoveToTarget(targets[0]);
        // action after reaching first target
        //             freeHumanMove.MoveFinished.AddListener(() =>
                        //{
                            // action[0](); // take next action
                            // freeHumanMove.MoveToTarget(targets[1]); // take next target
                        //});
        // second target
        // ...

        //_freeHuman.transform.position = Vector3.MoveTowards(_freeHuman.transform.position, _moveToConstructionTargetPosition, Speed * Time.deltaTime);
        //Debug.Log(_freeHuman.transform.position);
    }

    private void BuildConstructionPlace()
    {
        var targetPosition = Input.mousePosition;
        targetPosition.z = 1.0f;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(targetPosition);
        
        Instantiate(ConstructionSite, objectPos, Quaternion.identity);
    }
}
