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

        if (masLock == false && 0 < komaNum && komaNum< 31)　//マスロックが掛かってなく、かつ、自駒の場合
        {
            masHandler.CangeMasColor(255, 255, 255);
            //キューブをハイライト
            
        }

        if (masTarget == true)
        {
            masHandler.CangeMasColor(255, 122, 122);
        }
    }

    void OnMouseExit()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得

        if (masLock == false && 0 < komaNum && komaNum < 31)　//マスロックが掛かってなく、かつ、自駒の場合
        {
            masHandler.CangeMasColor(180, 180, 180);
            //キューブの色を戻す
        }
        if (masTarget == true)
        {
            masHandler.CangeMasColor(180, 180, 180);
            //キューブの色を戻す
        }
    }

    void OnMouseDown()
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnLockMas();
        //全てのマスをアンロック

        if ( 0 < komaNum && komaNum < 31)//マスロックが掛かってなく、かつ、自駒の場合
        {
            

            MasHandler masHandler = GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            cellsCreator.GetOriginalPosition(masHandler.masNumber);
            //masHandlerのマス番号をGetOriginalPositionメソッドに代入→動かす元のマス番号の把握

            cellsCreator.GetCurrentKomaNum();
            //今クリックしたマスにおいてある動かす駒が何か判別

            cellsCreator.UnSelectMas();
            //マスを一度クリア

            CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
            cellsHilighter.HilightCells();
            //動かせる場所をハイライト

            masHandler.CangeMasColor(122, 122, 255);
            //マスを青にハイライト
            masLock = true;
            //マスをロック
            cellsCreator.UnlockField();
            //マスターゲットアンロックフィールドに
        }
    }
}
