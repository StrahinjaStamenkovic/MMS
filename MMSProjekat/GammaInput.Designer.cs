
namespace MMSProjekat
{
    partial class GammaInput
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
            this.infoLbl = new System.Windows.Forms.Label();
            this.redNud = new System.Windows.Forms.NumericUpDown();
            this.greenNud = new System.Windows.Forms.NumericUpDown();
            this.blueNud = new System.Windows.Forms.NumericUpDown();
            this.redLbl = new System.Windows.Forms.Label();
            this.greenLbl = new System.Windows.Forms.Label();
            this.blueLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.redNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueNud)).BeginInit();
            this.SuspendLayout();
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Location = new System.Drawing.Point(34, 9);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(171, 15);
            this.infoLbl.TabIndex = 0;
            this.infoLbl.Text = "Enter values between .2 and 5.0";
            // 
            // redNud
            // 
            this.redNud.DecimalPlaces = 1;
            this.redNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.redNud.Location = new System.Drawing.Point(108, 47);
            this.redNud.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.redNud.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.redNud.Name = "redNud";
            this.redNud.Size = new System.Drawing.Size(63, 23);
            this.redNud.TabIndex = 1;
            this.redNud.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // greenNud
            // 
            this.greenNud.DecimalPlaces = 1;
            this.greenNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.greenNud.Location = new System.Drawing.Point(108, 93);
            this.greenNud.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.greenNud.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.greenNud.Name = "greenNud";
            this.greenNud.Size = new System.Drawing.Size(63, 23);
            this.greenNud.TabIndex = 2;
            this.greenNud.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // blueNud
            // 
            this.blueNud.DecimalPlaces = 1;
            this.blueNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.blueNud.Location = new System.Drawing.Point(108, 145);
            this.blueNud.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.blueNud.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.blueNud.Name = "blueNud";
            this.blueNud.Size = new System.Drawing.Size(63, 23);
            this.blueNud.TabIndex = 3;
            this.blueNud.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // redLbl
            // 
            this.redLbl.AutoSize = true;
            this.redLbl.Location = new System.Drawing.Point(64, 49);
            this.redLbl.Name = "redLbl";
            this.redLbl.Size = new System.Drawing.Size(30, 15);
            this.redLbl.TabIndex = 4;
            this.redLbl.Text = "Red:";
            // 
            // greenLbl
            // 
            this.greenLbl.AutoSize = true;
            this.greenLbl.Location = new System.Drawing.Point(64, 95);
            this.greenLbl.Name = "greenLbl";
            this.greenLbl.Size = new System.Drawing.Size(41, 15);
            this.greenLbl.TabIndex = 5;
            this.greenLbl.Text = "Green:";
            // 
            // blueLbl
            // 
            this.blueLbl.AutoSize = true;
            this.blueLbl.Location = new System.Drawing.Point(64, 147);
            this.blueLbl.Name = "blueLbl";
            this.blueLbl.Size = new System.Drawing.Size(33, 15);
            this.blueLbl.TabIndex = 6;
            this.blueLbl.Text = "Blue:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(130, 206);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(34, 206);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // GammaInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 241);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.blueLbl);
            this.Controls.Add(this.greenLbl);
            this.Controls.Add(this.redLbl);
            this.Controls.Add(this.blueNud);
            this.Controls.Add(this.greenNud);
            this.Controls.Add(this.redNud);
            this.Controls.Add(this.infoLbl);
            this.Name = "GammaInput";
            this.Text = "GammaInput";
            ((System.ComponentModel.ISupportInitialize)(this.redNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.NumericUpDown redNud;
        private System.Windows.Forms.NumericUpDown greenNud;
        private System.Windows.Forms.NumericUpDown blueNud;
        private System.Windows.Forms.Label redLbl;
        private System.Windows.Forms.Label greenLbl;
        private System.Windows.Forms.Label blueLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
    }
}