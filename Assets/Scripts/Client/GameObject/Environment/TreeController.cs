using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : EnvironmentController
{
    public SpriteRenderer UpperTree;
    public SpriteRenderer LowerTree;
    public SpriteRenderer Shadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (collision.CompareTag("MyPlayer"))
        //{
        //    SpriteRenderer[] TreeSprites = GetComponentsInChildren<SpriteRenderer>();
        //    foreach (SpriteRenderer TreeSprite in TreeSprites)
        //    {
        //        TreeSprite.color = new Color(1, 1, 1, 0.5f);
        //    }
        //}
    }

    // 플레이어 접근하면 이미지를 투명하게 바꿈
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("MyPlayer"))
       {
            UpperTree.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            LowerTree.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            Shadow.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
       }
    }

    // 플레이어가 벗어나면 원상태로 되돌림
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MyPlayer"))
        {
            UpperTree.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            LowerTree.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Shadow.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
