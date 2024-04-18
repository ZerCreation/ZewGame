using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;

        EventManager.Instance.RegisterToMoveEvent(OnMoveToNewTarget);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    _targetPosition.z = transform.position.z;
        //}

        //if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * 5);
        }
    }

    private void OnMoveToNewTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _targetPosition.z = 5f;
        isMoving = true;
        //Debug.Log(transform.position.z);
    }
}
