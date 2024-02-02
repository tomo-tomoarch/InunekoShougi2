using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsHilighter : MonoBehaviour
{
    int selectedPosNumber;

    public void HilightCells()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandler�X�N���v�g�̎擾
        selectedPosNumber = masHandler.masNumber;

        if (185 <= selectedPosNumber && selectedPosNumber <= 189)
        {
            CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
            cellsCreator.HilightField();
        }
    }
    
}
