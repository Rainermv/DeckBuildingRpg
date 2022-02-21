using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller.MovementResolver
{
    public class AStarPathFindResolver : IPathFindResolver
    {
        public Func<GridPosition, bool> OnIsPositionValid { get; set; }
        public Func<GridPosition, GridPosition, double> OnGetCostToCrossAtoB { get; set; }
        

        private class PriorityPositionSet
        {
            private List<GridPosition> _positions = new();
            public bool Any => _positions.Any();

            public void Add(GridPosition position)
            {
                _positions.Add(position);
            }

            public void Set(GridPosition position)
            {
                if (!_positions.Contains(position))
                {
                    _positions.Add(position);
                }
            }

            public List<GridPosition> ToList()
            {
                //return _positions.Select(element => element).ToList();
                return _positions;
            }

            public void Remove(GridPosition removePosition)
            {
                _positions.RemoveAll(position => position == removePosition);
            }


            public GridPosition GetLowest(ScoreSet fScore)
            {
                return _positions.OrderBy(pos => fScore.GetScore(pos)).First();
            }
        }

        private class ScoreSet
        {
            private Dictionary<GridPosition, double> _scores = new();

            public void Set(GridPosition position, double score)
            {
                if (_scores.ContainsKey(position))
                {
                    _scores[position] = score;
                    return;
                }

                _scores.Add(position, score);
            }

            public double GetScore(GridPosition position)
            {
                return _scores.TryGetValue(position, out var score) 
                    ? score 
                    : double.MaxValue;
            }
        }

        private class LinkedPositionSet
        {
            private Dictionary<GridPosition, GridPosition> _linkedPositions = new();
            public List<GridPosition> Keys => _linkedPositions.Keys.ToList();

            public void Set(GridPosition target, GridPosition current)
            {
                if (_linkedPositions.ContainsKey(target))
                {
                    _linkedPositions[target] = current;
                    return;
                }

                _linkedPositions.Add(target, current);
            }

            public GridPosition Get(GridPosition current)
            {
                return _linkedPositions[current];
            }
        }



        public PathFindResult FindPathToTarget(GridPosition start, GridPosition goal)
        {
            // The set of discovered nodes that may need to be (re-)expanded.
            // Initially, only the start node is known.
            // This is usually implemented as a min-heap or priority queue rather than a hash-set.
            //openSet:= { start}
            var openSet = new PriorityPositionSet();
            openSet.Add(start);

            // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from start
            // to n currently known.
            //cameFrom:= an empty map
            var cameFrom = new LinkedPositionSet();

            // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.

            //gScore:= map with default value of Infinity
            //gScore[start] := 0
            var gScore = new ScoreSet();
            gScore.Set(start, 0);

            // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
            // how short a path from start to finish can be if it goes through n.
            //fScore:= map with default value of Infinity
            //fScore[start] := h(start)
            var fScore = new ScoreSet();
            fScore.Set(start, Distance(start, goal));

            //while openSet is not empty
            while (openSet.Any)
            {

                // This operation can occur in O(1) time if openSet is a min-heap or a priority queue
                //current := the node in openSet having the lowest fScore[] value
                //if current = goal
                //return reconstruct_path(cameFrom, current)
                var current = openSet.GetLowest(fScore);
                if (current == goal)
                {
                    return new PathFindResult()
                    {
                        PathFound = true,
                        MovementPathPositions = reconstructPath(cameFrom, current)
                    };

                }
                //openSet.Remove(current)
                openSet.Remove(current);

                //for each neighbor of current
                foreach (var neighbor in NeighbourPositions(current))
                {
                    // if the neighbour is not valid, do not consider it (I guess it how it works)
                    /*
                    if (!OnIsPositionValid(neighbor))
                    {
                        continue;
                    }
                    */
                    // d(current,neighbor) is the weight of the edge from current to neighbor
                    // tentative_gScore is the distance from start to the neighbor through current
                    //tentative_gScore := gScore[current] + d(current, neighbor)
                    var tentative_gScore = gScore.GetScore(current) + OnGetCostToCrossAtoB(current, neighbor);

                    //if tentative_gScore < gScore[neighbor]
                    if (tentative_gScore < gScore.GetScore(neighbor))
                    {
                        // This path to neighbor is better than any previous one. Record it!
                        //cameFrom[neighbor] := current
                        //gScore[neighbor] := tentative_gScore
                        //fScore[neighbor] := tentative_gScore + h(neighbor)
                        //if neighbor not in openSet
                        //openSet.add(neighbor)
                        cameFrom.Set(neighbor, current);
                        gScore.Set(neighbor, tentative_gScore);
                        fScore.Set(neighbor, tentative_gScore + Distance(neighbor, goal));
                        openSet.Set(neighbor);

                    }
                }
            }
            // Open set is empty but goal was never reached
            //return failure
            return new PathFindResult()
            {
                PathFound = false,
                MovementPathPositions = new List<GridPosition>()
            };

        }

        private List<GridPosition> NeighbourPositions(GridPosition p)
        {
            var neighbours = new List<GridPosition>();
            for (int x = p.X -1; x <= p.X + 1; x++)
            {
                for (int y = p.Y -1; y <= p.Y + 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    var position = new GridPosition(x, y);
                    neighbours.Add(position);
                }
            }

            return neighbours;
        }

        // h is the heuristic function. h(n) estimates the cost to reach goal from node n.

        private double Distance(GridPosition position, GridPosition targetPosition)
        {
            return GridUtilities.Distance(position, targetPosition);
        }

        private List<GridPosition> reconstructPath(LinkedPositionSet cameFrom, GridPosition current)
        {
            var totalPath = new List<GridPosition>() { current };

            while (cameFrom.Keys.Contains(current))
            {
                current = cameFrom.Get(current);
                totalPath = totalPath.Prepend(current).ToList();
            }

            return totalPath;

            /*
             * total_path := {current
        
    while current in cameFrom.Keys:
    current := cameFrom[current]
    total_path.prepend(current)
    return total_path
             *
             *
             */

        }


    }

    
}