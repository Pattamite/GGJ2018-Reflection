using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVar : MonoBehaviour {
    public static string BLOCK_TAG_REFLECTIVE = "ReflectiveObject";
    public static string ENEMY_TAG = "Enemy";
    public static string BLOCK_TAG = "Block";
    public static string WALL_TAG = "Wall";
    public static int LAYER_BLOCK = 0;
    public static int LAYER_ENEMY = 8;
    public static float ATTACK_DELAY = 0.5f;
    public static string UI_MENU_PAUSE = "PauseMenu";
    public static string UI_MENU_GAMEOVER = "GameOverMenu";
    public static int STATE_GAME_PLAYING = 1;
    public static int STATE_GAME_PAUSE = 2;
    public static int STATE_GAME_OVER = 3;


}
