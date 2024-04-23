using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    public UnityEvent MoveFinished;

    private Vector3 _targetPosition;
    public bool IsMoving = false;

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

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * 5);

        if (transform.position == _targetPosition)
        {
            if (IsMoving)
            {
                //Debug.Log("Move finished");
                MoveFinished.Invoke();
            }
            IsMoving = false;
        }
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        OnMoveToNewTarget(targetPosition);
    }

    private void OnMoveToNewTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _targetPosition.z = 5f;
        IsMoving = true;
        //Debug.Log(transform.position.z);
    }
}
