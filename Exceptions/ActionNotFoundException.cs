using System;

namespace _20200613_TankLibrary
{
    public class ActionNotFoundException : Exception
    {
        public ActionNotFoundException()
        {            
        }

        public ActionNotFoundException(string message) 
            : base(message)
        {
        }

        public ActionNotFoundException(string message, Exception exp) 
            : base(message, exp)
        {
        }
    }
}
