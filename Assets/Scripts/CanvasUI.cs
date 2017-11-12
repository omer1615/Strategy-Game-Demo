using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {

    public static CanvasUI Instance;

    private IObject _obj;

    public RawImage objectImage, productionImage;
    public Text objectTypeText;
    public Texture barracksTexture, powerPlantTexture, soldierTexture;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CleanUpMenu();
    }

    //set object information in information menu
    public void SetInformationObject(IObject obj)
    {
        _obj = obj;

        if (obj.GetType() == typeof(Empty))
        {
            objectImage.gameObject.SetActive(false);
            productionImage.gameObject.SetActive(false);
            objectImage.texture = null;
            productionImage.texture = null;
        }
        else if (obj.GetType() == typeof(Barrack))
        {
            objectImage.gameObject.SetActive(true);
            productionImage.gameObject.SetActive(true);
            objectImage.texture = barracksTexture;
            productionImage.texture = soldierTexture;
        }
        else if (obj.GetType() == typeof(PowerPlant))
        {
            objectImage.gameObject.SetActive(true);
            productionImage.gameObject.SetActive(false);
            objectImage.texture = powerPlantTexture;
            productionImage.texture = null;
        }
        else if (obj.GetType() == typeof(Soldier))
        {
            objectImage.gameObject.SetActive(true);
            productionImage.gameObject.SetActive(false);
            objectImage.texture = soldierTexture;
            productionImage.texture = null;
        }


        objectTypeText.text = obj.Name;
    }

    //tell object to produce (from EventTrigger on Unity Editor)
    public void ObjectProduction()
    {
        if (Input.GetMouseButtonDown(0) && _obj != null)
        {
           _obj.Produce();
        }
    }

    private void CleanUpMenu()
    {
        objectTypeText.text = "";
        objectImage.gameObject.SetActive(false);
        productionImage.gameObject.SetActive(false);
    }

}
