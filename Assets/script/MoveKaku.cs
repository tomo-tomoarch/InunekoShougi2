using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKaku : MonoBehaviour
{
  


    void Start()
    {
        
    }


    void Update()
    {
        
    }
   
    public void MoveKakuNext(int i, int j)
    {
       
        transform.position = new Vector3(i, 0, j);
    }
    public void RightKaku()
    {

    }
    public void UpKaku()
    {

    }
    public void DownKaku()
    {

    }
}



/*
////////////////////////////////////////
foreach (int a in masu)   //盤面に駒を置く
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
/////////////////////////////////////////
*/


/*
/////////////////////////////////////////
GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
//角を出現させる（デバッグ用）
MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
//角スクリプトの取得
CurrentMasuNum = 112;
//角の現在位置マス番号
////////////////////////////////////////
*/

/*
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
             */