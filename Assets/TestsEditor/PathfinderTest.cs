using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PathfinderTest
{
    private static GridPosition G(int x, int y)
    {
        return new GridPosition(x, y);
    }


    public static IEnumerable GridPositions
    {
        get
        {
            var gridMap = new List<GridPosition>();

            var WIDTH = 3;
            var HEIGHT = 3;

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    gridMap.Add(new GridPosition(x,y));
                }
            }

            var start = G(0, 0);
            for (int x = 0; x < WIDTH +1; x++)
            {
                for (int y = 0; y < HEIGHT+1; y++)
                {
                    var end = G(x, y);
                    var distance = GridUtilities.Distance(start, end);

                    yield return new TestCaseData(gridMap,
                        start, end, (x< WIDTH && y< HEIGHT), (int)distance +1, distance);
                }
            }
        }
    }
    

    // A Test behaves as an ordinary method
    [TestCaseSource("GridPositions")]
    public void PathfinderSimpleGrid(List<GridPosition> gridMap, GridPosition start, GridPosition target, bool findPath, int expectedSteps, double distance)
    {
        var resolver = new AStarPathFindResolver();
        resolver.OnGetCostToCrossAtoB = (a, b) => gridMap.Contains(b)? 1 : double.MaxValue;
        resolver.OnIsPositionValid = position => gridMap.Contains(position);

        var result = resolver.FindPathToTarget(start, target);

        Debug.Log("Distance: " + distance);
        Debug.Log("Steps: " + string.Join(", ", result.MovementPathPositions));

        Assert.AreEqual(findPath, result.PathFound);
        Assert.AreEqual(findPath? expectedSteps : 0, result.MovementPathPositions.Count);

        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PathfinderTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
