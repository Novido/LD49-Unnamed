using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int money;

    public int startingMoney = 100;

    public int Money
    {
        get { return money; }
        set { money = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        Money = startingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
