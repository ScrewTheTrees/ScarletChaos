using System;

namespace ScarletChaos
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameInstance())
                game.Run();
        }
    }
#endif
}
