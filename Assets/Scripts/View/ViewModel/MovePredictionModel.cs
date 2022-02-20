using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.View
{
    internal class MovePredictionModel
    {
        public static MovePredictionModel Make(
            Action<MovePredictionModel> onMovePredictionModelUpdate)
        {
            return new MovePredictionModel()
            {
                _onMovePredictionModelUpdate = onMovePredictionModelUpdate,
                GridPositions = new List<GridPosition>()
            };
        }

        public List<GridPosition> GridPositions { get; private set; }
        public GridPosition LastPosition => GridPositions.LastOrDefault();

        private Action<MovePredictionModel> _onMovePredictionModelUpdate;

        public void Reset()
        {
            GridPositions = new List<GridPosition>();
        }

        public void Set(List<GridPosition> gridPositions)
        {
            GridPositions = gridPositions;
            _onMovePredictionModelUpdate(this);

        }
    }
}