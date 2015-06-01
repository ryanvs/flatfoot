using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExceptionHandler.View
{
    partial class ExceptionDialog
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
            this._PictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblErrorHeading = new System.Windows.Forms.Label();
            this._ErrorBox = new System.Windows.Forms.RichTextBox();
            this.lblScopeHeading = new System.Windows.Forms.Label();
            this._ScopeBox = new System.Windows.Forms.RichTextBox();
            this.lblActionHeading = new System.Windows.Forms.Label();
            this._ActionBox = new System.Windows.Forms.RichTextBox();
            this.lblMoreHeading = new System.Windows.Forms.Label();
            this._withEventsFieldButton1 = new System.Windows.Forms.Button();
            this._withEventsFieldButton2 = new System.Windows.Forms.Button();
            this._withEventsFieldButton3 = new System.Windows.Forms.Button();
            this._TxtMore = new System.Windows.Forms.TextBox();
            this.withEventsField_btnMore = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _PictureBox1
            // 
            this._PictureBox1.Location = new System.Drawing.Point(10, 9);
            this._PictureBox1.Name = "_PictureBox1";
            this._PictureBox1.Size = new System.Drawing.Size(38, 37);
            this._PictureBox1.TabIndex = 0;
            this._PictureBox1.TabStop = false;
            // 
            // lblErrorHeading
            // 
            this.lblErrorHeading.AutoSize = true;
            this.lblErrorHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblErrorHeading.Location = new System.Drawing.Point(58, 5);
            this.lblErrorHeading.Name = "lblErrorHeading";
            this.lblErrorHeading.Size = new System.Drawing.Size(118, 17);
            this.lblErrorHeading.TabIndex = 0;
            this.lblErrorHeading.Text = "What happened";
            // 
            // _ErrorBox
            // 
            this._ErrorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ErrorBox.BackColor = System.Drawing.SystemColors.Control;
            this._ErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ErrorBox.CausesValidation = false;
            this._ErrorBox.Location = new System.Drawing.Point(58, 28);
            this._ErrorBox.Name = "_ErrorBox";
            this._ErrorBox.ReadOnly = true;
            this._ErrorBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._ErrorBox.Size = new System.Drawing.Size(405, 74);
            this._ErrorBox.TabIndex = 1;
            this._ErrorBox.Text = "(error message)";
            // 
            // lblScopeHeading
            // 
            this.lblScopeHeading.AutoSize = true;
            this.lblScopeHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblScopeHeading.Location = new System.Drawing.Point(10, 106);
            this.lblScopeHeading.Name = "lblScopeHeading";
            this.lblScopeHeading.Size = new System.Drawing.Size(167, 17);
            this.lblScopeHeading.TabIndex = 2;
            this.lblScopeHeading.Text = "How this will affect you";
            // 
            // _ScopeBox
            // 
            this._ScopeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScopeBox.BackColor = System.Drawing.SystemColors.Control;
            this._ScopeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ScopeBox.CausesValidation = false;
            this._ScopeBox.Location = new System.Drawing.Point(29, 129);
            this._ScopeBox.Name = "_ScopeBox";
            this._ScopeBox.ReadOnly = true;
            this._ScopeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._ScopeBox.Size = new System.Drawing.Size(434, 74);
            this._ScopeBox.TabIndex = 3;
            this._ScopeBox.Text = "(scope)";
            // 
            // lblActionHeading
            // 
            this.lblActionHeading.AutoSize = true;
            this.lblActionHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblActionHeading.Location = new System.Drawing.Point(10, 208);
            this.lblActionHeading.Name = "lblActionHeading";
            this.lblActionHeading.Size = new System.Drawing.Size(183, 17);
            this.lblActionHeading.TabIndex = 4;
            this.lblActionHeading.Text = "What you can do about it";
            // 
            // _ActionBox
            // 
            this._ActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ActionBox.BackColor = System.Drawing.SystemColors.Control;
            this._ActionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ActionBox.CausesValidation = false;
            this._ActionBox.Location = new System.Drawing.Point(29, 231);
            this._ActionBox.Name = "_ActionBox";
            this._ActionBox.ReadOnly = true;
            this._ActionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._ActionBox.Size = new System.Drawing.Size(434, 106);
            this._ActionBox.TabIndex = 5;
            this._ActionBox.Text = "(action)";
            // 
            // lblMoreHeading
            // 
            this.lblMoreHeading.AutoSize = true;
            this.lblMoreHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMoreHeading.Location = new System.Drawing.Point(10, 346);
            this.lblMoreHeading.Name = "lblMoreHeading";
            this.lblMoreHeading.Size = new System.Drawing.Size(127, 17);
            this.lblMoreHeading.TabIndex = 6;
            this.lblMoreHeading.Text = "More information";
            // 
            // _withEventsFieldButton1
            // 
            this._withEventsFieldButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._withEventsFieldButton1.Location = new System.Drawing.Point(170, 540);
            this._withEventsFieldButton1.Name = "_withEventsFieldButton1";
            this._withEventsFieldButton1.Size = new System.Drawing.Size(90, 26);
            this._withEventsFieldButton1.TabIndex = 9;
            this._withEventsFieldButton1.Text = "_withEventsFieldButton1";
            this._withEventsFieldButton1.Click += new System.EventHandler(this.Button1Click);
            // 
            // _withEventsFieldButton2
            // 
            this._withEventsFieldButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._withEventsFieldButton2.Location = new System.Drawing.Point(271, 540);
            this._withEventsFieldButton2.Name = "_withEventsFieldButton2";
            this._withEventsFieldButton2.Size = new System.Drawing.Size(90, 26);
            this._withEventsFieldButton2.TabIndex = 10;
            this._withEventsFieldButton2.Text = "_withEventsFieldButton2";
            this._withEventsFieldButton2.Click += new System.EventHandler(this.Button2Click);
            // 
            // _withEventsFieldButton3
            // 
            this._withEventsFieldButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._withEventsFieldButton3.Location = new System.Drawing.Point(372, 540);
            this._withEventsFieldButton3.Name = "_withEventsFieldButton3";
            this._withEventsFieldButton3.Size = new System.Drawing.Size(90, 26);
            this._withEventsFieldButton3.TabIndex = 11;
            this._withEventsFieldButton3.Text = "_withEventsFieldButton3";
            this._withEventsFieldButton3.Click += new System.EventHandler(this.Button3Click);
            // 
            // _TxtMore
            // 
            this._TxtMore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TxtMore.CausesValidation = false;
            this._TxtMore.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._TxtMore.Location = new System.Drawing.Point(10, 374);
            this._TxtMore.Multiline = true;
            this._TxtMore.Name = "_TxtMore";
            this._TxtMore.ReadOnly = true;
            this._TxtMore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._TxtMore.Size = new System.Drawing.Size(453, 156);
            this._TxtMore.TabIndex = 8;
            this._TxtMore.Text = "(detailed information, such as exception details)";
            // 
            // withEventsField_btnMore
            // 
            this.withEventsField_btnMore.Location = new System.Drawing.Point(134, 342);
            this.withEventsField_btnMore.Name = "withEventsField_btnMore";
            this.withEventsField_btnMore.Size = new System.Drawing.Size(34, 27);
            this.withEventsField_btnMore.TabIndex = 7;
            this.withEventsField_btnMore.Text = ">>";
            this.withEventsField_btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // ExceptionDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(472, 573);
            this.Controls.Add(this.withEventsField_btnMore);
            this.Controls.Add(this._TxtMore);
            this.Controls.Add(this._withEventsFieldButton3);
            this.Controls.Add(this._withEventsFieldButton2);
            this.Controls.Add(this._withEventsFieldButton1);
            this.Controls.Add(this.lblMoreHeading);
            this.Controls.Add(this.lblActionHeading);
            this.Controls.Add(this.lblScopeHeading);
            this.Controls.Add(this.lblErrorHeading);
            this.Controls.Add(this._ActionBox);
            this.Controls.Add(this._ScopeBox);
            this.Controls.Add(this._ErrorBox);
            this.Controls.Add(this._PictureBox1);
            this.MinimizeBox = false;
            this.Name = "ExceptionDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "(app) has encountered a problem";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblActionHeading;
        private Label lblErrorHeading;
        private Label lblMoreHeading;
        private Label lblScopeHeading;
        private RichTextBox withEventsField_ActionBox;
        private RichTextBox withEventsField_ScopeBox;
        private Button _withEventsFieldButton1;
        private Button _withEventsFieldButton2;
        private Button _withEventsFieldButton3;
        private Button withEventsField_btnMore;
        private PictureBox _PictureBox1;
        private RichTextBox _ErrorBox;
        private RichTextBox _ScopeBox;
        private RichTextBox _ActionBox;
        private TextBox _TxtMore;
    }
}