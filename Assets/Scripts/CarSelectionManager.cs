using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars;
    public Text selectText;
    static int index = 0;
    int selectedIndex = 0;

    void Start()
    {
        cars[index].SetActive(true);
        selectText.text = "Selected";
    }

    public void RestoreSelectedCar()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        cars[selectedIndex].SetActive(true);
    }

    public void SwitchCars(bool left)
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
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

        cars[index].SetActive(true);
        if(index == selectedIndex)
        {
            selectText.text = "Selected";
        }
        else
        {
            selectText.text = "Select";
        }
    }

    public void ConfirmCar()
    {
        selectedIndex = index;
        CarSelectionHandler.instance.carindex = index;
        selectText.text = "Selected";
    }
}
