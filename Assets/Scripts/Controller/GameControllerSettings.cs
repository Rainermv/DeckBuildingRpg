using System.Collections.Generic;

namespace Assets.Scripts.Controller
{
    public class GameControllerSettings
    {



        public uint MapWidth { get; set; }
        public uint MapHeight { get; set; }
        public Dictionary<int, GridPosition> CharacterPositions { get; set; }

        public static GameControllerSettings Make()
        {
            const uint width = 20;
            const uint height = 20;

            var iwidth = (int)width;
            var iheight = (int)height;

            return new GameControllerSettings()
            {
                MapWidth = width,
                MapHeight = height,
                CharacterPositions = new Dictionary<int, GridPosition>()
                {
                    {0, new GridPosition(0,0)},
                    {1, new GridPosition(iwidth-1, 0)},
                    {2, new GridPosition(0,iheight-1)},
                    {3, new GridPosition(iwidth-1,iheight-1)},
                }
            };
        }
    }
}