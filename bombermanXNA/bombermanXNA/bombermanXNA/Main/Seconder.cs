using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bomberman.Main
{
    class Seconder
    {
        long lastSec;
        public event EventHandler seconderTrigger;

        void EventHandler(object sender, EventArgs e)
        {

        }

        public Seconder()
        {
            lastSec = DateTime.Now.Second;
            seconderTrigger += EventHandler;
        }

        public void Run()
        {
            if (DateTime.Now.Ticks - lastSec > 10000000)
            {
                seconderTrigger(this, new EventArgs());
                lastSec = DateTime.Now.Ticks;
            }
        }
    }
}
