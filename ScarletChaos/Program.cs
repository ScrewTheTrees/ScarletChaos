using System;

namespace ScarletResource
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
