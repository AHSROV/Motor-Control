using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace motor_control
{
    class VideoCameras
    {
        private ArduinoPort arduino;
        int currentCamera = 1;
        public VideoCameras(ArduinoPort port)
        {
            this.arduino = port;
        }

        public void SetActiveCamera(int id)
        {
            int modulized = id % 5;
            String msg = String.Format("v{0}", id-1);
            arduino.SendStringln(msg);
            currentCamera = id; 
        }

        public void Next()
        {
            int nextCamera = currentCamera + 1;
            if (nextCamera > 5 || nextCamera < 1)
                return;
            SetActiveCamera(currentCamera + 1);
        }

        public void Previous()
        {
            int previousCamera = currentCamera - 1;
            if (previousCamera > 5 || previousCamera < 1)
                return;
            SetActiveCamera(previousCamera);
        }

        public int GetCurrentCamera()
        {
            return currentCamera;
        }

        internal static void DrawVideoCameraText(Microsoft.Xna.Framework.Vector2 textPosition, VideoCameras videoCameras, Microsoft.Xna.Framework.Graphics.SpriteFont mainFont, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Color color;
            string message;
            string[] cameraNames = { "Laser", "Down", "Back", "Stomper", "Claw" };

            textPosition.Y += 10;
            message = "Cameras:";
            spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);
            textPosition.Y += 25;

            for(int i = 0; i < 5; i++)
            {
                color = videoCameras.GetCurrentCamera() == i + 1 ? Color.Red : Color.Black;
                message = String.Format("{0}: {1}", i + 1, cameraNames[i]); 
                spriteBatch.DrawString(mainFont, message, textPosition, color);
                textPosition.Y += 25;
            }
        }
    }
}
