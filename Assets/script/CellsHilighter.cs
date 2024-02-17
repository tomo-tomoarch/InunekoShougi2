using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsHilighter : MonoBehaviour
{
    int selectedPosNumber;
    CellsSelector cellsSelector;

    void Start()
    {
        cellsSelector = GetComponent<CellsSelector>();
    }
    void Update()
    {
       

        if (cellsSelector.masLock)
        {
            HilightBlue();
        }
    }

    public void HilightMyField()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        selectedPosNumber = masHandler.masNumber;

        

        if (185 <= selectedPosNumber && selectedPosNumber <= 189 )
        {
            CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
            cellsCreator.HilightField();
        }
        else if (selectedPosNumber == 191)
        {
            CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
            cellsCreator.HilightAllField();
        }
        else 
        {
            
            
        }

        HilightMyKoma();

    }

    public void HilightDark()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(0, 0, 0);
    }

    public void HilightGray()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(145, 126, 59);
    }

    public void HilightWhite()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(255, 255, 255);
        //フィールドをハイライト
    }

    public void HilightBlue()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(122, 122, 255);
    }

    public void HilightRed()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(255, 122, 122);
    }
    public void HilightYellow()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        masHandler.CangeMasColor(122, 255, 122);
    }

    public void HilightDefault()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得
        selectedPosNumber = masHandler.masNumber;

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得
        int a = masuInfo.GetKomaNum(selectedPosNumber);

        if (185 <= selectedPosNumber && selectedPosNumber <= 189)
        {
            HilightGray();
        }

        if (4 < selectedPosNumber / 15 && selectedPosNumber / 15 < 10)
        {
            if (4 < selectedPosNumber % 15 && selectedPosNumber % 15 < 10)
            {
                HilightGray();
                //フィールドをハイライト
            }
            else
            {
                HilightDark();
                //色を戻す
            }
        }
        else
        {
            HilightDark();
            //色を戻す
        }
        HilightMyKoma();
    }
    public void HilightMyKoma()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得
        int a = masuInfo.GetKomaNum(masHandler.masNumber);

        if (0 < a && a < 31)
        {
            HilightYellow();
        }
    }
}
