using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public class InitialisationNewGame : ILoader
    {
        #region ===--- Dataset ---===

        private GameField _field;

        #endregion

        #region ===--- Constructor ---===

        public InitialisationNewGame(GameField field)
        {
            _field = field;
        }

        #endregion

        #region ===--- InitialisationCharactesTank ---===

        public CharacteristicTank lightCharacter = new CharacteristicTank(ConstantValue.HP_LIGHT,
            1, 1, ConstantValue.ATACK_RANGE_LIGHT, ConstantValue.ATACK_DAMAGE_LIGHT,
            SkinTank.Light);

        public CharacteristicTank heavyCharacter = new CharacteristicTank(ConstantValue.HP_HEAVY,
            1, 1, ConstantValue.ATACK_RANGE_HEAVY, ConstantValue.ATACK_DAMAGE_HEAVY,
            SkinTank.Heavy);

        public CharacteristicTank destroyerCharacter = new CharacteristicTank(ConstantValue.HP_DESTROY,
            1, 1, ConstantValue.ATACK_RANGE_DESTROY, ConstantValue.ATACK_DAMAGE_DESTROY,
            SkinTank.Destroy);

        #endregion        

        #region ===--- InitialisationBlocks ---===

        public void BlockConstruction(int strRow, int strCol, int numWidth,
            int numHeight, int shift, SkinBlock skin)
        {
            for (int numBlockC = 0; numBlockC < numWidth; numBlockC++)
            {
                int col = strCol + numBlockC * ConstantValue.HEIGHT_BLOCK * shift;

                for (int numBlockR = 0; numBlockR < numHeight; numBlockR++)
                {
                    int row = strRow + numBlockR * ConstantValue.WIDTH_BLOCK;

                    Coordinate blockCoor = new Coordinate(row, col);

                    switch (skin)
                    {
                        case SkinBlock.Brick:
                            (new BrickBlock(blockCoor, _field)).CreateBlock();
                            break;
                        case SkinBlock.Metal:
                            (new MetalBlock(blockCoor, _field)).CreateBlock();
                            break;
                        case SkinBlock.Grass:
                            (new GrassBlock(blockCoor, _field)).CreateBlock();
                            break;
                        case SkinBlock.Ice:
                            (new IceBlock(blockCoor, _field)).CreateBlock();
                            break;
                    }
                }
            }
        }

        public void CreateAllBlocks()
        {
            BlockConstruction(6, 6, 4, 6, 2, SkinBlock.Brick);
            BlockConstruction(22, 41, 3, 1, 1, SkinBlock.Metal);
            BlockConstruction(6, 56, 4, 6, 2, SkinBlock.Brick);
            BlockConstruction(51, 6, 3, 4, 2, SkinBlock.Brick);
            BlockConstruction(51, 66, 3, 4, 2, SkinBlock.Brick);
            BlockConstruction(41, 1, 1, 1, 1, SkinBlock.Metal);
            BlockConstruction(41, 11, 4, 1, 1, SkinBlock.Brick);
            BlockConstruction(41, 66, 4, 1, 1, SkinBlock.Brick);
            BlockConstruction(41, 94, 1, 1, 1, SkinBlock.Metal);
            BlockConstruction(46, 36, 1, 3, 1, SkinBlock.Brick);
            BlockConstruction(46, 56, 1, 3, 1, SkinBlock.Brick);
            BlockConstruction(51, 41, 3, 1, 1, SkinBlock.Brick);
            BlockConstruction(66, 51, 1, 2, 1, SkinBlock.Brick);
            BlockConstruction(66, 46, 1, 1, 1, SkinBlock.Brick);
            BlockConstruction(66, 41, 1, 1, 1, SkinBlock.Brick);
            BlockConstruction(71, 41, 1, 1, 1, SkinBlock.Brick);
            BlockConstruction(36, 36, 5, 2, 1, SkinBlock.Grass);
            BlockConstruction(36, 11, 4, 1, 1, SkinBlock.Grass);
            BlockConstruction(46, 11, 4, 1, 1, SkinBlock.Grass);
            BlockConstruction(36, 66, 4, 1, 1, SkinBlock.Grass);
            BlockConstruction(46, 66, 4, 1, 1, SkinBlock.Grass);
        }

        #endregion

        #region ===--- InitialisationBase ---===

        public void CreateBase()
        {
            Coordinate initCoordinate = new Coordinate(ConstantValue.POS_ROW_BASE,
                ConstantValue.POS_COL_BASE);
            Base gameBase = new Base(initCoordinate, ColorSkin.Yellow, _field);
            gameBase.CreateBase();
            _field.GameBase = gameBase; 
        }

        #endregion

        #region ===--- InitialisationPlayerTank ---===

        public void CreatePlayer()
        {
            Coordinate initCoordinate = new Coordinate(ConstantValue.PLAYER_POS_ROW,
                ConstantValue.PLAYER_POS_COL);
            PlayerTank play = new PlayerTank(destroyerCharacter, Direction.Up, initCoordinate,
                ColorSkin.White, _field);
            play.CreateTank();
            _field.Player = play;
        }

        #endregion

        #region ===--- InitialisationEnemyTanks ---===

        public void CreateEnemys()
        {
            Coordinate firstEnemyCoor = new Coordinate(ConstantValue.ROW_START_ENEMY,
                ConstantValue.COL_START_ENEMY_1);
            EnemyTank enemy_1 = new EnemyTank(lightCharacter, Direction.Down, firstEnemyCoor, 
                ColorSkin.Gray, _field);
            Coordinate secondEnemyCoor = new Coordinate(ConstantValue.ROW_START_ENEMY,
                ConstantValue.COL_START_ENEMY_2);
            EnemyTank enemy_2 = new EnemyTank(destroyerCharacter, Direction.Down, secondEnemyCoor, 
                ColorSkin.Gray, _field);
            Coordinate thirdEnemyCoor = new Coordinate(ConstantValue.ROW_START_ENEMY, 
                ConstantValue.COL_START_ENEMY_3);
            EnemyTank enemy_3 = new EnemyTank(heavyCharacter, Direction.Down, thirdEnemyCoor,
                ColorSkin.Gray, _field);
            enemy_1.CreateTank();
            enemy_2.CreateTank();
            enemy_3.CreateTank();
            _field.Enemy = enemy_1;
            _field.Enemy = enemy_2;
            _field.Enemy = enemy_3;
        }

        #endregion

        #region ===--- Implementation Interface ---===

        public GameField LoadGame()
        {
            CreatePlayer();
            CreateEnemys();
            CreateBase();
            CreateAllBlocks();

            return _field;
        }

        #endregion
    }
}
