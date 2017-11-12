using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductionMenu : MonoBehaviour
{
    
    public GameObject barrackPanel, powerPlantPanel;

    private Canvas canvas;

    private NavigationList<GameObject> barrackPanelList = new NavigationList<GameObject>(), powerPlantPanelList = new NavigationList<GameObject>();

    public int count = 10,
        size = 150,
        scrollSpeed = 500,
        minimumLimitY = 0,
        maximumLimitY = 0;

    

    public bool onPanel;


    private void Start () 
    {
        canvas = FindObjectOfType<Canvas>();

        // calculate how many panel button we need
        float blockCount = (Screen.height / canvas.scaleFactor / size) + 2;
        count = (int) blockCount;

        //calculate limits based on screen resolution for start
        minimumLimitY = -(int)((Screen.height / canvas.scaleFactor / 2) + size);
        maximumLimitY = (int)((Screen.height / canvas.scaleFactor / 2) + size);

        //snap panels edge of limit
        barrackPanel.transform.localPosition = new Vector3(barrackPanel.transform.localPosition.x, maximumLimitY ,
            barrackPanel.transform.localPosition.x);
        powerPlantPanel.transform.localPosition = new Vector3(powerPlantPanel.transform.localPosition.x, maximumLimitY ,
            powerPlantPanel.transform.localPosition.x);

        //if count is calculated more than 100 dont draw to prevent crash 
        if (count < 100)
        {
            DrawPanels();
        }
        
	}


    private void Update ()
    {
        if (onPanel)
        {
            //refresh limits based on screen resolution
            minimumLimitY = -(int)((Screen.height / canvas.scaleFactor / 2) + size);
            maximumLimitY = (int)((Screen.height / canvas.scaleFactor / 2) + size);

            int mouseScroll = (int)Input.GetAxis("Mouse ScrollWheel");

            // move every panel on barrackPanelList
            foreach (var bp in barrackPanelList)
            {
                if (mouseScroll > 0) // forward
                {
                    bp.transform.localPosition -= new Vector3(0, scrollSpeed, 0);
                }
                else if (mouseScroll < 0) // backwards
                {
                    bp.transform.localPosition += new Vector3(0, scrollSpeed, 0);
                }
            }

            // if a panel reaches limits snap it to opposite side of panels
            foreach (var bp in barrackPanelList)
            {

                if (bp.transform.localPosition.y < minimumLimitY)
                {
                    bp.transform.localPosition =
                        new Vector3(bp.transform.localPosition.x,
                            barrackPanelList.GetNextByObject(bp).transform.localPosition.y + size,
                            bp.transform.localPosition.z);
                    if (barrackPanelList.GetNextByObject(bp).transform.localPosition.y + size == 500)
                    {
                        Debug.Log(barrackPanelList.GetNextByObject(bp).transform.localPosition.y + size);
                    }

                    
                }
                else if (bp.transform.localPosition.y > maximumLimitY)
                {
                    bp.transform.localPosition = 
                        new Vector3(bp.transform.localPosition.x,
                            barrackPanelList.GetPrevisousByObject(bp).transform.localPosition.y - size,
                            bp.transform.localPosition.z);
                }
    
            }



            // move every panel on powerPlantPanelList
            foreach (var ppp in powerPlantPanelList)
            {
                if (mouseScroll > 0) // forward
                {
                    ppp.transform.localPosition -= new Vector3(0, scrollSpeed, 0);
                }
                else if (mouseScroll < 0) // backwards
                {
                    ppp.transform.localPosition += new Vector3(0, scrollSpeed, 0);
                }
            }

            // if a panel reaches limits snap it to opposite side of panels
            foreach (var ppp in powerPlantPanelList)
            {

                if (ppp.transform.localPosition.y < minimumLimitY)
                {
                    ppp.transform.localPosition =
                        new Vector3(ppp.transform.localPosition.x,
                            powerPlantPanelList.GetNextByObject(ppp).transform.localPosition.y + size,
                            ppp.transform.localPosition.z);
                }
                else if (ppp.transform.localPosition.y > maximumLimitY)
                {
                    ppp.transform.localPosition =
                        new Vector3(ppp.transform.localPosition.x,
                            powerPlantPanelList.GetPrevisousByObject(ppp).transform.localPosition.y - size,
                            ppp.transform.localPosition.z);
                }
            }
        }	
	}

    // set onPanel variable (from EventTrigger on Unity Editor)
    public void SetOnPanel(bool value)
    {
        onPanel = value;
        if (value)
        {
            MapManager.Instance.pointerOnCell = null;
        }
    }

    // clone panels from barrack panel and powerplant panel "count" times
    private void DrawPanels()
    {

        barrackPanelList.Add(barrackPanel);
        powerPlantPanelList.Add(powerPlantPanel);

        for (int i = 0; i < count - 1; i++)
        {

            if (barrackPanel.activeInHierarchy)
            {
                GameObject bp = Instantiate(barrackPanel, Vector3.zero, Quaternion.identity);
                bp.transform.SetParent(transform);
                bp.transform.localPosition = barrackPanel.transform.localPosition - new Vector3(0, (i + 1) * size, 0);
                bp.transform.localScale = new Vector3(1,1,1);
                barrackPanelList.Add(bp);
            }
            
            if (powerPlantPanel.activeInHierarchy)
            {
                GameObject ppp = Instantiate(powerPlantPanel, Vector3.zero, Quaternion.identity);
                ppp.transform.SetParent(transform);
                ppp.transform.localPosition = powerPlantPanel.transform.localPosition - new Vector3(0, (i + 1) * size, 0);
                ppp.transform.localScale = new Vector3(1, 1, 1);
                powerPlantPanelList.Add(ppp);
            }
        }
    }
}