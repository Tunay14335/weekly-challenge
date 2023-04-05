using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stone : MonoBehaviour
{
    private StoneData stoneData;

    private GameObject StoneUI;
    private TextMeshProUGUI CountText;
    private SpriteRenderer Renderer;
    private Rigidbody2D rb;
    
    private const float kScaleFactor = 1.4f;
    private const float kGroundReaction = 3f;

    private static readonly Vector2 DefaultScale = new Vector2(1.5f, 1.5f);

    public uint Count {get; set;}
    public uint Scale {get; set;}
    public Vector2 InitVelocity {get; set;}

    #region Unity Impls

    private void Awake()
    {
        StoneUI = transform.GetChild(0).gameObject;
        CountText = StoneUI.GetComponentInChildren<TextMeshProUGUI>();
        Renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        StoneUI.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    
    private void OnEnable()
    {
        this.transform.localScale = DefaultScale;

        stoneData = new StoneData(Count, Scale, InitVelocity);

        this.transform.localScale *= kScaleFactor*(float)stoneData.Scale / StoneLiterals.Scale.Max;

        stoneData.OnLevelChanged += ReloadSprite;
        
        ReloadSprite();
        ReloadText();

        rb.velocity = InitVelocity;
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag(GameLiterals.TAG_CANNON))
        {
            Objectives.Instance.ReloadCurrentStageObjective(Objectives.Stage.LoseGame);
            GameCore.Instance.RequestEndGame();
            Debug.Log("Game Over");
        }

        if(coll.CompareTag(GameLiterals.TAG_BULLET))
        {
            Bullet.Remove(coll.gameObject);
            stoneData.Count--;
            ScoreSystem.Instance.IncreaseScore();
            if(stoneData.Count == 0) Break();
            ReloadText();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag(GameLiterals.TAG_GROUND))
        {
            rb.velocity = (rb.velocity * Vector2.right + Vector2.up * kGroundReaction);
        }
    }
    
    #endregion

    #region Custom Impls

    private void ReloadText()
    {
        CountText.text = stoneData.Count.ToString();
    }

    private void ReloadSprite()
    {
        Renderer.sprite = GameCore.Instance.GetCrystalSprites[(int)stoneData.Level-1];
    }
    
    private void Break()
    {
        if(Scale > StoneLiterals.Scale.Min)
        {
            GameObject child1 = GameCore.Instance.GetStonePool.Dequeue() as GameObject;
            GameObject child2 = GameCore.Instance.GetStonePool.Dequeue() as GameObject;

            uint initCount = (uint)Mathf.FloorToInt((float)Count/2);

            Stone childStone1 = child1.GetComponent<Stone>();
            Stone childStone2 = child2.GetComponent<Stone>();

            childStone1.Count = initCount;
            childStone2.Count = initCount;

            childStone1.Scale = Scale-1;
            childStone2.Scale = Scale-1;

            childStone1.InitVelocity = GameLiterals.FRAGMENT_STONE_INITIAL_VELOCITY * new Vector2(1,1);
            childStone2.InitVelocity = GameLiterals.FRAGMENT_STONE_INITIAL_VELOCITY * new Vector2(-1,1);
            
            childStone1.transform.position = this.transform.position;
            childStone2.transform.position = this.transform.position;

            childStone1.gameObject.SetActive(true);
            childStone2.gameObject.SetActive(true);
        }

        Objectives.Instance.breakStones.Progress++;
        GameCore.Instance.GetStonePool.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }

    #endregion

}
