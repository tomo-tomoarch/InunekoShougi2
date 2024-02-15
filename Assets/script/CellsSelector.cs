using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSelector : MonoBehaviour
{

    public bool masLock = false;
    public bool masTarget = false;
    public int komaNum;
    int selectedPosNumber;

    void Start()
    {
      
    }
    

    void OnMouseOver()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得

        selectedPosNumber = masHandler.masNumber;
        //選択されたマスの位置ナンバーを取得

        komaNum = masuInfo.GetKomaNum(selectedPosNumber);
        //盤面上の駒が何か判別する

        CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
        //masHandlerスクリプトの取得

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();



        if (masLock == false && 0 < komaNum && komaNum< 31)　//マスロックが掛かってなく、かつ、自駒の場合
        {
            cellsHilighter.HilightWhite();
            //キューブをハイライト
        }
        else
        {
            cellsHilighter.HilightDefault();
        }

        if (cellsCreator.MovableAreaNums.Contains(selectedPosNumber))
        {
            cellsHilighter.HilightRed();
        }
    }

    void OnMouseExit()
    {
        CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
        //masHandlerスクリプトの取得

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreatorスクリプトの取得

        if (masLock == false && 0 < komaNum && komaNum < 31)　//マスロックが掛かってなく、かつ、自駒の場合
        {
            cellsHilighter.HilightDefault();
            //駒があるマスだけハイライト
        }
        if (cellsCreator.MovableAreaNums.Contains(selectedPosNumber))
        {
            cellsHilighter.HilightWhite();
            //駒があるマスだけハイライト
        }
    }

    void OnMouseDown()
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnLockMas();
        //全てのマスをアンロック

        if ( 0 < komaNum && komaNum < 31)//マスロックが掛かってなく、かつ、自駒の場合
        {

            cellsCreator.UnSelectMas();
            //マスを全てデフォルトカラーにクリア

            CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
            cellsHilighter.HilightMyField();
            //動かせる場所をハイライト

            masLock = true;
            //マスをロック

            cellsCreator.UnlockField();
            //マスターゲットアンロックフィールドに

            cellsHilighter.HilightBlue();
            //マスを青にハイライト
        }
    }
}
