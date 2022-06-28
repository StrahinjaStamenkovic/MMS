
namespace MMSProjekat
{
    partial class HueSaturationInput
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.hueLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.useSaturationChx = new System.Windows.Forms.CheckBox();
            this.hueNud = new System.Windows.Forms.NumericUpDown();
            this.saturationNud = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.hueNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationNud)).BeginInit();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(30, 156);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(122, 156);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // hueLbl
            // 
            this.hueLbl.AutoSize = true;
            this.hueLbl.Location = new System.Drawing.Point(42, 28);
            this.hueLbl.Name = "hueLbl";
            this.hueLbl.Size = new System.Drawing.Size(30, 13);
            this.hueLbl.TabIndex = 2;
            this.hueLbl.Text = "Hue:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Saturation:";
            // 
            // useSaturationChx
            // 
            this.useSaturationChx.AutoSize = true;
            this.useSaturationChx.Location = new System.Drawing.Point(45, 62);
            this.useSaturationChx.Name = "useSaturationChx";
            this.useSaturationChx.Size = new System.Drawing.Size(108, 17);
            this.useSaturationChx.TabIndex = 5;
            this.useSaturationChx.Text = "Enable saturation";
            this.useSaturationChx.UseVisualStyleBackColor = true;
            this.useSaturationChx.CheckedChanged += new System.EventHandler(this.useSaturationChx_CheckedChanged);
            // 
            // hueNud
            // 
            this.hueNud.DecimalPlaces = 1;
            this.hueNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.hueNud.Location = new System.Drawing.Point(106, 26);
            this.hueNud.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.hueNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.hueNud.Name = "hueNud";
            this.hueNud.Size = new System.Drawing.Size(80, 20);
            this.hueNud.TabIndex = 6;
            // 
            // saturationNud
            // 
            this.saturationNud.DecimalPlaces = 1;
            this.saturationNud.Enabled = false;
            this.saturationNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.saturationNud.Location = new System.Drawing.Point(106, 96);
            this.saturationNud.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.saturationNud.Name = "saturationNud";
            this.saturationNud.Size = new System.Drawing.Size(80, 20);
            this.saturationNud.TabIndex = 7;
            // 
            // HueSaturationInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 195);
            this.Controls.Add(this.saturationNud);
            this.Controls.Add(this.hueNud);
            this.Controls.Add(this.useSaturationChx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hueLbl);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "HueSaturationInput";
            this.Text = "HueSaturationInput";
            ((System.ComponentModel.ISupportInitialize)(this.hueNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label hueLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox useSaturationChx;
        private System.Windows.Forms.NumericUpDown hueNud;
        private System.Windows.Forms.NumericUpDown saturationNud;
    }
}