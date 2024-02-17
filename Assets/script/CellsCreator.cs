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
    public List<int> BlockedMovableAreaNums = new List<int>();

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
                    cellsHilighter.HilightMyKoma();
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
    public void HilightAllField()　//初期配置できる2列だけをハイライト
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

            if (4 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsHilighter.HilightWhite();
                    //フィールドをハイライト
                    cellsHilighter.HilightMyKoma();
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

    public void HilightKikimichi()
    {

        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            int a = masHandler.masNumber;
            //マスの番号を取得する

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandlerスクリプトの取得


            if (MovableAreaNums.Contains(a))
            {
                cellsHilighter.HilightWhite();
                //フィールドをハイライト
                cellsHilighter.HilightMyKoma();
       
            }
            else
            {

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

            CheckBlockingKoma(a,b);

            HilightKikimichi();
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

           
            if (DestinationKomaNum>30 && DestinationKomaNum < 60)
            {
                TakeKoma();
            }
            else
            {
                UnTargetField();
                //マスターゲットオフ
                cellsHilighter.HilightWhite();
                //フィールドをハイライト
                SwitchKomaNum();
                //駒をスイッチする

                StartCoroutine(WaitOneSecond());
            }
                       
        }

    }

    IEnumerator WaitOneSecond()
    {
        //Debug.Log("Wait start");

        // 1秒待機
        yield return new WaitForSeconds(1);

        OpponentTurnBegin();
    }

    void OpponentTurnBegin()
    {
        List<int> CurrentOpponentKomaNum = new List<int>();
        List<int> CurrentOpponentMovableField = new List<int>();

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();

        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            int a = masHandler.masNumber;

            
            if (33 < a && a < 56)
            {
                //相手の持ち駒のあるマス確認
            
                int b = masuInfo.GetKomaNum(a);

                if (b > 30)
                {
                    //相手の持ち駒の種類確認
                    CurrentOpponentKomaNum.Add(b);
                }
             
            }
        }

        //手駒がある場合
        if (CurrentOpponentKomaNum.Count > 0)
        {
            // 手駒リストからランダムに要素を選択
            int komaindex = UnityEngine.Random.Range(0, CurrentOpponentKomaNum.Count);
            CurrentKomaNum = CurrentOpponentKomaNum[komaindex];
            //駒番号の取得

            foreach (GameObject obj in masuGameObject)
            {
                //マスオブジェクトそれぞれに処理を行う

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //masHandlerスクリプトの取得
                int a = masHandler.masNumber;

                int b = masuInfo.GetKomaNum(a);

                if (b == CurrentKomaNum)
                {
                    CurrentMasuNum = a;
                    //マス番号の取得
                }
            }

            //打てる範囲からランダムにマスを選択
            CurrentOpponentMovableField = MovableAreaEnemyField();
            int masuindex = UnityEngine.Random.Range(0, CurrentOpponentMovableField.Count);

            DestinationMasNum = CurrentOpponentMovableField[masuindex];
            //打つ先を取得

            DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //打つ先の駒番号
        }
        else　//手駒がない場合
        {
            foreach (GameObject obj in masuGameObject)
            {
                //マスオブジェクトそれぞれに処理を行う

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //masHandlerスクリプトの取得

                int a = masHandler.masNumber;


                if (4 < a / n && a / n < 10)
                {
                    if (4 < a % n && a % n < 10)
                    {
                        //敵AIの持ち駒のあるマス確認

                        int b = masuInfo.GetKomaNum(a);

                        if (b > 30)
                        {
                            //敵AIの盤面の駒確認
                            CurrentOpponentKomaNum.Add(b);
                        }
                    }
                }
            }

            // 手駒リストからランダムに要素を選択
            int komaindex = UnityEngine.Random.Range(0, CurrentOpponentKomaNum.Count);
            CurrentKomaNum = CurrentOpponentKomaNum[komaindex];
            //駒番号の取得

            foreach (GameObject obj in masuGameObject)
            {
                //マスオブジェクトそれぞれに処理を行う

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //masHandlerスクリプトの取得
                int a = masHandler.masNumber;

                int b = masuInfo.GetKomaNum(a);

                if (b == CurrentKomaNum)
                {
                    CurrentMasuNum = a;
                    //マス番号の取得
                }
            }
            if(CurrentKomaNum == 31)
            {
                CurrentOpponentMovableField = MovableAreaGyoku(CurrentMasuNum);
            }
            else if (CurrentKomaNum == 32)
            {
                CurrentOpponentMovableField = MovableAreaEnemyKin(CurrentMasuNum);
            }
            else if (CurrentKomaNum == 33)
            {
                CurrentOpponentMovableField = MovableAreaHisha(CurrentMasuNum);
            }
            else if (CurrentKomaNum == 34)
            {
                CurrentOpponentMovableField = MovableAreaKaku(CurrentMasuNum);
            }
            else if (CurrentKomaNum == 35)
            {
                CurrentOpponentMovableField = MovableAreaEnemyKeima(CurrentMasuNum);
            }
            
            //打てる範囲からランダムにマスを選択

            int masuindex = UnityEngine.Random.Range(0, CurrentOpponentMovableField.Count);
            DestinationMasNum = CurrentOpponentMovableField[masuindex];
            //打つ先を取得

            DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //打つ先の駒番号
        }

        if( DestinationKomaNum >0 && DestinationKomaNum < 31)
        {
            TakeKoma();
        }
        else
        {
            SwitchKomaNum();
        }
        
    }

    public List<int> CalcInitMovableArea(int a, int b)
    {
        List<int> MovableAreas = new List<int>();
        //マスを格納するリスト

       
      
        if (a >184 && a < 192)
        {
            //打ち駒の場合

            if(b == 11)
            {
                //マタタビの場合
                MovableAreas = MovableAreaAllField();
                return MovableAreas;
            }
            else if (0 < b && b < 15)
            {
                //初手の自駒の場合
                MovableAreas = MovableAreaMyField();
                return MovableAreas;
            }
            else if (16 < b && b < 31)
            {
                //取得した打ち駒の場合
                MovableAreas = MovableAreaAllField();
                return MovableAreas;
            }else
            {
                return MovableAreas;
            }
        }
        else if(4 < a / n && a / n < 10 && 4 < a % n && a % n < 10)
        {
            //盤上の駒の場合
            if (b == 1)
            {
                //1 = Gyoku fuseの場合
                MovableAreas = MovableAreaGyoku(a);
                return MovableAreas;
            }
            else if (b == 2)
            {
                //2 = Kin fuseの場合
                MovableAreas = MovableAreaKin(a);
                return MovableAreas;
            }
            else if (b == 3)
            {
                //3 = hisha fuseの場合
                MovableAreas = MovableAreaHisha(a);
                return MovableAreas;
            }
            else if (b == 4)
            {
                //4 = kaku fuseの場合
                MovableAreas = MovableAreaKaku(a);
                return MovableAreas;
            }
            else if (b == 5)
            {
                //5 = keima fuseの場合
                MovableAreas = MovableAreaKeima(a);
                return MovableAreas;
            }
            else
            {
                return MovableAreas;
            }            
        }else
        {
            return MovableAreas;
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
        UnSelectMas();
        UnTargetField();
        CurrentMasuNum = 0;
        CurrentKomaNum = 0;
        DestinationKomaNum = 0;
        DestinationMasNum = 0;
        MovableAreaNums = new List<int>();
    }

    public void TakeKoma()
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo スクリプトの取得

        masuInfo.GetCoordinate(CurrentMasuNum);

        int xc = masuInfo.ColumnNumber;
        int yc = masuInfo.LowNumber;

        banmen[xc, yc] = 0;


        if (DestinationKomaNum <= 30)
        {
            if (DestinationKomaNum == 11)
            {
                banmen[2, 11] = 41;
            }
            else if (DestinationKomaNum % 5 == 0)
            {
                banmen[3, 6] = 50;

            }
            else if (DestinationKomaNum % 5 == 1)
            {
                Debug.Log("game end");
            }
            else if (DestinationKomaNum % 5 == 2)
            {
                banmen[3, 9] = 47;
            }
            else if (DestinationKomaNum % 5 == 3)
            {
                banmen[3, 8] = 48;
            }
            else if (DestinationKomaNum % 5 == 4)
            {
                banmen[3, 7] = 49;
            }
            else
            {

            }
            
        }
        else
        {
            if (DestinationKomaNum == 11)
            {
                banmen[12, 12] = 11;
            }
            else if (DestinationKomaNum % 5 == 0)
            {
                banmen[11, 9] = 20;

            }
            else if (DestinationKomaNum % 5 == 1)
            {
                Debug.Log("game end");
            }
            else if (DestinationKomaNum % 5 == 2)
            {
                banmen[11, 6] = 17;
            }
            else if (DestinationKomaNum % 5 == 3)
            {
                banmen[11, 7] = 18;
            }
            else if (DestinationKomaNum % 5 == 4)
            {
                banmen[11, 8] = 19;
            }
            else
            {

            }

        }
        

            masuInfo.GetCoordinate(DestinationMasNum);

        int xd = masuInfo.ColumnNumber;
        int yd = masuInfo.LowNumber;

        banmen[xd, yd] = CurrentKomaNum;

        //駒番号の移動を行う

        ResetBanmen();
        UnSelectMas();
        UnTargetField();
        CurrentMasuNum = 0;
        CurrentKomaNum = 0;
        DestinationKomaNum = 0;
        DestinationMasNum = 0;
        MovableAreaNums = new List<int>();
    }

    void CheckBlockingKoma(int a,int b)
    {
       
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            int c = masHandler.masNumber;
            //int b = masuInfo.GetKomaNum(a);

            if (MovableAreaNums.Contains(c))
            {
                if (b % 5 == 2)
                {
                    if (a/ n == c / n && c > a)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                       
                    }
                    else if (a % n == c % n && a > c)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                    else if (a/ n == c / n && a > c)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                    else if (a % n == c % n && c > a)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                }else if(b % 5 == 3)
                {
                    if (0 == (a - c) % (n + 1) && c > a)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                    else if (0 == (a - c) % (n - 1) && a > c)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                    else if (0 == (a - c) % (n + 1) && a > c)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }
                    else if (0 == (a - c) % (n - 1) && c > a)
                    {
                        if (c != a)
                        {
                            MovableAreaNums.Remove(c);
                        }
                    }

                }
   
            }
        }
     
    }
    public List<int> MovableAreaAllField()
    {
        List<int> MovableArea = new List<int>();

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

    public List<int> MovableAreaMyField()
    {
        List<int> MovableArea = new List<int>();

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

    public List<int> MovableAreaEnemyField()
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 7)
            {
                if (4 < x % n && x % n < 10)
                {
                    MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
                    //masuInfo スクリプトの取得

                    int b = masuInfo.GetKomaNum(x);

                    if(b == 0)
                    {
                        MovableArea.Add(x);
                    }
                                        
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

    public List<int> MovableAreaGyoku(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if ( x == a + n - 1 || x == a + n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n - 1 || x == a - n + 1 || x == a - n )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }


    public List<int> MovableAreaKin(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a - n - 1 || x == a - n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaHisha(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if ( x % n == a % n || x / n == a / n )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaKaku(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (0 == (a - x) % (n + 1) || 0 == (a - x) % (n - 1))
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaKeima(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a - 2*n - 1 || x == a - 2*n + 1 )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }


    public List<int> MovableAreaEnemyKin(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a + n - 1 || x == a + n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaEnemyKeima(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a + 2 * n - 1 || x == a + 2 * n + 1)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }
}
