
namespace MMSProjekat
{
    partial class ClampValuesInput
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
            this.minNud = new System.Windows.Forms.NumericUpDown();
            this.minLbl = new System.Windows.Forms.Label();
            this.maxLbl = new System.Windows.Forms.Label();
            this.maxNud = new System.Windows.Forms.NumericUpDown();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNud)).BeginInit();
            this.SuspendLayout();
            // 
            // minNud
            // 
            this.minNud.Location = new System.Drawing.Point(102, 24);
            this.minNud.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.minNud.Name = "minNud";
            this.minNud.Size = new System.Drawing.Size(58, 20);
            this.minNud.TabIndex = 0;
            this.minNud.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // minLbl
            // 
            this.minLbl.AutoSize = true;
            this.minLbl.Location = new System.Drawing.Point(34, 26);
            this.minLbl.Name = "minLbl";
            this.minLbl.Size = new System.Drawing.Size(27, 13);
            this.minLbl.TabIndex = 1;
            this.minLbl.Text = "Min:";
            // 
            // maxLbl
            // 
            this.maxLbl.AutoSize = true;
            this.maxLbl.Location = new System.Drawing.Point(34, 61);
            this.maxLbl.Name = "maxLbl";
            this.maxLbl.Size = new System.Drawing.Size(30, 13);
            this.maxLbl.TabIndex = 3;
            this.maxLbl.Text = "Max:";
            // 
            // maxNud
            // 
            this.maxNud.Location = new System.Drawing.Point(102, 59);
            this.maxNud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxNud.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.maxNud.Name = "maxNud";
            this.maxNud.Size = new System.Drawing.Size(58, 20);
            this.maxNud.TabIndex = 2;
            this.maxNud.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(37, 119);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(50, 23);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(111, 119);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(49, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // ClampValuesInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 154);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.maxLbl);
            this.Controls.Add(this.maxNud);
            this.Controls.Add(this.minLbl);
            this.Controls.Add(this.minNud);
            this.Name = "ClampValuesInput";
            this.Text = "Clamp values";
            ((System.ComponentModel.ISupportInitialize)(this.minNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown minNud;
        private System.Windows.Forms.Label minLbl;
        private System.Windows.Forms.Label maxLbl;
        private System.Windows.Forms.NumericUpDown maxNud;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}