using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
    // Start is called before the first frame update

    const int n = 15;
    //マスの数　
    public GameObject originObject;
    //マスコンポーネント
    public GameObject kaku;
    //角コンポーネント
    public GameObject koma;

    public int CurrentMasuNum;
    //駒の現在位置のマスの数字

    int[,] masu = new int[n, n];
    //二次元配列マスのナンバリング1-224

    int[,] banmen = new int[n, n]{
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,35,340,33,32,31,0,0,0,0,0},
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


    void Start()
    {

        int k;
    

        for (k = 0; k < masu.GetLength(1); k++)
        {
            int i;

            for (i = 0; i < masu.GetLength(1); i++)
            {
                masu[k, i] = (k * masu.GetLength(1)) + i;

                //Debug.Log(masu[j, i]);　//デバッグ用

                GameObject obj = (GameObject)Instantiate(originObject, new Vector3(k - (n / 2), 0, i - (n / 2)), Quaternion.identity);
                //マスを原点中心に描画する

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //描画したマスのMasHandlerスクリプトの取得

                masHandler.MasNumber(masu[k, i]);
                //MasNumberメソッドの実行1-224のナンバリングをマス自体に付ける

                masuGameObject.Add(obj);
            　　//リストにマス自体を加えマスゲームオブジェクトのリストを作る

            }
        }
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
       
        foreach (int a in masu)
        {
            int number = a;
            int i = number / 15;
            int j = number % 15;


            if (banmen[j, i] > 0)
            {
                GameObject obj = (GameObject)Instantiate(koma, new Vector3(j - (n / 2), 1, i - (n / 2)), Quaternion.identity);
                //駒を配置

                KomaInfo komaInfo = obj.GetComponent<KomaInfo>();
                //駒の情報をゲット

                komaInfo.CangeKomColor(255, 255, 0);
                //駒に色を付ける
            }
        };

        //盤面に駒を置く





        GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
        //角を出現させる（デバッグ用）
        MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
        //角スクリプトの取得
        CurrentMasuNum = 112;
        //角の現在位置マス番号
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            UnSelectMas();
            //角メソッドの呼び出し
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UnSelectMas();

            GameObject gameObject = GameObject.FindWithTag("Koma");
            Destroy(gameObject);
            //一度駒をリセット

            GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
            //角を出現させる（デバッグ用）
            MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
            //角スクリプトの取得
            
            int nextKaku = CurrentMasuNum - 1;
            int i = nextKaku / 15;
            int j = nextKaku % 15;
            CurrentMasuNum = nextKaku;
            //次のマスを計算する

            moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
            //角を動かす

            CheckKaku();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UnSelectMas();

            GameObject gameObject = GameObject.FindWithTag("Koma");
            Destroy(gameObject);
            //一度駒をリセット

            GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
            //角を出現させる（デバッグ用）
            MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
            //角スクリプトの取得

            int nextKaku = CurrentMasuNum + 1;
            int i = nextKaku / 15;
            int j = nextKaku % 15;
            CurrentMasuNum = nextKaku;
            //次のマスを計算する

            moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
            //角を動かす

            CheckKaku();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UnSelectMas();

            GameObject gameObject = GameObject.FindWithTag("Koma");
            Destroy(gameObject);
            //一度駒をリセット

            GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
            //角を出現させる（デバッグ用）
            MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
            //角スクリプトの取得

            int nextKaku = CurrentMasuNum - 15;
            int i = nextKaku / 15;
            int j = nextKaku % 15;
            CurrentMasuNum = nextKaku;
            //次のマスを計算する

            moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
            //角を動かす

            CheckKaku();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UnSelectMas();

            GameObject gameObject = GameObject.FindWithTag("Koma");
            Destroy(gameObject);
            //一度駒をリセット

            GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
            //角を出現させる（デバッグ用）
            MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
            //角スクリプトの取得

            int nextKaku = CurrentMasuNum + 15;
            int i = nextKaku / 15;
            int j = nextKaku % 15;
            CurrentMasuNum = nextKaku;
            //次のマスを計算する

            moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
            //角を動かす

            CheckKaku();
        }
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

    public void UnSelectMas()
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得

            int k = masHandler.masNumber;
            //マスの番号を取得する

            if (4 < k / 15 && k / 15 < 10)
            {
                if (4 < k % 15 && k % 15 < 10)
                {
                    masHandler.CangeMasColor(255, 255, 255);
                    //フィールドをハイライト
                }
                else
                {
                    masHandler.CangeMasColor(122, 122, 122);
                    //色を戻す
                }
            }
            else
            {
                masHandler.CangeMasColor(122, 122, 122);
                //色を戻す
            }



        }
    }

    public void HilightField()
    {
        foreach (GameObject obj in masuGameObject)
        {
            //マスオブジェクトそれぞれに処理を行う

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandlerスクリプトの取得
            int k = masHandler.masNumber;
            //マスの番号を取得する

            if (7 < k / 15 && k / 15 < 10)
            {
                if (4 < k % 15 && k % 15 < 10)
                {
                    masHandler.CangeMasColor(122, 122, 255);
                    //フィールドをハイライト
                }
                else
                {
                    masHandler.CangeMasColor(122, 122, 122);
                    //色を戻す
                }
            }
            else
            {
                masHandler.CangeMasColor(122, 122, 122);
                //色を戻す
            }
        }
    }
}
