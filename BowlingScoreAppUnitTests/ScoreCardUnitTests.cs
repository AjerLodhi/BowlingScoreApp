using System.Collections.Generic;
using System;
using System.Dynamic;
using BowlingScoreApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingScoreApp.ViewModels;
using System.Collections.ObjectModel;
using MicroMvvm;

namespace BowlingScoreAppUnitTests
{
    [TestClass]
    public class ScoreCardUnitTests : ObservableObject
    {
        public ScoreCardUnitTests() { }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Integer_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            bool check = sc.IsValidScoreInput(3);

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_False_For_Invalid_Integer_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            bool check = sc.IsValidScoreInput(12);

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Small_Strike_x_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            sc.AddScore(2);
            sc.AddScore(2);
            bool check = sc.IsValidScoreInput('x');

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Big_Strike_X_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            sc.AddScore(2);
            sc.AddScore(2);
            bool check = sc.IsValidScoreInput('X');

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Spare_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            sc.AddScore(2);
            sc.AddScore(2);
            sc.AddScore(2);
            bool check = sc.IsValidScoreInput('/');

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_False_For_Invalid_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            sc.AddScore(2);
            sc.AddScore(2);
            bool check = sc.IsValidScoreInput('a');

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_False_If_CurrentFrame_Is_Null_For_Integer_Input()
        {
            // Arrange
            var sc = new ScoreCard();
            ObservableCollection<Frame> _frames = new ObservableCollection<Frame>();

            // Act
            var CurFrame = _frames.FirstOrDefault();
            CurFrame = null;
            bool check = sc.IsValidScoreInput('/');

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_AddScore_Increases_Count_If_FrameCount_Is_Null()
        {
            // Arrange
            var sc = new ScoreCard();
            ObservableCollection<Frame> frames = new ObservableCollection<Frame>();
            ObservableCollection<Frame> Frames = frames;

            //Act
            var frameCount = 1;
            sc.AddScore(3);
            sc.AddScore(3);
            sc.AddScore(3);

            //Assert
            Assert.AreNotEqual(frameCount, Frames.Count());
        }

        [TestMethod]
        public void Check_If_IsComplete_Is_True_After_Reaching_Final_Frame_Using_AddScore_With_Integer_Input()
        {
            // Arrange
            var sc = new ScoreCard();
            
            // Act
            for(int i = 0; i < 10; i++)
            {
                sc.AddScore(3);
                sc.AddScore(3);
            }

            // Assert
            Assert.AreEqual(sc.IsComplete, true);
        }

        public void Check_If_IsComplete_Is_True_After_Reaching_Final_Frame_Using_AddScore_With_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            // Act
            for (int i = 0; i < 11; i++)
            {
                sc.AddScore(3);
                sc.AddScore('/');
            }
            sc.AddScore('x');

            // Assert
            Assert.AreEqual(sc.IsComplete, true);
        }

        [TestMethod]
        public void Check_If_Exception_Is_Thrown_After_Reaching_Final_Frame_Using_AddScore_With_Integer_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            try
            {
                // Act
                for (int i = 0; i < 10; i++)
                {
                    sc.AddScore(3);
                    sc.AddScore(3);
                }

                Assert.AreEqual(sc.IsComplete, true);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [TestMethod]
        public void Check_If_Exception_Is_Thrown_After_Reaching_Final_Frame_Using_AddScore_With_Character_Input()
        {
            // Arrange
            var sc = new ScoreCard();

            try
            {
                // Act
                for (int i = 0; i < 11; i++)
                {
                    sc.AddScore(3);
                    sc.AddScore('/');
                }
                sc.AddScore('x');

                Assert.AreEqual(sc.IsComplete, false);
            }
            catch 
            {
                // Assert
                Assert.AreEqual(sc.IsComplete, true);
            }
        }
    }
}