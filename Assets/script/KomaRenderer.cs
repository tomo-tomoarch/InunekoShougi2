using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaRenderer : MonoBehaviour
{
    public Sprite[] faces;
    public Sprite komaBack;
    public int komaIndex;
    public int komaBackIndex;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RenderKoma();

    }

    public void RenderKoma()
    {
        MasHandler masHandler = GetComponentInParent<MasHandler>();
        if (masHandler != null)
        {
            // �e�X�N���v�g�̊֐���ϐ����g�p����

            int masuIndex = masHandler.masNumber;

            MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
            //masuInfo �X�N���v�g�̎擾
            komaIndex = masuInfo.GetKomaNum(masuIndex);

            spriteRenderer.sprite = faces[komaIndex];

            if (komaIndex > 30)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
          
        }

    }   
}