using UnityEngine;

public class ProductionMenuPanelHandle : MonoBehaviour
{
    public GameObject BarrackPointerGameObject, powePlantPointerGameObject;
    private GameObject currentPointer;

    public Vector3 offScreenPosition;

    private void Start()
    {
        ClearPointers();
    }

    private void Update()
    {
        if (currentPointer)
        {
            //get mouse input position in world space
            var v3 = Input.mousePosition;
            v3.z = 1;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            currentPointer.transform.position = new Vector3(v3.x, v3.y, -1);

            //if mouse is clicked create object at currentPointers position
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (currentPointer == BarrackPointerGameObject)
                {
                    MapManager.Instance.DrawObject(ObjectFactory.GetObject(ObjectTypes.Barrack));
                }
                else if (currentPointer == powePlantPointerGameObject)
                {
                    MapManager.Instance.DrawObject(ObjectFactory.GetObject(ObjectTypes.PowerPlant));
                }
                currentPointer = null;
            }
        }
        else
        {
            ClearPointers();
        }
    }

    public void BarrackPlacing()
    {
        if (Input.GetMouseButtonDown(0))
            currentPointer = BarrackPointerGameObject;
    }

    public void PowerPlantPlacing()
    {
        if (Input.GetMouseButtonDown(0))
            currentPointer = powePlantPointerGameObject;
    }

    //move pointer gameobject to offscreen position
    private void ClearPointers()
    {
        BarrackPointerGameObject.transform.position = offScreenPosition;
        powePlantPointerGameObject.transform.position = offScreenPosition;
        
    }

}
