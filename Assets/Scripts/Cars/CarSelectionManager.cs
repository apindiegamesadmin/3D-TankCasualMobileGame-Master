using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectionManager : MonoBehaviour
{
    public Car[] cars;
    public Text selectText;
    public TextMeshProUGUI coinAmountText;
    public GameObject price;

    static int index = 0;
    int selectedIndex = 0;
    int coinAmount;

    void Start()
    {
        //cars[index].car.SetActive(true);
        RestoreSelectedCar();
        selectText.text = "Selected";
    }

    public void RestoreSelectedCar()
    {
        foreach (Car car in cars)
        {
            car.car.SetActive(false);
        }
        cars[selectedIndex].car.SetActive(true);
    }

    public void SwitchCars(bool left)
    {
        foreach (Car car in cars)
        {
            car.car.SetActive(false);
        }

        if (left)
        {
            if(index == 0)
            {
                index = cars.Length - 1;
            }
            else
            {
                index--;
            }
        }
        else
        {
            if (index == cars.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        cars[index].car.SetActive(true);
        if (cars[index].owned)
        {
            selectText.gameObject.SetActive(true);
            price.SetActive(false);
            if (index == selectedIndex)
            {
                selectText.text = "Selected";
            }
            else
            {
                selectText.text = "Select";
            }
        }
        else
        {
            selectText.gameObject.SetActive(false);
            price.SetActive(true);
            price.GetComponentInChildren<TextMeshProUGUI>().text = cars[index].price.ToString();
        }
    }

    public void ConfirmCar()
    {
        selectedIndex = index;
        CarSelectionHandler.instance.carindex = index;
        if (cars[index].owned)
        {
            selectText.text = "Selected";
        }
        else
        {
            selectText.gameObject.SetActive(true);
            price.SetActive(false);
            //Reduce coin amount
            coinAmount -= cars[index].price;
            cars[index].owned = true;
            selectText.text = "Select";
        }
    }
}

[Serializable]
public class Car
{
    public GameObject car;
    public int price;
    public bool owned;
}
