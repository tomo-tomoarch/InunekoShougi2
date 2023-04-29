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
        //������}�X�̐F��ς���

        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //���̈ʒu�̎擾


        //���̃}�X�ԍ��̎擾
    }
    void OnMouseUp()
    {
        cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnSelectMas();
        //�Ֆʂ̃n�C���C�g


        //�u������̃}�X�ԍ��̎擾


    }
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        if (currentPosition.y != 1)//drag �΍�
        {
           currentPosition.y = 1;//drag �΍�
        }
        transform.position = currentPosition;
        //�u������̈ʒu�̎擾

        

    }
}
