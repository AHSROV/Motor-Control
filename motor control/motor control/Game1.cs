#define USE_RELAYS


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

// This code likes Courtney 
namespace motor_control
{
    /// <summary>
    /// This is the main type for your games
    /// </summary>
    public class ROVMainProgram : Microsoft.Xna.Framework.Game
    {

        #region Mobin & Mebin: BarGraph
        
        Texture2D BarGraph;
        Texture2D Bar;
        Texture2D Rov_All;
        Texture2D ROV_Status_001;
        Texture2D ROV_Status_002;
        Texture2D ROV_Status_003;
        Texture2D ROV_Status_004;
        Texture2D ROV_Status_005;
        Texture2D ROV_Status_006;
        Texture2D ROV_Status_007;
        Rectangle ROV_Rectangle;
        SpriteFont BarGraphFont;
        Rectangle Bar1Rectangle;
        Rectangle Bar2Rectangle;
        Rectangle Bar3Rectangle;
        Rectangle Bar4Rectangle;
        Rectangle Bar5Rectangle;
        Rectangle Bar6Rectangle;
        Rectangle Bar7Rectangle;
        Rectangle Bar8Rectangle;
        Rectangle BarGraphRectangle;
        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = 1000;
        const int CLAMP = WINDOW_HEIGHT-290;
        //rotation of the ROV
        float current_Rotation;
        Vector2 origin;

        
        #endregion
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainFont;

        ArduinoPort arduino;

        Motor upFront;
        Motor upBack;
        Motor frontLeft;
        Motor frontRight;
        Motor backLeft;
        Motor backRight;

        Relay leech;
        Relay stomper;
        Relay claw;
        Relay rack;
        Relay laser;

        VideoCameras videoCameras;
        ConductivitySensor conductivitySensor;

        bool travelMode = false;
        bool firstTime = true;

        bool displayErrors = false;
        bool displaySpeedText = true;
        bool displaySpeedGraphs = true;

        GamePadState lastPadState;
        KeyboardState lastKeyState;

        MotorSpeeds desiredSpeeds;

        IControls set1;
        IControls set2;
        IControls set3;
        IControls set4;
        IControls set5;
        IControls currentControls;

        Stopwatch timeoutStopwatch;

        bool testingMode;

        ShipwreckChooser shipwreckChooserDialog = new ShipwreckChooser();

        private bool emergencyTriggered = false;

        // Audio objects
        SoundEffect emergencySoundEffect;

        public ROVMainProgram()
        {
            //make the object port
            arduino = new ArduinoPort();

            //ask the user what port to use
            var dialog = new PortSelector();
            dialog.ShowDialog();

            arduino.SetUp(dialog.arduinoPortName());

            testingMode = dialog.testingModeChecked();

            // setup game graphics
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set up timeout stopwatch
            timeoutStopwatch = new Stopwatch();
            timeoutStopwatch.Start();

            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            this.graphics.SynchronizeWithVerticalRetrace = true;

            //Width = 290 and Height = 285 for Just the BarGraph at .5F scale
            this.graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            this.graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            
            this.Window.BeginScreenDeviceChange(true);
            this.IsMouseVisible = true;

            arduino.Emergency += new ArduinoPort.EmergecnyEventHandler(Emergency);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region Mobin + Mebin: Bargraph Initialize

            // TODO: Add your initialization logic here
            ROV_Status_001 = Content.Load<Texture2D>("ROV000");
            Rov_All = Content.Load<Texture2D>("ROV_ALL");
            ROV_Status_002 = Content.Load<Texture2D>("ROV002");
            ROV_Status_003 = Content.Load<Texture2D>("ROV003");
            ROV_Status_004 = Content.Load<Texture2D>("ROV004");
            ROV_Status_005 = Content.Load<Texture2D>("ROV005");
            ROV_Status_006 = Content.Load<Texture2D>("ROV006");
            ROV_Status_007 = Content.Load<Texture2D>("ROV007");
         
            Bar = Content.Load<Texture2D>("Bar");
            BarGraph = Content.Load<Texture2D>("Background");
            BarGraphRectangle = new Rectangle(0, 0, BarGraph.Width, BarGraph.Height);
            Bar1Rectangle = new Rectangle(8, CLAMP / 2,  Bar.Width, Bar.Height);
            Bar2Rectangle = new Rectangle(70, CLAMP / 2, Bar.Width, Bar.Height);
            Bar3Rectangle = new Rectangle(130, CLAMP / 2, Bar.Width, Bar.Height);
            Bar4Rectangle = new Rectangle(188, CLAMP / 2, Bar.Width, Bar.Height);
            Bar5Rectangle = new Rectangle(245,CLAMP  / 2, Bar.Width, Bar.Height);
            Bar6Rectangle = new Rectangle(301, CLAMP / 2, Bar.Width, Bar.Height);
            Bar7Rectangle = new Rectangle(357, CLAMP / 2, Bar.Width, Bar.Height);
            Bar8Rectangle = new Rectangle(415, CLAMP / 2, Bar.Width, Bar.Height);
            ROV_Rectangle = new Rectangle(768,300, ROV_Status_001.Width,ROV_Status_001.Height);
            BarGraphFont = Content.Load<SpriteFont>("BarGraphFont");
            origin.X = ROV_Status_001.Width / 2;
            origin.Y = ROV_Status_001.Height / 2;
            #endregion

            #region Mobin: BarGraph Scaling

            //Scale here. Both values are naturally .5F

            //FrontLeftUpDownMotorBarGraph.Values.HeightScale = .5F;
            //FrontLeftUpDownMotorBarGraph.Values.WidthScale = .5F;

            #endregion

            set1 = new ChrisControls();
            set2 = new TestControls();
            set3 = new Xcontrols();
            set4 = new DownXControls();
            set5 = new BackXControls();
             
            currentControls = set3;

            #region Chris: make a new comChanel and motors

            upFront = new Motor();
            upBack = new Motor();
            frontLeft = new Motor();
            frontRight = new Motor();
            backLeft = new Motor();
            backRight = new Motor();

            leech    = new Relay();
            stomper       = new Relay();
            claw   = new Relay();
            rack = new Relay();
            laser = new Relay();

            desiredSpeeds = new MotorSpeeds();

            #endregion

            videoCameras = new VideoCameras(arduino);
            conductivitySensor = new ConductivitySensor(arduino);

            // set up for 2014 (incomplete) motor config

            SerialCoordinator coordinator = new SerialCoordinator(700, 6);

            upFront.SetUp    (arduino, 5, coordinator);
            upBack.SetUp     (arduino, 4, coordinator);
            frontLeft.SetUp  (arduino, 0, coordinator);
            frontRight.SetUp (arduino, 1, coordinator);
            backLeft.SetUp   (arduino, 2, coordinator);
            backRight.SetUp  (arduino, 3, coordinator);

            leech.SetUp  (arduino, 3);
            stomper.SetUp(arduino, 1);
            claw.SetUp   (arduino, 0);
            rack.SetUp   (arduino, 2);
            laser.SetUp  (arduino, 4);

            coordinator.Start();
            base.Initialize();
        }

        private void Emergency(object sender, EventArgs e)
        {
            if (emergencyTriggered == false && !testingMode)
            {
                SoundEffectInstance instance = emergencySoundEffect.CreateInstance();
                instance.IsLooped = true;
                instance.Play();
                emergencyTriggered = true;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // loading font
            mainFont = Content.Load<SpriteFont>("MainText");

            // Load sound effect
            emergencySoundEffect = Content.Load<SoundEffect>("tos-redalert");

            //Loading BarGraph Font
            //BarGraphFont = Content.Load<SpriteFont>("BarGraphFont");


            #region Mobin: BarGraph

            //Loading Bar Graphs and Yellow Lines
            //if c key is pressed then change displaySpeedGraphs value
            if (displaySpeedGraphs == true)
            {

                //FrontLeftUpDownMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");
                //FrontRightUpDownMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");

                //BackLeftUpDownMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");
                //BackRightUpDownMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");

                //BackLeftMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");
                //BackRightMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");

                //FrontLeftMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");
                //FrontRightMotorBarGraph.LoadContent(this.Content, "BarGraph", "Yellow Line");
            }
            #endregion

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Update(GameTime gameTime)
        {
            //getting the controler and keyboard state
            GamePadState padState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();
            if (firstTime == true)
            {
                lastKeyState = keyState;
                lastPadState = padState;
                firstTime = false;
            }
          
            #region Chris:Allows the game to exit

            // Allows the game to exit
            if ((padState.Buttons.Back == ButtonState.Pressed) ||
                (keyState.IsKeyDown(Keys.Escape)))
            {
                frontLeft.ShutDown();
                frontRight.ShutDown();
                backLeft.ShutDown();
                backRight.ShutDown();
                claw.ShutDown();
                rack.ShutDown();
                stomper.ShutDown();
                leech.ShutDown();

                this.Exit();
            }
            //Self Destruct sequence :D 
            //enter here (note find how to draw pictures on the screen.)
            #endregion

            #region Chris:Find if Button is Pressed

            //find if the one button is pressed and if so then swap to set one if two is pressed then swap to set 2
            if(keyState.IsKeyDown(Keys.LeftControl) || keyState.IsKeyDown(Keys.RightControl))
            {
                if (keyState.IsKeyDown(Keys.D1))
                {
                    currentControls = set1;
                }
                if (keyState.IsKeyDown(Keys.D2))
                {
                    currentControls = set2;
                }
                if (keyState.IsKeyDown(Keys.D3))
                {
                    currentControls = set3;
                }
                if (keyState.IsKeyDown(Keys.D4))
                {
                    currentControls = set4;
                }
                if (keyState.IsKeyDown(Keys.D5))
                {
                    currentControls = set5;
                }
               
            }
            else
            {
                if (System.Windows.Forms.Form.ActiveForm != null && System.Windows.Forms.Form.ActiveForm.Text.Equals(this.Window.Title))
                {
                    //Set up video camera output
                    Keys[] keys = { Keys.D8, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7 };
                    for (int i = 0; i < 8; i++)
                    {
                        if (lastKeyState.IsKeyDown(keys[i]) != keyState.IsKeyDown(keys[i]))
                        {
                            if (keyState.IsKeyDown(keys[i]))
                            {
                                videoCameras.SetActiveCamera(i);
                            }
                        }
                    }
                }
            }

            //check for the zxc keys being pressed to change what is displayed
            if (lastKeyState.IsKeyDown(Keys.Z) != keyState.IsKeyDown(Keys.Z))
            {
                if (keyState.IsKeyDown(Keys.Z))
                {
                    displaySpeedText = !displaySpeedText;
                }
            }

            if (lastKeyState.IsKeyDown(Keys.X) != keyState.IsKeyDown(Keys.X))
            {
                if (keyState.IsKeyDown(Keys.X))
                {
                    displayErrors = !displayErrors;
                }
            }
            if (lastKeyState.IsKeyDown(Keys.C) != keyState.IsKeyDown(Keys.C))
            {
                if (keyState.IsKeyDown(Keys.C))
                {
                    displaySpeedGraphs = !displaySpeedGraphs;
                }
            }
            if (lastKeyState.IsKeyDown(Keys.R) != keyState.IsKeyDown(Keys.R))
            {
                if (keyState.IsKeyDown(Keys.R))
                {
                    // Yikes!  Reset the arduino connection!
                    arduino.ResetConnection();
                    /*string arduinoPort = arduino.nameOfPort;
                    arduino = new ArduinoPort();
                    arduino.SetUp(arduinoPort);*/
                }
            }
            if (lastKeyState.IsKeyDown(Keys.OemMinus) != keyState.IsKeyDown(Keys.OemMinus))
            {
                if(keyState.IsKeyDown(Keys.OemMinus))
                {
                    laser.SetState(false);
                }
            }
            if (lastKeyState.IsKeyDown(Keys.OemPlus) != keyState.IsKeyDown(Keys.OemPlus))
            {
                if(keyState.IsKeyDown(Keys.OemPlus))
                {
                    laser.SetState(true);
                }
            }
            if (lastKeyState.IsKeyDown(Keys.End) != keyState.IsKeyDown(Keys.End))
            {
                if (keyState.IsKeyDown(Keys.End) && !shipwreckChooserDialog.Visible)
                {
                    shipwreckChooserDialog.Show();
                }
            }
            if (keyState.IsKeyDown(Keys.OemComma))  
            {
                conductivitySensor.SetState(false);
            }
            if (keyState.IsKeyDown(Keys.OemPeriod))
            {
                conductivitySensor.SetState(true);
            }
            if (keyState.IsKeyDown(Keys.PageUp))
            {
                travelMode = true;
            }
            else if (keyState.IsKeyDown(Keys.PageDown))
            {
                travelMode = false;
            }
            if (lastPadState.Buttons.LeftShoulder != padState.Buttons.LeftShoulder)
            {
                if(padState.Buttons.LeftShoulder == ButtonState.Pressed)
                   videoCameras.Previous();
            }
            if (lastPadState.Buttons.RightShoulder != padState.Buttons.RightShoulder)
            {
                if (padState.Buttons.RightShoulder == ButtonState.Pressed)
                  videoCameras.Next();
            }
            #endregion

            if (currentControls == set3 || currentControls == set4 || currentControls == set5)
            {
                switch (videoCameras.GetCurrentCamera())
                {
                    case 2: // Down Camera
                        currentControls = set4;
                        break;
                    case 3: // back camera
                    case 4: //Stomp camera
                        currentControls = set5;
                        break;
                    default:
                        currentControls = set3;
                        break;
                }
            }

            //control what control mode we are using depending on the camara in use
            desiredSpeeds = currentControls.Update(padState, lastPadState, keyState, lastKeyState, gameTime, desiredSpeeds);

            if (travelMode == false)
            {
                desiredSpeeds.frontLeft  *= 0.8F;
                desiredSpeeds.frontRight *= 0.8F;
                desiredSpeeds.upFront    *= 0.8F;
                desiredSpeeds.upBack     *= 0.8F;
                desiredSpeeds.backRight  *= 0.8F;
                desiredSpeeds.backLeft   *= 0.8F;
            }

            //set the motor speed equal to the y value
            frontLeft.SetPercentSpeed(desiredSpeeds.frontLeft);
            frontRight.SetPercentSpeed(desiredSpeeds.frontRight);
            upBack.SetPercentSpeed(desiredSpeeds.upBack);
            upFront.SetPercentSpeed(desiredSpeeds.upFront);
            backRight.SetPercentSpeed(desiredSpeeds.backRight);
            backLeft.SetPercentSpeed(desiredSpeeds.backLeft);
            claw.SetState(desiredSpeeds.claw);
            leech.SetState(desiredSpeeds.leech);
            rack.SetState(desiredSpeeds.rack);
            stomper.SetState(desiredSpeeds.stomper);

            // Mobin & Mebin: Update BarGraph
            const float slope = -CLAMP / 2.0f;
            const float yIntercept = CLAMP / 2.0f;
            Bar1Rectangle.Y = (int)(desiredSpeeds.frontLeft * slope + yIntercept);
            Bar2Rectangle.Y = (int)(desiredSpeeds.frontRight * slope + yIntercept);
            Bar3Rectangle.Y = (int)(desiredSpeeds.upFront * slope + yIntercept);
            Bar4Rectangle.Y = (int)(desiredSpeeds.upBack * slope + yIntercept);
            Bar5Rectangle.Y = (int)(desiredSpeeds.backRight * slope + yIntercept);
            Bar6Rectangle.Y = (int)(desiredSpeeds.backLeft * slope + yIntercept);
            Bar7Rectangle.Y = (int)(desiredSpeeds.upBack * slope + yIntercept);
            Bar8Rectangle.Y = (int)(desiredSpeeds.upFront * slope + yIntercept);

            lastPadState = padState;
            lastKeyState = keyState;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (desiredSpeeds.frontLeft > 0.0 && desiredSpeeds.backRight > 0.0 && desiredSpeeds.frontRight < 0.0 && desiredSpeeds.backLeft < 0.0)
            {
                current_Rotation -= elapsed;
            }
            else if (desiredSpeeds.frontLeft < 0.0 && desiredSpeeds.backRight < 0.0 && desiredSpeeds.frontRight > 0.0 && desiredSpeeds.backLeft > 0.0)
            {
                current_Rotation += elapsed;
            }
            

          
            float circle = MathHelper.Pi * 2;
            current_Rotation = current_Rotation % circle;
            base.Update(gameTime);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        { }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // Start drawing sprites
            spriteBatch.Begin();
                  #region Mebin + Mobin: Draw BarGraphs && ROV LOGIC
            #region Mobins
            //Draw Bar Graphs and Yellow Lines
            //The Width Scales are simply to show the intent. Eventually they have to respond to scale

            //FrontLeftUpDownMotorBarGraph.BarGraphPosition = new Vector2(Values.FLeftUDMotorBarGraph_X, Values.FLeftUDMotorBarGraph_Y);
            //FrontLeftUpDownMotorBarGraph.YellowLines.Position = new Vector2(Values.FLeftUDMotorBarGraph_X + 4 * Values.WidthScale, FrontLeftUpDownMotorBarGraph.YellowLines.Position.Y);
            //FrontLeftUpDownMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //FrontRightUpDownMotorBarGraph.BarGraphPosition = new Vector2(Values.FRightUDMotorBarGraph_X, Values.FRightUDMotorBarGraph_Y);
            //FrontRightUpDownMotorBarGraph.YellowLines.Position = new Vector2(Values.FRightUDMotorBarGraph_X + 4 * Values.WidthScale, FrontRightUpDownMotorBarGraph.YellowLines.Position.Y);
            //FrontRightUpDownMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //BackLeftUpDownMotorBarGraph.BarGraphPosition = new Vector2(Values.BLeftUDMotorBarGraph_X, Values.BLeftUDMotorBarGraph_Y);
            //BackLeftUpDownMotorBarGraph.YellowLines.Position = new Vector2(Values.BLeftUDMotorBarGraph_X + 4 * Values.WidthScale, BackLeftUpDownMotorBarGraph.YellowLines.Position.Y);
            //BackLeftUpDownMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //BackRightUpDownMotorBarGraph.BarGraphPosition = new Vector2(Values.BRightUDMotorBarGraph_X, Values.BRightUDMotorBarGraph_Y);
            //BackRightUpDownMotorBarGraph.YellowLines.Position = new Vector2(Values.BRightUDMotorBarGraph_X + 4 * Values.WidthScale, BackRightUpDownMotorBarGraph.YellowLines.Position.Y);
            //BackRightUpDownMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //FrontLeftMotorBarGraph.BarGraphPosition = new Vector2(Values.FwdLeftMotorBarGraph_X, Values.FwdLeftMotorBarGraph_Y);
            //FrontLeftMotorBarGraph.YellowLines.Position = new Vector2(Values.FwdLeftMotorBarGraph_X + 4 * Values.WidthScale, FrontLeftMotorBarGraph.YellowLines.Position.Y);
            //FrontLeftMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //FrontRightMotorBarGraph.BarGraphPosition = new Vector2(Values.FwdRightMotorBarGraph_X, Values.FwdRightMotorBarGraph_Y);
            //FrontRightMotorBarGraph.YellowLines.Position = new Vector2(Values.FwdRightMotorBarGraph_X + 4 * Values.WidthScale, FrontRightMotorBarGraph.YellowLines.Position.Y);
            //FrontRightMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //BackLeftMotorBarGraph.BarGraphPosition = new Vector2(Values.StrafeBackBarGraph_X, Values.StrafeBackBarGraph_Y);
            //BackLeftMotorBarGraph.YellowLines.Position = new Vector2(Values.StrafeBackBarGraph_X + 4 * Values.WidthScale, BackLeftMotorBarGraph.YellowLines.Position.Y);
            //BackLeftMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);

            //BackRightMotorBarGraph.BarGraphPosition = new Vector2(Values.StrafeFrontBarGraph_X, Values.StrafeFrontBarGraph_Y);
            //BackRightMotorBarGraph.YellowLines.Position = new Vector2(Values.StrafeFrontBarGraph_X + 4 * Values.WidthScale, BackRightMotorBarGraph.YellowLines.Position.Y);
            //BackRightMotorBarGraph.Draw(this.spriteBatch, this.spriteBatch);
            #endregion 
            #region Mebins
            spriteBatch.Draw(BarGraph, BarGraphRectangle, Color.White);
            spriteBatch.Draw(Bar, Bar1Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar2Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar3Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar4Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar5Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar6Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar7Rectangle, Color.White);
            spriteBatch.Draw(Bar, Bar8Rectangle, Color.White);
#endregion
            #region Mebin Rov Logic
            if (desiredSpeeds.backLeft < 0.0 && desiredSpeeds.backRight > 0.0 && desiredSpeeds.frontRight > 0.0 && desiredSpeeds.frontLeft < 0.0)
            {
                if (ROV_Rectangle.X <= 820)
                {
                    ROV_Rectangle.X += 10;
                }
            }
            if (desiredSpeeds.backLeft > 0.0 && desiredSpeeds.backRight < 0.0 && desiredSpeeds.frontRight < 0.0 && desiredSpeeds.frontLeft > 0.0)
            {
                if (ROV_Rectangle.X >= 740)
                {
                    ROV_Rectangle.X -= 10;
                }
            }
            if (desiredSpeeds.backLeft < 0.0 && desiredSpeeds.backRight < 0.0 && desiredSpeeds.frontRight > 0.0 && desiredSpeeds.frontLeft > 0.0)
            {
                if (ROV_Rectangle.Y <= 330)
                {
                    ROV_Rectangle.Y += 10;
                }
            }
            if (desiredSpeeds.backLeft > 0.0 && desiredSpeeds.backRight > 0.0 && desiredSpeeds.frontRight < 0.0 && desiredSpeeds.frontLeft < 0.0)
            {
                if (ROV_Rectangle.Y >= 270)
                {
                    ROV_Rectangle.Y -= 10;
                }
            }
           
                if (leech.GetState() == true && claw.GetState() == true && stomper.GetState() == true)
                {
                    spriteBatch.Draw(Rov_All, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == false && claw.GetState() == true && stomper.GetState() == true)
                {
                    spriteBatch.Draw(ROV_Status_002, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == true && claw.GetState() == false && stomper.GetState() == true)
                {
                    spriteBatch.Draw(ROV_Status_003, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == true && claw.GetState() == true && stomper.GetState() == false)
                {
                    spriteBatch.Draw(ROV_Status_004, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == false && claw.GetState() == true && stomper.GetState() == false)
                {
                    spriteBatch.Draw(ROV_Status_005, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == true && claw.GetState() == false && stomper.GetState() == false)
                {
                    spriteBatch.Draw(ROV_Status_006, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else if (leech.GetState() == false && claw.GetState() == false && stomper.GetState() == true)
                {
                    spriteBatch.Draw(ROV_Status_007, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(ROV_Status_001, ROV_Rectangle, null, Color.White, current_Rotation, origin, SpriteEffects.None, 0f);
                }
                
            
                  #endregion
                  #endregion

            // Chris: Draw Motor Controls Text
            Vector2 textPosition;
            string message;

            textPosition.X = 500;
            textPosition.Y = 000;

            //show how to change the interface in use
            message = String.Format("1=Normal, 2=Test, PgUp/PgDn = Speed");
            spriteBatch.DrawString(mainFont, message, textPosition, Color.Orchid);

            //show how to change the display elements
            textPosition.Y += 25;
            message = String.Format("Z = Spds, X = Errs,  END = Grph, R = Rst, C = Hdg");
            spriteBatch.DrawString(mainFont, message, textPosition, Color.Orchid);

            // show current control set
            textPosition.Y += 25;
            string travelText = "Slow";
            if (travelMode == true)
            {
                travelText = "Fast";
            }

            message = String.Format("Control set {0} ({1})", currentControls.GetName(), travelText);
            spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);
            textPosition.Y += 25;

            VideoCameras.DrawVideoCameraText(textPosition, videoCameras, mainFont, spriteBatch);
            
            if (displaySpeedText == true)
            {
                textPosition.X = 30;
                textPosition.Y = CLAMP;
                // Show commanded motor speed
                textPosition.Y += 25;
                message = String.Format("run the forward lift motors at {0}", desiredSpeeds.upFront);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                message = String.Format("run the aft lift motors at {0}", desiredSpeeds.upBack);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                message = String.Format("run the front left motor at {0}", desiredSpeeds.frontLeft);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                message = String.Format("run the front right motor at {0}", desiredSpeeds.frontRight);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                message = String.Format("run the back right motor at {0}", desiredSpeeds.backRight);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                message = String.Format("run the back left at {0}", desiredSpeeds.backLeft);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                String clawState = !claw.GetState() ? "open" : "closed";
                message = String.Format("claw is {0}", clawState);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                string leechState = !leech.GetState() ? "extended" : "retracted";
                message = String.Format("leech is {0}", leechState);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                string rackState = rack.GetState() ? "down" : "up";
                message = String.Format("rack is {0}", rackState);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.Y += 25;
                string stomperState = !stomper.GetState() ? "up" : "down";
                message = String.Format("Stomper is {0}", stomperState);
                spriteBatch.DrawString(mainFont, message, textPosition, Color.Black);

                textPosition.X = 500;
                textPosition.Y = 540;

                Color rectangleColor;
                if (arduino.Connected)
                {
                    message = "Control Box Connected";
                    rectangleColor = Color.Pink;
                }
                else
                {
                    message = "Control Box Not Connected";
                    rectangleColor = Color.LightGray;
                }

                Texture2D texture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                texture.SetData<Color>(new Color[] { rectangleColor });
                spriteBatch.Draw(texture, new Rectangle((int)textPosition.X, (int)textPosition.Y, (int)mainFont.MeasureString(message).X, (int)mainFont.MeasureString(message).Y), rectangleColor);

                spriteBatch.DrawString(mainFont, message, textPosition, Color.Blue);
            }

            // Check for and report any motor errorsl
            // if the x key is pressed then change the value of displayErrors
            if (displayErrors == true)
            {
                textPosition.X = 20;
                textPosition.Y = 300;
            }
            
            #region Mobin: Draw Text BarGraph

            //Draw Motor Names

            //textPosition = new Vector2(Values.FLeftUDMotorBarGraph_X + 6.75f * Values.WidthScale, Values.FLeftUDMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "F.L. Up/Down";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(20, 600), Color.White);

            //textPosition = new Vector2(Values.FRightUDMotorBarGraph_X + 4f * Values.WidthScale, Values.FRightUDMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "F.R. Up/Down";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(112, 600), Color.White);

            //textPosition = new Vector2(Values.BLeftUDMotorBarGraph_X + 4 * Values.WidthScale, Values.BLeftUDMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "B.L. Up/Down";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(200, 600), Color.White);

            //textPosition = new Vector2(Values.BRightUDMotorBarGraph_X + 3.45f * Values.WidthScale, Values.BRightUDMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "B.R. Up/Down";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(288, 600), Color.White);

            //textPosition = new Vector2(Values.FwdLeftMotorBarGraph_X + 30 * Values.WidthScale, Values.FwdLeftMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "Frnt Lt";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(376, 600), Color.White);

            //textPosition = new Vector2(Values.FwdRightMotorBarGraph_X + 20 * Values.WidthScale, Values.FwdRightMotorBarGraph_Y + 530 * Values.HeightScale);
            message = "Frnt Rt";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(464, 600), Color.White);

            //textPosition = new Vector2(Values.StrafeBackBarGraph_X + 14 * Values.WidthScale, Values.StrafeBackBarGraph_Y + 530 * Values.HeightScale);
            message = "Back Lt";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(550, 600), Color.White);

            //textPosition = new Vector2(Values.StrafeFrontBarGraph_X + 14 * Values.WidthScale, Values.StrafeFrontBarGraph_Y + 530 * Values.HeightScale);
            message = "Back Rt";
            spriteBatch.DrawString(BarGraphFont, message, new Vector2(630, 600), Color.White);

            #endregion

            // Done with sprites
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
