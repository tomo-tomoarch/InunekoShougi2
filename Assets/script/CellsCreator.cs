using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
   
    const int n = 15;
    //マスの数　
    public GameObject originObject;
    //マスコンポーネント
    public GameObject kaku;
    //角コンポーネント
    public GameObject koma;
    //駒コンポーネント

    public int CurrentMasuNum;
    //駒の現在位置のマスの数字

    public int CurrentKomaNum;
    //現在セレクトされている駒の数字

    public int DestinationMasNum;
    //駒が動く先のマス番号

    public int DestinationKomaNum;
    //駒が動く先のマスにいる駒番号

    public List<int> MovableAreaNums = new List<int>();
    //駒が動ける先のマス


    GameObject clickedGameObject;
    //セレクトされるターゲットマスのゲームオブジェクト格納用

    public int[,] masu = new int[n, n];
    //masu二次元配列の初期化

    List<int> onetoTwotwofive = new List<int>();
    //movablearea計算用二次元配列の初期化

    public int[,] banmen = new int[n, n]{
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,35,34,33,32,31,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,2,3,4,5,0,11,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
    //駒の初期配置2次元配列の宣言

    List<GameObject> masuGameObject = new List<GameObject>();
    //マスをゲームオブジェクトとして格納するリストの宣言


    // 0 = empty, 1 = Gyoku fuse, 2 = Kin fuse, 3 = hisha fuse, 4 = kaku fuse, 5 =keima fuse,
    //            6 = Gyoku Omote, 7= Kin Omote, 8 = Hisha Omote, 9= Kaku Omote, 10= Keima Omote,
    //            11= Matatabi,                  13= Hisha Nari, 14= Kaku Nari, 15= Keima Nari
    //                            17= Kin Uti  , 18= Hisha Uti,  19= Kaku Uti,  20 =Keima Uti
    //                                           23= Hisha Uti Nari, 24=Kaku Uti Nari, 25 Keima Uti Nari,
    //                                          28= Hisha bare, 29= kaku bare,  30= keima bare,
    // 0 = empty, 31 = Gyoku fuse, 32 = Kin fuse, 33 = hisha fuse, 34 = kaku fuse, 35 =keima fuse,
    //            36 = Gyoku Omote, 37= Kin Omote, 38 = Hisha Omote, 39= Kaku Omote, 40= Keima Omote,
    //            41= Hone,                       43= Hisha Nari, 44= Kaku Nari, 45= Keima Nari            
    //                            47= Kin Uti  , 48= Hisha Uti,  49= Kaku Uti,  50 =Keima Uti
    //                                           53= Hisha Uti Nari, 54=Kaku Uti Nari, 55 Keima Uti Nari,
    //                                          58= Hisha bare, 59= kaku bare,  60= keima bare,


    void Start()
    {
        DrawBanmen();
        UnSelectMas();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectNewMasuOrDestination();
        }
          
    }

    void DrawBanmen() //盤面を描く
    {
        int k;


        for (k = 0; k < masu.GetLength(1); k++)
        {
            int i;

            for (i = 0; i < masu.GetLength(1); i++)
            {
                masu[k, i] = (k * masu.GetLength(1)) + i;
                //二次元配列マスのナンバリング1-224

                //Debug.Log(masu[j, i]);　//デバッグ用

                GameObject obj = (GameObject)Instantiate(originObject, new Vector3(k - (n / 2), 0, i - (n / 2)), Quaternion.identity);
                //マスを原点中心に描画する

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //描画したマスのMasHandlerスクリプトの取得

                masHandler.MasNumber(masu[k, i]);
                //MasNumberメソッドの実行1-224のナンバリングをマス自体に付ける

                masuGameObject.Add(obj);
                onetoTwotwofive.Add(masu[k, i]);
                //リストにマス自体を加えマスゲームオブジェクトのリストを作る

            }
        }
    }

    void ResetBanmen()　//盤面を更新する
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う
            DebugText debugText = obj.GetComponent<DebugText>();

            debugText.UpdateText();
            //マスをすべて更新する
        }
    }

    public void GetOriginalPosition(int masNum) //動かす駒の元の位置を取得する
    {
        CurrentMasuNum = masNum;
    }

    public void GetCurrentKomaNum() //動かしている駒の番号を取得する
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得
        CurrentKomaNum = masuInfo.GetKomaNum(CurrentMasuNum);
        //盤面上の駒が何か判別する
    }

    public void CheckKaku()
    {
        List<int> HilightMasuNum = new List<int>();
        //角道をハイライトさせるマスを格納するリスト

        int i;
        int masNum;

        for(i=-10;i<10;i++ )
        {
            masNum = CurrentMasuNum + 14 * i;
            //角道その１

            if (masNum > 224 || masNum< 0 ){ continue; }
            //0-224以外は飛ばす

            HilightMasuNum.Add(masNum); 
            //マスナンバーの格納
        }

        for (i = -10; i < 10; i++)
        {
            masNum = CurrentMasuNum + 16 * i;
            //角道その２

            if (masNum > 224 || masNum < 0) { continue; }
            //0-224以外は飛ばす

            HilightMasuNum.Add(masNum);
            //マスナンバーの格納
        }

        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            int k = masHandler.masNumber;
            //マスの番号を取得する

            foreach (int num in HilightMasuNum)
            {
                //角道のマス番号それぞれに処理を行う

                if (k == num)
                {
                    //マス番号と角道の番号が一致する場合

                    masHandler.CangeMasColor(255,0,0);
                    //色を変える
                }
            }
           
        }
       
    }

    public void UnSelectMas() //フィールド全てをハイライト
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandlerスクリプトの取得

            cellsHilighter.HilightDefault();
        }
    }

    public void HilightField()　//初期配置できる2列だけをハイライト
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandlerスクリプトの取得

            int k = masHandler.masNumber;
            //マスの番号を取得する

            if (7 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsHilighter.HilightWhite();
                    //フィールドをハイライト
                }
                else
                {
                    cellsHilighter.HilightDefault();
                    //色を戻す
                }
            }
            else
            {
                cellsHilighter.HilightDefault();
                //色を戻す
            }
        }
    }

   

    public void UnLockMas()　//全てのマスロック解除
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            cellsSelector.masLock = false;
            //マスロック解除

        }
    }

    public void UnlockField()　//初期配置できる2列だけをマスターゲットオン
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            //CellsSelectorスクリプトの取得

            int k = masHandler.masNumber;
            //マスの番号を取得する

            if (7 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsSelector.masTarget = true;
                }
                else
                {
                   
                }
            }
            else
            {
                
            }
        }
    }

    public void UnTargetField()　//全部マスターゲットオフ
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            //CellsSelectorスクリプトの取得
            cellsSelector.masTarget = false;
        }
    }

    (int,int) Selectmasu()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;
            MasHandler masHandler = clickedGameObject.GetComponent<MasHandler>();
            int a = masHandler.masNumber;

            CellsSelector cellsSelector = clickedGameObject.GetComponent<CellsSelector>();
            //CellsSelectorスクリプトの取得

            CellsHilighter cellsHilighter = clickedGameObject.GetComponent<CellsHilighter>();
            //masHandlerスクリプトの取得

            MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
            //masuInfo スクリプトの取得

            int b = masuInfo.GetKomaNum(a);

            return (a,b); // masuNum a , KomaNum b をタプルで返す
        } else
        {
            Debug.Log("error");
            return (0,0); // 0,0をタプルで返す
        }
    }
    void SelectNewMasuOrDestination() //選択したマスオブジェクトを取得する
    {

        var c = Selectmasu();
        //タプル値を一回受ける

        int a = c.Item1;
        //masuNum
        //Debug.Log(a+"A");

        int b = c.Item2;
        //komaNum
        //Debug.Log(b+"B");
                 
        if (0 < b && b < 31)
        {
            CurrentMasuNum = a;

            CurrentKomaNum = b;

            MovableAreaNums = CalcInitMovableArea(a, b);
            //駒の動けるエリアのマス番号をリストでゲット
        }
        else if (MovableAreaNums.Contains(a))//上のリストに含まれている場合
        {
            CellsHilighter cellsHilighter = clickedGameObject.GetComponent<CellsHilighter>();
            //masHandlerスクリプトの取得

            DestinationMasNum = a;
            //駒が動く先のマスにいる駒番号

            MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
            //masuInfo スクリプトの取得
            DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //動かす先の駒の番号を取得する

            UnTargetField();
            //マスターゲットオフ
            cellsHilighter.HilightWhite();
            //フィールドをハイライト
            SwitchKomaNum();
            //駒をスイッチする
              
          
        }
    }

    public List<int> CalcInitMovableArea(int a, int b)
    {
        List<int> MovableArea = new List<int>();
        //角道をハイライトさせるマスを格納するリスト

       
      
        if (a >184 && a < 192)
        {
            if(b == 11)
            {
                foreach (int x in onetoTwotwofive)
                {
                    if (4 < x / n && x / n < 10)
                    {
                        if (4 < x % n && x % n < 10)
                        {
                            MovableArea.Add(x);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }

                return MovableArea;
            }
            else if (0 < b && b < 31)
            {
                foreach (int x in onetoTwotwofive)
                {
                    if (7 < x / n && x / n < 10)
                    {
                        if (4 < x % n && x % n < 10)
                        {
                            MovableArea.Add(x);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
                return MovableArea;
            }
            else
            {
                return MovableArea;
            }
        }
        else
        {
            return MovableArea;
        }

    }



    public void SwitchKomaNum()//駒番号の交換を行う
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得

        masuInfo.GetCoordinate(CurrentMasuNum);

        int xc = masuInfo.ColumnNumber;
        int yc = masuInfo.LowNumber;

        banmen[xc, yc] = DestinationKomaNum;

        masuInfo.GetCoordinate(DestinationMasNum);

        int xd = masuInfo.ColumnNumber;
        int yd = masuInfo.LowNumber;

        banmen[xd, yd] = CurrentKomaNum;

        //駒番号の交換を行う

        ResetBanmen();
        DrawBanmen();
        UnSelectMas();
        UnTargetField();
    }
 
}
