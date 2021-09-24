using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    public class Play : Statement
    {
        public string roomName;
        public int level;

        public Play()
        {
            statement = State.PLAY;
        }
    }
}
