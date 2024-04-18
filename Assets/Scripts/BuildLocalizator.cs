using UnityEngine;

public class BuildLocalizator : MonoBehaviour
{
    public GameObject ConstructionSite;
    public float Speed = 5f;

    //private GameObject _freeHuman;
    //private Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //_freeHuman = GameObject.Find("Human");
        //_targetPosition = _freeHuman.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildConstructionPlace();

            var moveToConstructionTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            EventManager.Instance.TriggerMoveEvent(moveToConstructionTargetPosition);
        }

        //_freeHuman.transform.position = Vector3.MoveTowards(_freeHuman.transform.position, _targetPosition, Speed * Time.deltaTime);
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
