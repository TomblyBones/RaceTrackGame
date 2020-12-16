namespace RaceTrackPrj
{
    partial class frmGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlGameGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlGameGrid
            // 
            this.pnlGameGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGameGrid.Name = "pnlGameGrid";
            this.pnlGameGrid.Size = new System.Drawing.Size(1200, 800);
            this.pnlGameGrid.TabIndex = 0;
            this.pnlGameGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGameGrid_Paint);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnlGameGrid);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1216, 839);
            this.MinimumSize = new System.Drawing.Size(1216, 839);
            this.Name = "frmGame";
            this.ShowIcon = false;
            this.Text = "Race Track Game";
            this.Load += new System.EventHandler(this.frmGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGame_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlGameGrid;
        
    }
}

