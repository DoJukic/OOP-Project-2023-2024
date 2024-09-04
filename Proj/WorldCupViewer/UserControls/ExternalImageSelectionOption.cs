using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupViewer.Selectables;

namespace WorldCupViewer.UserControls
{
    public partial class ExternalImageSelectionOption : UserControl
    {
        public event Action? OnSelected;

        private bool isFocused = false;
        private bool isMouseHovering = false;
        private bool isSelected = false;

        public ExternalImageSelectionOption(String imgName)
        {
            InitializeComponent();

            lblExternalImageName.Text = imgName;
            epbImage.ExternalImageID = imgName;

            this.TabStop = true;
            this.MouseEnter += MouseEnterEvent;
            this.MouseLeave += MouseLeaveEvent;
            this.Click += ClickedEvent;
            this.DoubleClick += ClickedEvent;

            foreach (var thing in LocalUtils.GetAllControls(this))
            {
                thing.MouseEnter += MouseEnterEvent;
                thing.MouseLeave += MouseLeaveEvent;
                thing.Click += ClickedEvent;
                thing.DoubleClick += ClickedEvent;
            }
        }

        public String GetExternalImageID()
        {
            return epbImage.ExternalImageID ?? "";
        }

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            SetAppropriateBG();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            SetAppropriateBG();
            base.OnLostFocus(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                    Selected();
            }
            if (e.KeyCode == Keys.Left)
                base.OnKeyDown(e);
        }

        private void MouseEnterEvent(object? sender, EventArgs e)
        {
            isMouseHovering = true;
            SetAppropriateBG();
        }

        private void MouseLeaveEvent(object? sender, EventArgs e)
        {
            isMouseHovering = false;
            SetAppropriateBG();
        }

        private void ClickedEvent(object? sender, EventArgs e)
        {
            if (SelectablesHandler.IsMouseOffsetTooLarge())
                return;

            Selected();

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
            if (isSelected && isFocused)
            {
                SetHoverFocusBG();
                return;
            }
            if (isSelected)
            {
                SetSelectedBG();
                return;
            }
            if (isMouseHovering || isFocused)
            {
                SetHoverBG();
                return;
            }
            SetDefaultBG();
        }

        public void Selected()
        {
            this.Focus();

            isSelected = true;
            SetAppropriateBG();

            OnSelected?.Invoke();
        }
        public void Deselected()
        {
            isSelected = false;
            SetAppropriateBG();
        }
    }
}
