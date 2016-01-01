using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDXTutorial2
{
    class FPSCounter : GameSystem
    {
        Game m_game;
        int m_frameCount;
        TimeSpan m_fpsPrev;
        double m_maxFrame;

        public FPSCounter(Game game) : base(game)
        {
            m_game = game;

            this.Enabled = true;

            game.GameSystems.Add(this);

            m_maxFrame = double.MinValue;
        }

        public override void Update(GameTime gameTime)
        {
            m_frameCount++;

            if (gameTime.ElapsedGameTime.TotalMilliseconds > m_maxFrame)
                m_maxFrame = gameTime.ElapsedGameTime.TotalMilliseconds;

            var time = gameTime.TotalGameTime;

            var diff = time - m_fpsPrev;

            if (diff.TotalMilliseconds >= 1000)
            {
                var fps = m_frameCount / diff.TotalSeconds;

                this.Game.Window.Title = string.Format("{0} frames in {1:F2} ms = {2:F2} fps, max {3:F2} ms", m_frameCount, diff.TotalMilliseconds, fps, m_maxFrame);

                m_frameCount = 0;
                m_maxFrame = double.MinValue;
                m_fpsPrev = time;
            }
        }
    }
}
