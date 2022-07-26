using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{

    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    public void Eat(ItemStack food)
    {
        if (food.item.Edible)
        {
            //update the day
            
            //update the checklist


            food.RemoveFromStack(1);
            gardenManager.FoodEaten = true;

        }

    }

}
