using MoreMountains.InfiniteRunnerEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoreMountains.InfiniteRunnerEngine
{
    public class CheckComplete : MonoBehaviour
{
    public Rigidbody2D body2D;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (GameManager.Instance.Status != GameManager.GameStatus.GameOver && collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.SetStatus(GameManager.GameStatus.GameOver); 
            LevelManagerForJumpingGame.Instance.PlayerComplelteLevel();
        }
    }

    }
}

