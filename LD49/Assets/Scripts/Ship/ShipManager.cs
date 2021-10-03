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
    private Rigidbody2D rb2d;
    private Vector2 centerOfMass;
    

    private float initialWeight = 1f;
    public float initialFuel = 10f;
    public float maxFuel;
    public float initialFuelConsumption = 0.5f;
    public float initialThrustPower = 3f;
    public float initialRotationalThrustPower = 0.05f;

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
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.mass = 0; 
        

        // This might have to change when introducing upgrades.
        Fuel = initialFuel;
        maxFuel = initialFuel;
        //FreightWeight = initialFreightWeight;
        Thrust = initialThrustPower;
        RotationalThrust = initialRotationalThrustPower;
        FuelConsumption = initialFuelConsumption;
    }    

    // Update is called once per frame
    void Update()
    {
        // For DEBUG
        //currentFuelAmount = Fuel;
        centerOfMass = rb2d.centerOfMass;
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

    public void UpdateMass()
    {
        rb2d.mass = rb2d.mass + FreightWeight;
    }

    public void ResetMass()
    {
        rb2d.mass = initialWeight;
    }


}
