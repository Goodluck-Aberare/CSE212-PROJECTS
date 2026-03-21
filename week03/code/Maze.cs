using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int UP = 2;
    private const int DOWN = 3;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    private bool[] GetDirections()
    {
        var key = (_currX, _currY);
        if (!_mazeMap.ContainsKey(key))
            throw new InvalidOperationException("Invalid maze position!");
        return _mazeMap[key];
    }

    public void MoveLeft()
    {
        var directions = GetDirections();
        if (directions[LEFT])
            _currX--;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveRight()
    {
        var directions = GetDirections();
        if (directions[RIGHT])
            _currX++;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveUp()
    {
        var directions = GetDirections();
        if (directions[UP])
            _currY--; 
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveDown()
    {
        var directions = GetDirections();
        if (directions[DOWN])
            _currY++;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}