using System;

namespace _20200613_TankLibrary
{
    public class CreateBulletException : Exception
    {
        public CreateBulletException()
        {
        }

        public CreateBulletException(string message)
            : base(message)
        {
        }

        public CreateBulletException(string message, Exception exp)
            : base(message, exp)
        {
        }
    }
}
