using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlobalSceneGame : MonoBehaviour
{

    static public bool Running = true;
    static public int score = 0;

    public void Pause()
    {
        Running = false;
    }

    public void Resume()
    {
        Running = true;
    }

    public void Change(){
        Running = !Running;
    }
}

public class Utils
{
    public enum Orientation
    {
        UP, RIGHT, DOWN, LEFT
    }

    public static Vector3Int V2I2V3I(Vector2Int vector2, int z = 0)
    {
        var temp = new Vector3Int(vector2.x, vector2.y, z);
        return temp;
    }

    public static float GetAngleFromVector2(Vector2 vector)
    {
        var angle = Vector3.Angle(Vector3.right, vector);
        if (vector.y < 0) angle = 360 - angle;
        return angle;
    }

    public static Vector2 NormalizedVectorFromAngle(float angle)
    {
        var rad = Mathf.Deg2Rad * angle;
        var x = Mathf.Cos(rad);
        var y = Mathf.Sin(rad);
        var vector = new Vector2(x, y);
        return vector;
    }

    public static void MapBoxFill(ref Tilemap map, ref Tile tile, Vector3Int pos, Vector3Int size)
    {
        var x = pos.x + size.x - 1;
        var y = pos.y + size.y - 1;
        map.BoxFill(pos, tile, pos.x, pos.y, x, y);
    }

    public static void Pause()
    {
        GlobalSceneGame.Running = false;
    }

    public static void Open()
    {
        GlobalSceneGame.Running = true;
        GlobalSceneGame.score = 0;
    }
}