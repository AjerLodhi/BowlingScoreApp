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
    public class ScoreViewModelTests : ObservableObject
    {
        public ScoreViewModelTests() { }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_True_For_Valid_Integer_Score()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 4;
            bool check = svm.IsValidScoreInput();

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidScoreInput_Returns_False_For_Invalid_Integer_Score()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 12;
            bool check = svm.IsValidScoreInput();

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_IsValidCharScoreInput_Returns_True_For_Valid_x_Character_CharScore()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.CharScore = 'x';
            bool check = svm.IsValidCharScoreInput();

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidCharScoreInput_Returns_True_For_Valid_X_Character_CharScore()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.ScoreCard.AddScore(2);
            svm.ScoreCard.AddScore(2);
            svm.CharScore = 'X';
            bool check = svm.IsValidCharScoreInput();

            // Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void Check_If_IsValidCharInput_Returns_False_For_Invalid_Character_CharScore()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.CharScore = 's';
            bool check = svm.IsValidCharScoreInput();

            // Assert
            Assert.AreEqual(check, false);
        }

        [TestMethod]
        public void Check_If_AddScore_Works()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 2;
            svm.AddScore();
            svm.AddScore();
            var fs = svm.ScoreCard.Frames.FirstOrDefault().FirstShot;
            var ss = svm.ScoreCard.Frames.FirstOrDefault().SecondShot;
           
            // Assert
            Assert.AreEqual(fs,ss);
        }

        [TestMethod]
        public void Check_If_AddCharScore_Works()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 2;
            svm.AddScore();
            svm.AddScore();
            svm.CharScore = 'x';
            svm.AddCharScore();
            var res = svm.ScoreCard.Frames.FirstOrDefault().Score;

            // Assert
            Assert.AreEqual(4, res);
        }

        [TestMethod]
        public void Check_If_ResetTable_Makes_CurrentFrame_Is_Null()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            var PreAddRes = svm.ScoreCard.Frames.FirstOrDefault();
            svm.Score = 3;
            svm.AddScore();
            svm.AddScore();
            svm.ResetTable.Execute(null);

            var PostAddPlusResetRes = svm.ScoreCard.Frames.FirstOrDefault();

            // Assert
            Assert.AreEqual(PreAddRes, PostAddPlusResetRes);
        }

        [TestMethod]
        public void Check_If_UpdateScore_Updates_FirstShot()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 2;
            svm.AddScore();
            svm.UpdateScore.Execute(null);

            var res = svm.ScoreCard.Frames.FirstOrDefault().FirstShot;

            // Assert
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void Check_If_ResetTable_Makes_UpdateScore_Updates_FirstShot()
        {
            // Arrange
            var svm = new ScoreViewModel();

            // Act
            svm.Score = 2;
            svm.AddScore();
            svm.AddScore();
            svm.CharScore = 'x';
            svm.AddCharScore();
            svm.UpdateCharScore.Execute(null);

            var res = svm.ScoreCard.Frames.FirstOrDefault().FirstShot;

            // Assert
            Assert.AreEqual(2, res);
        }


    }
}