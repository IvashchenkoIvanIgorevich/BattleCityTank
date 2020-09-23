﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class ConstantValue
    {
        public const int TOP_MAIN_MENU = 55;
        public const int MAX_NUM_BULLETS = 100; 
        public const int LEFT_MAIN_MENU = 0;
        public const int LEFT_STAT_MENU = 78;
        public const int TOP_STAT_MENU = 53;
        public const int BUFFER_HEIGHT = 78;
        public const int BUFFER_WIDTH = 100;
        public const int LEFT_AREA = 90;
        public const int TOP_AREA = 50;
        public const int TIME_SLEEP = 10;
        public const int NUM_RND_ACTION = 20;
        public const int SHIFT_NEW_ENEMY = 8;
        public const int NUM_BONUS_TANK = 3;
        public const int NUM_RND_BONUS = 15;
        public const int BONUS_HP = 100;
        public const int BONUS_SPEED = 1;
        public const int BONUS_ATACK_SP = 1;
        public const int BONUS_ATACK_DM = 100;
        public const int NUM_RND_CHARAC = 5;
        public const int SHIFT_POS_COL_NEW_ENEMY = 46;
        public const int START_NUM_ENEMYTANK = 3;
        public const int WIDTH_GAMEFIELD = 100;
        public const int HEIGHT_GAMEFIELD = 77;
        public const int CAPACITY_GAMEBLOCKS = 2475;
        public const int WIDTH_TANK = 5;
        public const int HEIGHT_TANK = 5;
        public const int POS_COL_BASE = 46;
        public const int POS_ROW_BASE = 71;
        public const int WIDTH_BASE = 5;
        public const int HEIGHT_BASE = 5;
        public const int PLAYER_POS_COL = 31;
        public const int PLAYER_POS_ROW = 71;
        public const int HP_LIGHT = 400;
        public const int HP_HEAVY = 1000;
        public const int HP_DESTROY = 700;
        public const int ATACK_RANGE_LIGHT = 30;
        public const int ATACK_RANGE_HEAVY = 40;
        public const int ATACK_RANGE_DESTROY = 30;
        public const int ATACK_DAMAGE_LIGHT = 50;
        public const int ATACK_DAMAGE_HEAVY = 100;
        public const int ATACK_DAMAGE_DESTROY = 200;
        public const int TIME_MOVE_ENEMY = 10;
        public const int TIME_DIRECT_ENEMY = 500;
        public const int TIME_CREATE_BULLET = 200;
        public const int TIME_MOVE_BULLET = 3;
        public const int QUANTITY_ENEMY = 20;
        public const int SHIFT_START_WINDOW = 5;
        public const int CUR_LEFT_PAUSE = 45;
        public const int CUR_TOP_PAUSE = 36;
        public const int CUR_LEFT_STAT = 101;
        public const int CUR_TOP_STAT = 1;
        public const int CUR_TOP_HP = 22;
        public const int WIDTH_RIGHT_BAR = 9;
        public const int CUR_TOP_GAMEPAD = 14;
        public const int TOP_RIGHT_ARROW = 2;
        public const int TOP_LEFT_ARROW = 4;
        public const int TOP_UP_ARROW = 6;
        public const int TOP_DOWN_ARROW = 8;
        public const int TOP_FIRE = 10;
        public const int TOP_LOAD = 12;
        public const int WIDTH_BLOCK = 5;
        public const int HEIGHT_BLOCK = 5;
        public const int NUM_ALL_ENEMY = 10;
        public const int NUM_DIRECTION = 4;
        public const int NUM_RND_ACTION_ENEMY = 3;
        public const int NUM_ENEMY_ON_FIELD = 3;
        public const string PATH_FILE = "..\\..\\ImgTank\\ViewGameField.txt";
        public const string PATH_FILE_LIGHTTANK = "..\\..\\ImgTank\\ViewLightTank.txt";
        public const string PATH_FILE_HEAVYTANK = "..\\..\\ImgTank\\ViewHeavyTank.txt";
        public const string PATH_FILE_DESTROYTANK = "..\\..\\ImgTank\\ViewDestroyTank.txt";
        public const string PATH_FILE_SAVE_GAME = "..\\..\\SaveGames\\Save.dat";
        public const char BRICK_BLOCK = '\u25A0';
        public const char METAL_BLOCK = '\u2588';
        public const char GRASS_BLOCK = '\u2593';
        public const char ICE_BLOCK = '\u2592';
        public const char BLOCK = '\u2588';
        public const char BULLET = '*';
    }
}
