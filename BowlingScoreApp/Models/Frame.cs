// Ajer Lodhi
// Purpose: Creates a Frame model that initializes one out of 10 turns of a bowling match.

using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Models
{

    /// <summary>
    /// The states the frame can be on.
    /// </summary>
    #region Frame States
    public enum FState
    {
        First, // The first attempt of the users frame.
        Second, // The second attempt of the users frame.
        Strike, // If the user gets a strike.
        Spare, // When the user gets a spare
        Over // When the users turn is over 
    }
    #endregion Frame States

    public class Frame : ObservableObject
    {
        #region Variables
        public int FirstShot { get; private set; }
        public int SecondShot { get; private set; }
        public int StrikeShot { get; private set; }
        public int SpareShot { get; private set; }
        public bool IsStrike { get; private set; }
        public bool IsSpare { get; private set; }
        public int Score { get; private set; }
        public FState CurrentState { get; private set; }
        public bool IsExtension { get; private set; } // Whether the values of the score needs to be extended.
        public bool IsOver { get { return CurrentState == FState.Over; } }
        #endregion Variables

        #region Constructors
        public Frame() : this(false) { }

        public Frame(bool isExtension)
        {
            CurrentState = FState.First;
            IsExtension = isExtension;
        }
        #endregion Constructors

        /// <summary>
        /// Checks current state of the frame and changes score and state accordingly.
        /// </summary>
        /// <param name="score"></param>
        /// <exception cref="Exception"></exception>
        #region Private CheckState Method
        private void CheckState(int score)
        {
            switch (CurrentState)
            {
                case FState.First:
                    FirstShot = score;
                    if (score < 10)
                        CurrentState = FState.Second;
                    else
                    {
                        CurrentState = FState.Strike;
                        IsStrike = true;
                    }
                    RaisePropertyChanged(() => FirstShot);
                    break;

                case FState.Second:
                    SecondShot = score;
                    if (FirstShot + SecondShot < 10)
                        CurrentState = FState.Over;
                    else
                    {
                        CurrentState = FState.Spare;
                        IsSpare = true;
                    }
                    RaisePropertyChanged(() => SecondShot);
                    break;

                case FState.Strike:
                    StrikeShot = score;
                    CurrentState = FState.Spare;
                    break;

                case FState.Spare:
                    SpareShot = score;
                    CurrentState = FState.Over;
                    break;

                case FState.Over:
                    throw new Exception("Frame completed!");
            }
            RaisePropertyChanged(() => IsOver);
        }
        #endregion Private Method that Checks current state of the frame and changes score and state accordingly 

        /// <summary>
        /// Checks if Score input is within valid range and is in the correct state
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        #region  IsValidScoreInput
        public bool IsValidScoreInput(int score)
        {
            return score >= 0 && score <= 10 && CurrentState < FState.Strike && FirstShot + score <= 10;
        }
        #endregion  IsValidScoreInput

        /// <summary>
        /// Add Score input by user to Frame and change state according
        /// </summary>
        /// <param name="score"></param>
        /// <exception cref="ArgumentException"></exception>
        #region AddScore
        public void AddScore(int score)
        {
            if (CurrentState < FState.Strike && FirstShot + score > 10)
                throw new ArgumentException("Score must be less than or equal to (10 - FirstShot)");
            if (score > 10 || score < 0)
                throw new ArgumentException("The score should be less than 10 but not negative!");

            CheckState(score);

            if (!IsExtension)
            {
                Score = FirstShot + SecondShot + SpareShot + StrikeShot;
                RaisePropertyChanged(() => Score);
            }
        }
        #endregion AddScore

    }
}