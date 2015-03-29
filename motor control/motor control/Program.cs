using System;

namespace motor_control
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ROVMainProgram game = new ROVMainProgram())
            {
                game.Run();
            }
        }
    }
#endif
}

