using SharedDataLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupLib;
using WorldCupViewer.Selectables;

namespace WorldCupViewer.UserControls
{
    public partial class CupPlayerDisplay : UserControl, ISelectable
    {
        private bool mouseHovering = false;
        private bool focused = false;
        private bool isSelected = false;

        public readonly CupPlayer associatedPlayer;

        private class PositionCounter
        {
            public String positionName = "";
            public int timesOccured = 0;
        }

        public CupPlayerDisplay(CupPlayer player, bool isFavourite)
        {
            InitializeComponent();
            TabStop = true;

            pbIsFavourite.Image = Image.FromStream(Images.GetStarPngStream());
            SetIsFavourite(isFavourite);

            associatedPlayer = player;

            lblName.Text = associatedPlayer.name;
            lblShirtNumber.Text = associatedPlayer.shirtNumber.ToString();

            int gamesCaptained = 0;

            List<PositionCounter> positionCounterList = new(); ;

            foreach (var match in associatedPlayer.SortedMatches)
            {
                if (match.awayTeamStatistics.StartingElevenPlayers.Union(match.awayTeamStatistics.SubstitutePlayers).Contains(associatedPlayer))
                {
                    if (match.awayTeam.captainList.Contains(associatedPlayer))
                        gamesCaptained += 1;

                    foreach (var posPair in match.awayTeam.playerPositionPairList)
                    {
                        if (posPair.Key == associatedPlayer)
                        {
                            foreach (var posCounter in positionCounterList)
                            {
                                if (posCounter.positionName == posPair.Value)
                                {
                                    posCounter.timesOccured += 1;
                                    goto breakbreak;
                                }
                            }
                            positionCounterList.Add(new() { positionName = posPair.Value, timesOccured = 1 });
                        }
                    }
                breakbreak:;
                }

                if (match.homeTeamStatistics.StartingElevenPlayers.Union(match.homeTeamStatistics.SubstitutePlayers).Contains(associatedPlayer))
                {
                    if (match.homeTeam.captainList.Contains(associatedPlayer))
                        gamesCaptained += 1;

                    foreach (var posPair in match.homeTeam.playerPositionPairList)
                    {
                        if (posPair.Key == associatedPlayer)
                        {
                            foreach (var posCounter in positionCounterList)
                            {
                                if (posCounter.positionName == posPair.Value)
                                {
                                    posCounter.timesOccured += 1;
                                    goto breakbreak;
                                }
                            }
                            positionCounterList.Add(new() { positionName = posPair.Value, timesOccured = 1 });
                        }
                    }
                breakbreak:;
                }
            }

            foreach (var thing in LocalUtils.GetAllControls(this))
            {
                thing.MouseEnter += CupPlayerDisplay_MouseEnter;
                thing.MouseLeave += CupPlayerDisplay_MouseLeave;
                thing.Click += CupPlayerDisplay_Click;
                thing.DoubleClick += CupPlayerDisplay_Click;
            }

            int mostCommonCount = 0;
            int mostCommonPos = -1;

            for (int i = 0; i < positionCounterList.Count; i++)
            {
                if (positionCounterList[i].timesOccured > mostCommonCount)
                {
                    mostCommonPos = i;
                    mostCommonCount = positionCounterList[i].timesOccured;
                }
            }

            if (mostCommonPos < 0)
            {
                lblUsualPosition.Text = "???";
            }
            else
            {
                lblUsualPosition.Text = positionCounterList[mostCommonPos].positionName;
            }

            lblGamesCaptained.Text = gamesCaptained.ToString();
        }

        public void SetIsFavourite(bool isFavourite)
        {
            if (isFavourite)
                pbIsFavourite.Visible = true;
            else
                pbIsFavourite.Visible = false;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            focused = true;
            SetAppropriateBG();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            focused = false;
            SetAppropriateBG();
            base.OnLostFocus(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (isSelected)
                    Deselected();
                else
                    Selected();
            }
            if (e.KeyCode == Keys.Left)
            base.OnKeyDown(e);
        }

        private void CupPlayerDisplay_MouseEnter(object sender, EventArgs e)
        {
            mouseHovering = true;
            SetAppropriateBG();
        }

        private void CupPlayerDisplay_MouseLeave(object sender, EventArgs e)
        {
            mouseHovering = false;
            SetAppropriateBG();
        }

        private void CupPlayerDisplay_Click(object sender, EventArgs e)
        {
            if (SelectablesHandler.IsMouseOffsetTooLarge())
                return;

            if (!isSelected)
                Selected();
            else
                Deselected();

            SetAppropriateBG();
        }

        private void SetDefaultBG()
        {
            this.BackColor = Color.White;
            foreach (var thing in LocalUtils.GetAllControls(this))
                thing.BackColor = Color.White;
        }
        private void SetHoverBG()
        {
            this.BackColor = SelectablesHandler.hoverColor;
            foreach (var thing in LocalUtils.GetAllControls(this))
                thing.BackColor = SelectablesHandler.hoverColor;
        }
        private void SetHoverFocusBG()
        {
            Color tgtColor = Color.FromArgb(255,
                (SelectablesHandler.hoverColor.R + SelectablesHandler.selectedColor.R) / 2,
                (SelectablesHandler.hoverColor.G + SelectablesHandler.selectedColor.G) / 2,
                (SelectablesHandler.hoverColor.B + SelectablesHandler.selectedColor.B) / 2
            );

            this.BackColor = tgtColor;
            foreach (var thing in LocalUtils.GetAllControls(this))
                thing.BackColor = tgtColor;
        }
        private void SetSelectedBG()
        {
            this.BackColor = SelectablesHandler.selectedColor;
            foreach (var thing in LocalUtils.GetAllControls(this))
                thing.BackColor = SelectablesHandler.selectedColor;
        }

        private void SetAppropriateBG()
        {
            if (isSelected && focused)
            {
                SetHoverFocusBG();
                return;
            }
            if (isSelected)
            {
                SetSelectedBG();
                return;
            }
            if (mouseHovering || focused)
            {
                SetHoverBG();
                return;
            }
            SetDefaultBG();
        }

        public void Selected()
        {
            if (this.Parent == null)
                return;

            this.Focus();

            isSelected = true;
            SetAppropriateBG();
            ((ISelectable)this).NotifyHandlerSelfIsSelected(this.Parent);
        }
        public void Deselected()
        {
            isSelected = false;
            SetAppropriateBG();
            ((ISelectable)this).NotifyHandlerSelfIsDeselected(this.Parent);
        }

        public void NotifySelectableIsDeselected()
        {
            Deselected();
        }

        public bool GetIsSelected()
        {
            return isSelected;
        }

        private void changeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            epbProfilePicture.Visible = false;
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CupPlayerDisplay_Click(null, null);
        }
    }
}
