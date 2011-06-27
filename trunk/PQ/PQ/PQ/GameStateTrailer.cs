using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GameStateTrailer : GameState
    {
        public override GameStateId StateId
        {
            get
            {
                return GameStateId.StateTrailer;
            }
        }
        public GameStateTrailer(MyGame game)
            : base(game)
        {

        }
        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle(0, 0, Game.Width, Game.Height);
            }
        }
        public override List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> list = new List<Rectangle>();
                list.Add(new Rectangle(0, 0, Game.Width, Game.Height));
                return list;
            }
        }
        Video video;
        VideoPlayer player;
        Texture2D videoTexture;
        public override void LoadContent()
        {
            video = this.MyGame.Content.Load<Video>(@"Assets\Puzzle Quest 2");
            player = new VideoPlayer();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (player.State == MediaState.Stopped)
            {
                player.IsLooped = true;
                player.Play(video);
            }
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();
            Rectangle screen = new Rectangle(spriteBatch.GraphicsDevice.Viewport.X,
                                            spriteBatch.GraphicsDevice.Viewport.Y,
                                            spriteBatch.GraphicsDevice.Viewport.Width,
                                            spriteBatch.GraphicsDevice.Viewport.Height);
            if (videoTexture != null)
                spriteBatch.Draw(videoTexture, screen, Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        public override void InitEvents()
        {
            this.MouseDown += new EventHandler<GameMouseEventArgs>(GameStateTrailer_MouseDown);
            this.KeyDown += new EventHandler<GameKeyEventArgs>(GameStateTrailer_KeyDown);
        }

        void GameStateTrailer_KeyDown(object sender, GameKeyEventArgs e)
        {
            this.Game.SwitchState(new GameStateMainMenu(this.Game));
        }

        void GameStateTrailer_MouseDown(object sender, GameMouseEventArgs e)
        {
            this.Game.SwitchState(new GameStateMainMenu(this.Game));
        }
    }
}
