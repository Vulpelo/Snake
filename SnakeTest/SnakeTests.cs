using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;

namespace SnakeTests
{
    [TestClass]
    public class SnakeTests
    {
        [TestMethod]
        public void PlayerSnake()
        {
            int len = 5;
            PlayerSnake snake = new PlayerSnake(len);
            var a = snake.Segments.Count;

            Assert.AreEqual(len, a, "Not correct amount of segments");
        }

        [TestMethod]
        public void PlayerSnakeSegments()
        {
            int len = 1;
            PlayerSnake snake = new PlayerSnake(len);
            snake.addSegment();
            snake.addSegment();
            var a = snake.Segments.Count;

            Assert.AreEqual(len, a, "Not correct amount of segments");
            Assert.AreEqual(2, snake.newSegments, "Not correct amount of segments");
        }


        [TestMethod]
        public void setMovementDirection1()
        {
            PlayerSnake snake = new PlayerSnake(5);

            snake.MovementDirection = Direction.Left;

            Assert.AreEqual(Direction.Right, snake.MovementDirection, "Wrong new movement direction after choosing opposite direction");
        }

        [TestMethod]
        public void setMovementDirection2()
        {
            PlayerSnake snake = new PlayerSnake(5);

            snake.MovementDirection = Direction.Down;

            Assert.AreEqual(Direction.Down, snake.MovementDirection, "Wrong new movement direction");
        }

        [TestMethod]
        public void setMovementDirection3()
        {
            PlayerSnake snake = new PlayerSnake(5);

            snake.MovementDirection = Direction.Down;
            snake.MovementDirection = Direction.Up;

            snake.updateLastMovementDirection();

            snake.MovementDirection = Direction.Down;

            Assert.AreEqual(Direction.Up, snake.MovementDirection, "Wrong new movement direction");
        }

        [TestMethod]
        public void setMovementDirectionByModel1()
        {
            Model mod = new Model();
            mod.changeDirection('w');
            mod.TheSnake.updateLastMovementDirection();

            Assert.AreEqual(Direction.Up, mod.TheSnake.MovementDirection, "Wrong new movement direction");
        }

        [TestMethod]
        public void setMovementDirectionByModel2()
        {
            Model mod = new Model();
            mod.changeDirection('a');
            mod.TheSnake.updateLastMovementDirection();

            Assert.AreEqual(Direction.Right, mod.TheSnake.MovementDirection, "Wrong new movement direction");
        }

        [TestMethod]
        public void Model_defaultValues()
        {
            Model mod = new Model();
            mod.changeDirection('w');
            mod.TheSnake.updateLastMovementDirection();

            mod.defaultValues();
            Assert.AreEqual(Direction.Right, mod.TheSnake.MovementDirection, "Wrong new movement direction");
        }
    }
}
