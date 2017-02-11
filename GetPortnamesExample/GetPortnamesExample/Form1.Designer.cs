namespace GetPortnamesExample
{
   partial class Form1
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
         this.listBoxComports = new System.Windows.Forms.ListBox();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // listBoxComports
         // 
         this.listBoxComports.FormattingEnabled = true;
         this.listBoxComports.ItemHeight = 31;
         this.listBoxComports.Location = new System.Drawing.Point(102, 96);
         this.listBoxComports.Name = "listBoxComports";
         this.listBoxComports.Size = new System.Drawing.Size(589, 624);
         this.listBoxComports.TabIndex = 0;
         this.listBoxComports.SelectedValueChanged += new System.EventHandler(this.listBoxComports_SelectedValueChanged);
         // 
         // button1
         // 
         this.button1.AutoSize = true;
         this.button1.Location = new System.Drawing.Point(102, 35);
         this.button1.Name = "button1";
         this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
         this.button1.Size = new System.Drawing.Size(270, 42);
         this.button1.TabIndex = 1;
         this.button1.Text = "Open Selected port";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(999, 997);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.listBoxComports);
         this.Name = "Form1";
         this.Text = "Form1";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ListBox listBoxComports;
      private System.Windows.Forms.Button button1;
   }
}

