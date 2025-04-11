using UnityEngine;
using UnityEngine.UI;

public class FurnitureShopMenu : MonoBehaviour
{   
    public void Load()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            Debug.Log("Loaded Furinture Shop Menu");
        }));
    }
}
