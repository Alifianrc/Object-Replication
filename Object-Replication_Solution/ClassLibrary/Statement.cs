using System;

namespace ClassLibrary
{
    [Serializable]
    public class Statement
    {
        public enum State
        {
            LOGIN,
            PLAY
        }
        public State statement;
    }
}
