using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ce103_hw5_snake_dll;
namespace ce103_hw5_snake_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void collision_snake_test1()
        {
            Class1 collision = new Class1();
            int[,] snakeXY = new int[2,310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            Assert.AreEqual(false, collision.collisionSnake(29, 3, snakeXY, 9, 1));
        }
        [TestMethod]
        public void collision_snake_test2()
        {
            Class1 collision = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            Assert.AreEqual(false, collision.collisionSnake(41, 3, snakeXY, 7, 0));
        }
        [TestMethod]
        public void collision_snake_test3()
        {
            Class1 collision = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            Assert.AreEqual(false, collision.collisionSnake(62, 18, snakeXY, 12, 1));
        }
        [TestMethod]
        public void eatfood_test_1()
        {
            Class1 eat = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void eatfood_test_2()
        {
            Class1 eat = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 20;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void eatfood_test_3()
        {
            Class1 eat = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 25;
            snakeXY[1, 0] = 15;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void collision_detection_test1()
        {
            Class1 snake = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 55;
            Assert.AreEqual(0, snake.collisionDetection(snakeXY,28,61,6));
        }
        [TestMethod]
        public void collision_detection_test2()
        {
            Class1 snake = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 60;
            snakeXY[1, 0] = 45;
            Assert.AreEqual(0, snake.collisionDetection(snakeXY, 14, 46, 5));
        }
        [TestMethod]
        public void collision_detection_test3()
        {
            Class1 snake = new Class1();
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 50;
            snakeXY[1, 0] = 40;
            Assert.AreEqual(0, snake.collisionDetection(snakeXY, 20, 56, 4));
        }
    }
}
