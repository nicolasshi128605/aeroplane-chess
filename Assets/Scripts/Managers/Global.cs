using UI;

namespace Managers
{
    public static class Global
    {
        public static DeckManager DeckManager;
        public static GameManager GameManager;
        public static Player.Player Player;
        public static Player.Player Bot;
        public static CardDisplayManager CardDisplayManager;
        public static CardDescription CardDescription;

        public static bool doubleDMG;
        public static bool DMGHeal;

        public static void Clear()
        {
            doubleDMG = false;
            DMGHeal = false;
        }
    }
}