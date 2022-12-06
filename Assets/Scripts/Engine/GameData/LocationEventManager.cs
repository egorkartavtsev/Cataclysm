using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace GameData
{
    public static class LocationEventManager
    {
        #region Bonfire
        public static Action BonfireSetUp;
        public static Action BonfireDestroy;

        public static void MakePlayerBonfire()
        {
           if(BonfireSetUp != null) BonfireSetUp.Invoke();
        }

        public static void DestroyPlayerBonfire()
        {
            if (BonfireDestroy != null) BonfireDestroy.Invoke();
        }
        #endregion

        #region Buttons
        public static Action CloseButtonPress;

        public static void CloseMenu()
        {
            if (CloseButtonPress != null) CloseButtonPress.Invoke();

        }
        #endregion

        #region BuildingEvents
        public static Action<List<Tile>> ConstructionPlaced;

        public static void PlaceConstruction(List<Tile> tochedTiles)
        { 
            if(ConstructionPlaced != null) ConstructionPlaced.Invoke(tochedTiles);
        }


        #endregion

        #region GameModeEvents
        public static Action<GameMode> GameModeChanged;

        public static void ChangeGameMode(GameMode mode)
        {
            if (GameModeChanged != null) GameModeChanged.Invoke(mode);
        }
        #endregion
    }
}
