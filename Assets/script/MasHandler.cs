using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasHandler : MonoBehaviour
{
    public int masNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MasNumber(int a) 
    {
        masNumber = a;
    }
    public void CangeMasColor(byte a,byte b, byte c)
    {
        GetComponent<Renderer>().material.color = new Color32(a, b, c, 1);
    }

}
