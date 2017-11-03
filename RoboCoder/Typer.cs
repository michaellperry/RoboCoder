using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboCoder
{
    class Typer
    {
        // Millisecond delay
        private const int MinDelay = 5;
        private const int MaxDelay = 150;

        private static readonly Regex ChunkMatcher = new Regex(@"{[A-Z+{}()%]+}|.");

        public double Speed { get; private set; } = 1;

        public void SetSpeed(double perc)
        {
            var clamped = Math.Max(0, Math.Min(1, perc));

            Speed = clamped;
        }

        public void Type(string instruction)
        {
            if (Math.Abs(Speed - 1) < 0.01)
            {
                SendKeys.SendWait(instruction);
            }
            else
            {
                var chunks = ChunkMatcher.Matches(instruction);
                var delay = (int)Math.Floor((MaxDelay - MinDelay) * (1 - Speed) + MinDelay);

                for (var i = 0; i < chunks.Count; i++)
                {
                    SendKeys.SendWait(chunks[i].Value);
                    Task.Delay(delay).Wait();
                }
            }
        }
    }
}
