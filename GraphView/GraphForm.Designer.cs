﻿namespace GraphView
{
    partial class GraphForm
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
            this.canvasView1 = new GraphView.CanvasView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvasView1)).BeginInit();
            this.SuspendLayout();
            // 
            // canvasView1
            // 
            this.canvasView1.Location = new System.Drawing.Point(130, 12);
            this.canvasView1.Name = "canvasView1";
            this.canvasView1.Size = new System.Drawing.Size(342, 258);
            this.canvasView1.TabIndex = 0;
            this.canvasView1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add Vertex";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Load Vertex";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 282);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.canvasView1);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            ((System.ComponentModel.ISupportInitialize)(this.canvasView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CanvasView canvasView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}