using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private float freightWright;    
    private float fuel;
    private float fuelConsumption;
    private float thrust;
    private float rotationalThrust;
    

    public float initialFreightWeight = 1;
    public float initialFuel = 10f;
    public float maxFuel;
    public float initialFuelConsumption = 0.75f;
    public float initialThrustPower = 2f;
    public float initialRotationalThrustPower = 0.1f;

    public float currentFuelAmount;

    public float FreightWeight
    {
        get { return freightWright; }
        set { freightWright = value; }
    }

    public float Fuel
    {
        get { return fuel; }
        set { fuel = value; }
    }

    public float FuelConsumption
    {
        get { return fuelConsumption; }
        set { fuelConsumption = value; }
    }
        
    public float Thrust
    {
        get { return thrust; }
        set { thrust = value; }
    }

    public float RotationalThrust
    {
        get { return rotationalThrust; }
        set { rotationalThrust = value; }
    }



    // Start is called before the first frame update
    void Awake()
    {
        // This might have to change when introducing upgrades.
        Fuel = initialFuel;
        maxFuel = initialFuel;
        FreightWeight = initialFreightWeight;
        Thrust = initialThrustPower;
        RotationalThrust = initialRotationalThrustPower;
        FuelConsumption = initialFuelConsumption;
    }

    // Update is called once per frame
    void Update()
    {
        // For DEBUG
        currentFuelAmount = Fuel;
    }

    public void BurnFuel()
    {
        //Debug.Log("fuelConsumption: " + fuelConsumption.ToString() + "\n FreightWeight: " + FreightWeight.ToString());
        float totalConsumption = FuelConsumption * (FreightWeight / initialThrustPower);
        Fuel -= totalConsumption;
        //Debug.Log("Fuel consumption: " + totalConsumption.ToString());
    }

    public float GetFuelPercentage()
    {
        float total = Fuel / maxFuel;
        if (total < 0)
            return 0;
        else
            return total;
    }


}
