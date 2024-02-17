using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuInfo : MonoBehaviour
{
    public int ColumnNumber { get; private set; }
    public int LowNumber { get; private set; }
    public int MasuNumber { get; private set; }

   

    public void GetCoordinate(int x)//位置ナンバーを座標に変換
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator スクリプトの取得

        int rows = cellsCreator.banmen.GetLength(0);
        int cols = cellsCreator.banmen.GetLength(1);
        int row = x / cols;
        int col = x % cols;
        //位置ナンバーを座標に変換

        ColumnNumber = row;
        LowNumber = col;

    }
    public void GetMasuNum(int x, int y)//座標を位置ナンバーに変換
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator スクリプトの取得

        MasuNumber = cellsCreator.masu[x, y];
        //座標を位置ナンバーに変換
    }
    public int GetKomaNum(int x)//位置ナンバーを駒ナンバーに変換
    {
        GetCoordinate(x);

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator スクリプトの取得
        return cellsCreator.banmen[ColumnNumber, LowNumber];
    }

}
