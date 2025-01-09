using UnityEngine;

public class ConstructionProgress : MonoBehaviour
{
    public IConstruction Construction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Construction == null)
        {
            return;
        }

        //Debug.Log(Construction.GetType());
        Debug.Log(Construction?.Progress);
    }
}
