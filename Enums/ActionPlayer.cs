﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200613_TankLibrary
{
    [Flags]
    public enum ActionPlayer
    {
        NoAction = 0x00,
        PressRight = 0x01,
        PressLeft = 0x02,
        PressUp = 0x04,
        PressDown = 0x08,
        PressFire = 0x10,
        PressExit = 0x20,
        PressPause = 0x40,
        PressEnter = 0x80,
        PressSave = 0x100,
        PressLoad = 0x200,
        PressNo = 0x400,
        PressYes = 0x800,
        MoveAction = PressRight | PressLeft | PressUp | PressDown,
        StartAction = PressEnter | PressExit | PressLoad
    }
}
