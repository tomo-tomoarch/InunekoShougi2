using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public TextMeshPro myText;
    string newText;
    int komaNum;
    int selectedPosNumber;

    void Start()
    {
        UpdateText();
    }

    public void GetKomaNum()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandlerスクリプトの取得

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator スクリプトの取得

        selectedPosNumber = masHandler.masNumber;
        //選択されたマスの位置ナンバーを取得

        int rows = cellsCreator.banmen.GetLength(0);
        int cols = cellsCreator.banmen.GetLength(1);
        int row = selectedPosNumber / cols;
        int col = selectedPosNumber % cols;
        //位置ナンバーを座標に変換

        komaNum = cellsCreator.banmen[row, col];
        //盤面上の駒が何か判別する
    }
    public void UpdateText()
    {
        GetKomaNum();
        int number = komaNum;
        string numberAsString = "" + number;
        DebugTextUpdate(numberAsString);
    }

    public void DebugTextUpdate(string newText)
    {  
         myText.text = newText;
    }

}
