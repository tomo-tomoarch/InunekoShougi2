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
foreach (int a in masu)   //�Ֆʂɋ��u��
{
    int number = a;
    int i = number / 15;
    int j = number % 15;


    if (banmen[j, i] > 0)
    {
        GameObject obj = (GameObject)Instantiate(koma, new Vector3(j - (n / 2), 1, i - (n / 2)), Quaternion.identity);
        //���z�u

        KomaInfo komaInfo = obj.GetComponent<KomaInfo>();
        //��̏����Q�b�g

        komaInfo.CangeKomColor(255, 255, 0);
        //��ɐF��t����
    }
};
/////////////////////////////////////////
*/


/*
/////////////////////////////////////////
GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
//�p���o��������i�f�o�b�O�p�j
MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
//�p�X�N���v�g�̎擾
CurrentMasuNum = 112;
//�p�̌��݈ʒu�}�X�ԍ�
////////////////////////////////////////
*/

/*
         if (Input.GetButtonDown("Jump"))
         {
             UnSelectMas();
             //�p���\�b�h�̌Ăяo��
         }
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             UnSelectMas();

             GameObject gameObject = GameObject.FindWithTag("Koma");
             Destroy(gameObject);
             //��x������Z�b�g

             GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
             //�p���o��������i�f�o�b�O�p�j
             MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
             //�p�X�N���v�g�̎擾

             int nextKaku = CurrentMasuNum - 1;
             int i = nextKaku / 15;
             int j = nextKaku % 15;
             CurrentMasuNum = nextKaku;
             //���̃}�X���v�Z����

             moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
             //�p�𓮂���

             CheckKaku();
         }
         if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             UnSelectMas();

             GameObject gameObject = GameObject.FindWithTag("Koma");
             Destroy(gameObject);
             //��x������Z�b�g

             GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
             //�p���o��������i�f�o�b�O�p�j
             MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
             //�p�X�N���v�g�̎擾

             int nextKaku = CurrentMasuNum + 1;
             int i = nextKaku / 15;
             int j = nextKaku % 15;
             CurrentMasuNum = nextKaku;
             //���̃}�X���v�Z����

             moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
             //�p�𓮂���

             CheckKaku();
         }
         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             UnSelectMas();

             GameObject gameObject = GameObject.FindWithTag("Koma");
             Destroy(gameObject);
             //��x������Z�b�g

             GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
             //�p���o��������i�f�o�b�O�p�j
             MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
             //�p�X�N���v�g�̎擾

             int nextKaku = CurrentMasuNum - 15;
             int i = nextKaku / 15;
             int j = nextKaku % 15;
             CurrentMasuNum = nextKaku;
             //���̃}�X���v�Z����

             moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
             //�p�𓮂���

             CheckKaku();
         }
         if (Input.GetKeyDown(KeyCode.DownArrow))
         {
             UnSelectMas();

             GameObject gameObject = GameObject.FindWithTag("Koma");
             Destroy(gameObject);
             //��x������Z�b�g

             GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
             //�p���o��������i�f�o�b�O�p�j
             MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
             //�p�X�N���v�g�̎擾

             int nextKaku = CurrentMasuNum + 15;
             int i = nextKaku / 15;
             int j = nextKaku % 15;
             CurrentMasuNum = nextKaku;
             //���̃}�X���v�Z����

             moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
             //�p�𓮂���

             CheckKaku();
         }
             */