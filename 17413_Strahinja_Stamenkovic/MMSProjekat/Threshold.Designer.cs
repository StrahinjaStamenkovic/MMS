
namespace MMSProjekat
{
    partial class Threshold
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
            this.thresholdNud = new System.Windows.Forms.NumericUpDown();
            this.thresholdLbl = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNud)).BeginInit();
            this.SuspendLayout();
            // 
            // thresholdNud
            // 
            this.thresholdNud.Location = new System.Drawing.Point(105, 12);
            this.thresholdNud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.thresholdNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thresholdNud.Name = "thresholdNud";
            this.thresholdNud.Size = new System.Drawing.Size(120, 23);
            this.thresholdNud.TabIndex = 0;
            this.thresholdNud.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // thresholdLbl
            // 
            this.thresholdLbl.AutoSize = true;
            this.thresholdLbl.Location = new System.Drawing.Point(12, 14);
            this.thresholdLbl.Name = "thresholdLbl";
            this.thresholdLbl.Size = new System.Drawing.Size(62, 15);
            this.thresholdLbl.TabIndex = 1;
            this.thresholdLbl.Text = "Threshold:";
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(33, 59);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(128, 59);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // Threshold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 94);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.thresholdLbl);
            this.Controls.Add(this.thresholdNud);
            this.Name = "Threshold";
            this.Text = "Threshold";
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown thresholdNud;
        private System.Windows.Forms.Label thresholdLbl;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}