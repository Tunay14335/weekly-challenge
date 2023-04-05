using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLiterals
{
    public static readonly string SCENE_ENTRY = "Entry";
    public static readonly string SCENE_GAME = "Game";

    public static readonly string TAG_CANNON = "Cannon";
    public static readonly string TAG_BULLET = "Bullet";
    public static readonly string TAG_GROUND = "Ground";

    public static readonly uint INITAL_BULLET_COUNT = 1000;

    public static readonly Vector2 INITAL_ROOT_STONE_VELOCITY = new Vector2(0f, -8f);
    public static readonly Vector2 INITAL_ROOT_STONE_POSITION = new Vector2(0f, 4f);
    public static readonly Vector2 FRAGMENT_STONE_INITIAL_VELOCITY = new Vector2(4f, 4f);

    public static readonly CountInterval INITIAL_STONE_COUNT_INTERVAL = new CountInterval(300, 3000);
}
