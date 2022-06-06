// Ajer Lodhi
// Purpose: Holds a Collection of Frames so the score for each frame can be updated based on which frame the user is on.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroMvvm;
using BowlingScoreApp.Models;

namespace BowlingScoreApp.ViewModels
{
    public class ScoreCard : ObservableObject
    {
        public ScoreCard() {}

        #region Variables
        ObservableCollection<Frame> frames = new ObservableCollection<Frame>();
        public bool IsComplete { get; private set; }
        public int Score { get; private set; }
        public ObservableCollection<Frame> Frames { get { return frames; } }
        #endregion Variables

        #region Private Methods

        // Takes our Current frame and adds the most recent score input.
        #region AddScoreToFrame
        private void AddScoreToFrame(int score)
        {
            var CurFrame = frames.LastOrDefault();

            if (CurFrame == null || CurFrame.CurrentState > FState.Second)
            {
                AddFrame();
            }

            foreach (var frame in frames.Where(f => f.CurrentState != FState.Over))
                frame.AddScore(score);

            if (frames.Count >= 10 && frames[9].CurrentState == FState.Over)
                IsComplete = true;

            RaisePropertyChanged(() => Frames);
        }
        #endregion AddScoreToFrame

        // Adds a Frame to Obersrvable Collection of Frames as long as the frame count stays below 10.
        #region AddFrame
        private void AddFrame()
        {
            if (Frames.Count < 10)
                frames.Add(new Frame());
            else
                frames.Add(new Frame(true));
        }
        #endregion AddFrame

        // Gets Score from the current frame we are on
        #region ComputeScore
        private void GetScore()
        {
            Score = frames.Where(f => !f.IsExtension && f.CurrentState == FState.Over).Sum(f => f.Score);
            RaisePropertyChanged(() => Score);
        }
        #endregion ComputeScore

        #endregion Private Methods

        /// <summary>
        /// Check if Scores entered by the user are valid 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        #region Check Input Validity
        public bool IsValidScoreInput(int score)
        {
            var CurFrame = frames.LastOrDefault();

            return !IsComplete && score <= 10 && score >= 0 &&
                (CurFrame == null || CurFrame.CurrentState > FState.Second || CurFrame.IsValidScoreInput(score));
        }

        public bool IsValidScoreInput(char score)
        {
            var CurFrame = frames.LastOrDefault();

            if (CurFrame != null || Frames.Count == 0)
            {
                if (CurFrame == null && (score != 'x' && score != 'X'))
                    return false;
                if (score == 'x' || score == 'X' && (Frames.Count == 0 || CurFrame.CurrentState == FState.First))
                {
                    return IsValidScoreInput(10);

                }
                if (score == 'X' || score == 'x' && CurFrame.CurrentState == FState.First)
                {
                    return IsValidScoreInput(10);

                }
                else if (score == '/' && (Frames.Count == 0 || CurFrame.CurrentState == FState.Second))
                {
                    if (CurFrame == null)
                        return false;
                     if (CurFrame.FirstShot >= 0 && CurFrame.FirstShot <= 9)
                         return IsValidScoreInput(10 - CurFrame.FirstShot);
                }
                else
                    return false;
            }
            return false;
        }
        #endregion Check Input Validity

        /// <summary>
        /// Add Scores into each Frame 
        /// </summary>
        /// <param name="score"></param>
        /// <exception cref="Exception"></exception>
        #region Add Scores
        public void AddScore(int score)
        {
            if (IsComplete)
                throw new Exception("ScoreCard completed!");

            AddScoreToFrame(score);

            GetScore();
        }

        public void AddScore(char score)
        {
            var CurFrame = frames.LastOrDefault();

            if (IsComplete)
                throw new Exception("ScoreCard completed!");

            if (score == 'x' || score == 'X')
                AddScoreToFrame(10);
            else if (score == '/')
                AddScoreToFrame(10 - CurFrame.FirstShot);

            GetScore();
        }
        #endregion Add Scores

    }
}