using System.Collections.Generic;
using System;
using System.Dynamic;
using BowlingScoreApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoreAppUnitTests
{
    [TestClass]
    public class FrameUnitTests
    {
        public FrameUnitTests() { }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            bool check = f.IsValidScoreInput(0);

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_False_For_Invalid_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            bool check = f.IsValidScoreInput(100);

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_AddScore_Changes_State_To_SecondShot_After_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            f.AddScore(3);

            //Assert
            Assert.AreEqual(f.CurrentState, FState.Second);
        }

        [TestMethod]
        public void Check_If_AddScore_Changes_State_To_Over_After_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            f.AddScore(3);
            f.AddScore(3);

            //Assert
            Assert.AreEqual(f.CurrentState, FState.Over);
        }

        [TestMethod]
        public void Check_If_AddScore_Changes_State_To_Strike_After_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            f.AddScore(10);

            //Assert
            Assert.AreEqual(f.CurrentState, FState.Strike);
        }

        [TestMethod]
        public void Check_If_AddScore_Changes_State_To_Spare_After_Input()
        {
            // Arrange
            var f = new Frame();

            // Act
            f.AddScore(2);
            f.AddScore(8);

            //Assert
            Assert.AreEqual(f.CurrentState, FState.Spare);
        }

        [TestMethod]
        public void Check_If_AddScore_Throws_Exception_After_Invalid_Input()
        {
            // Arrange
            var f = new Frame();

            try
            {
                // Act
                f.AddScore(12);
                Assert.Fail("No exception thrown");
            }
            catch(Exception ex)
            {
                // Assert
                Assert.IsTrue(ex is ArgumentException);
            }
           
        }
    }
}