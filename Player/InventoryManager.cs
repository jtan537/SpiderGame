using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A bit of a misnomer...
// manages whatever is currently equipped.
public class InventoryManager : MonoBehaviour
{
    public IHand Equipped { get; set; }
    
    public void Awake()
    {
        Equipped = GetComponent<IHand>();
        if(Equipped != null)
        {
            Debug.Log("Successful Equip!");
        }
    }
    public void MainAction()
    {
        Equipped.Use();
    }
}
