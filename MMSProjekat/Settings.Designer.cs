
namespace MMSProjekat
{
    partial class Settings
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
            this.info1Lbl = new System.Windows.Forms.Label();
            this.info2Lbl = new System.Windows.Forms.Label();
            this.memorySizeNud = new System.Windows.Forms.NumericUpDown();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.info3Lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.memorySizeNud)).BeginInit();
            this.SuspendLayout();
            // 
            // info1Lbl
            // 
            this.info1Lbl.AutoSize = true;
            this.info1Lbl.Location = new System.Drawing.Point(12, 9);
            this.info1Lbl.Name = "info1Lbl";
            this.info1Lbl.Size = new System.Drawing.Size(262, 15);
            this.info1Lbl.TabIndex = 0;
            this.info1Lbl.Text = "Input the memory size of the undo buffer in MB.";
            // 
            // info2Lbl
            // 
            this.info2Lbl.AutoSize = true;
            this.info2Lbl.Location = new System.Drawing.Point(12, 38);
            this.info2Lbl.Name = "info2Lbl";
            this.info2Lbl.Size = new System.Drawing.Size(147, 15);
            this.info2Lbl.TabIndex = 1;
            this.info2Lbl.Text = "The default value is 200MB";
            // 
            // memorySizeNud
            // 
            this.memorySizeNud.Location = new System.Drawing.Point(12, 104);
            this.memorySizeNud.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.memorySizeNud.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.memorySizeNud.Name = "memorySizeNud";
            this.memorySizeNud.Size = new System.Drawing.Size(120, 23);
            this.memorySizeNud.TabIndex = 2;
            this.memorySizeNud.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(158, 144);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(64, 144);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // info3Lbl
            // 
            this.info3Lbl.AutoSize = true;
            this.info3Lbl.Location = new System.Drawing.Point(12, 72);
            this.info3Lbl.Name = "info3Lbl";
            this.info3Lbl.Size = new System.Drawing.Size(112, 15);
            this.info3Lbl.TabIndex = 5;
            this.info3Lbl.Text = "The current value is ";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 179);
            this.Controls.Add(this.info3Lbl);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.memorySizeNud);
            this.Controls.Add(this.info2Lbl);
            this.Controls.Add(this.info1Lbl);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.memorySizeNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label info1Lbl;
        private System.Windows.Forms.Label info2Lbl;
        private System.Windows.Forms.NumericUpDown memorySizeNud;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label info3Lbl;
    }
}