using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaInfo : MonoBehaviour
{
    // Start is called before the first frame update
    CellsCreator cellsCreator;

    private Vector3 screenPoint;
    private Vector3 offset;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CangeKomColor(byte a, byte b, byte c)
    {
        GetComponent<Renderer>().material.color = new Color32(a, b, c, 1);
    }
    void OnMouseDown()
    {
        cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.HilightField();
        //おけるマスの色を変える

        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //元の位置の取得


        //元のマス番号の取得
    }
    void OnMouseUp()
    {
        cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnSelectMas();
        //盤面のハイライト


        //置いた先のマス番号の取得


    }
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        if (currentPosition.y != 1)//drag 対策
        {
           currentPosition.y = 1;//drag 対策
        }
        transform.position = currentPosition;
        //置いた先の位置の取得

        

    }
}
