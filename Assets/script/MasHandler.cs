using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasHandler : MonoBehaviour
{
    public int masNumber;
        
    public void MasNumber(int a) 
    {
        masNumber = a;
    }
    public void CangeMasColor(byte a,byte b, byte c)
    {
        GetComponent<Renderer>().material.color = new Color32(a, b, c, 1);
    }

}
