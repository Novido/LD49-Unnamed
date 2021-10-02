using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FuelManager : MonoBehaviour
{
    private ShipManager shipManager;
    private Text fuelGuageText;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        shipManager = GameObject.Find("Ship").GetComponent<ShipManager>();
        fuelGuageText = GameObject.Find("FuelText").GetComponent<Text>();
        slider = GameObject.Find("FuelSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelGuageText.text = (shipManager.GetFuelPercentage() * 100).ToString("0.00") + "%" ;
        slider.value = shipManager.GetFuelPercentage();
    }
}
