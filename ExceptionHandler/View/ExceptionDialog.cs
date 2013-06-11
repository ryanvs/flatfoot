#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ExceptionHandler.View
{
    public class ExceptionDialog : Form, IExceptionDialog
    {
        #region " Windows Form Designer generated code "

        private IContainer components;
        internal Label lblActionHeading;
        internal Label lblErrorHeading;
        internal Label lblMoreHeading;
        internal Label lblScopeHeading;
        private RichTextBox withEventsField_ActionBox;
        private RichTextBox withEventsField_ScopeBox;
        private Button _withEventsFieldButton1;
        private Button _withEventsFieldButton2;
        private Button _withEventsFieldButton3;
        private Button withEventsField_btnMore;

        public ExceptionDialog()
        {
            Load += UserErrorDialog_Load;

            //This call is required by the Windows Form Designer.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
        }

        public Button Button1
        {
            get { return _withEventsFieldButton1; }
            set
            {
                if (_withEventsFieldButton1 != null)
                {
                    _withEventsFieldButton1.Click -= Button1Click;
                }
                _withEventsFieldButton1 = value;
                if (_withEventsFieldButton1 != null)
                {
                    _withEventsFieldButton1.Click += Button1Click;
                }
            }
        }

        public IButtonControl FormAcceptButton
        {
            set { AcceptButton = value; }
        }

        public PictureBox PictureBox1 { get; private set; }
        public TextBox TxtMore { get; private set; }

        public Button Button2
        {
            get { return _withEventsFieldButton2; }
            set
            {
                if (_withEventsFieldButton2 != null)
                {
                    _withEventsFieldButton2.Click -= Button2Click;
                }
                _withEventsFieldButton2 = value;
                if (_withEventsFieldButton2 != null)
                {
                    _withEventsFieldButton2.Click += Button2Click;
                }
            }
        }

        public Button Button3
        {
            get { return _withEventsFieldButton3; }
            set
            {
                if (_withEventsFieldButton3 != null)
                {
                    _withEventsFieldButton3.Click -= Button3Click;
                }
                _withEventsFieldButton3 = value;
                if (_withEventsFieldButton3 != null)
                {
                    _withEventsFieldButton3.Click += Button3Click;
                }
            }
        }

        internal Button btnMore
        {
            get { return withEventsField_btnMore; }
            set
            {
                if (withEventsField_btnMore != null)
                {
                    withEventsField_btnMore.Click -= btnMore_Click;
                }
                withEventsField_btnMore = value;
                if (withEventsField_btnMore != null)
                {
                    withEventsField_btnMore.Click += btnMore_Click;
                }
            }
        }

        public void FormShowDialog()
        {
            ShowDialog();
        }

        public RichTextBox ErrorBox { get; private set; }

        public RichTextBox ScopeBox { get; private set; }

        public RichTextBox ActionBox { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((components != null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            PictureBox1 = new PictureBox();
            lblErrorHeading = new Label();
            ErrorBox = new RichTextBox();
            lblScopeHeading = new Label();
            ScopeBox = new RichTextBox();
            lblActionHeading = new Label();
            ActionBox = new RichTextBox();
            lblMoreHeading = new Label();
            Button1 = new Button();
            Button2 = new Button();
            Button3 = new Button();
            TxtMore = new TextBox();
            btnMore = new Button();
            SuspendLayout();
            //
            //_withEventsFields_PictureBox1
            //
            PictureBox1.Location = new Point(8, 8);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(32, 32);
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            //
            //lblErrorHeading
            //
            lblErrorHeading.AutoSize = true;
            lblErrorHeading.Font = new Font("Tahoma", 8f, FontStyle.Bold);
            lblErrorHeading.Location = new Point(48, 4);
            lblErrorHeading.Name = "lblErrorHeading";
            lblErrorHeading.Size = new Size(91, 16);
            lblErrorHeading.TabIndex = 0;
            lblErrorHeading.Text = "What happened";
            //
            //ErrorBox
            //
            ErrorBox.Anchor = ((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
            ErrorBox.BackColor = SystemColors.Control;
            ErrorBox.BorderStyle = BorderStyle.None;
            ErrorBox.CausesValidation = false;
            ErrorBox.Location = new Point(48, 24);
            ErrorBox.Name = "ErrorBox";
            ErrorBox.ReadOnly = true;
            ErrorBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            ErrorBox.Size = new Size(416, 64);
            ErrorBox.TabIndex = 1;
            ErrorBox.Text = "(error message)";
            //
            //lblScopeHeading
            //
            lblScopeHeading.AutoSize = true;
            lblScopeHeading.Font = new Font("Tahoma", 8f, FontStyle.Bold);
            lblScopeHeading.Location = new Point(8, 92);
            lblScopeHeading.Name = "lblScopeHeading";
            lblScopeHeading.Size = new Size(134, 16);
            lblScopeHeading.TabIndex = 2;
            lblScopeHeading.Text = "How this will affect you";
            //
            //ScopeBox
            //
            ScopeBox.Anchor = ((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
            ScopeBox.BackColor = SystemColors.Control;
            ScopeBox.BorderStyle = BorderStyle.None;
            ScopeBox.CausesValidation = false;
            ScopeBox.Location = new Point(24, 112);
            ScopeBox.Name = "ScopeBox";
            ScopeBox.ReadOnly = true;
            ScopeBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            ScopeBox.Size = new Size(440, 64);
            ScopeBox.TabIndex = 3;
            ScopeBox.Text = "(scope)";
            //
            //lblActionHeading
            //
            lblActionHeading.AutoSize = true;
            lblActionHeading.Font = new Font("Tahoma", 8f, FontStyle.Bold);
            lblActionHeading.Location = new Point(8, 180);
            lblActionHeading.Name = "lblActionHeading";
            lblActionHeading.Size = new Size(143, 16);
            lblActionHeading.TabIndex = 4;
            lblActionHeading.Text = "What you can do about it";
            //
            //ActionBox
            //
            ActionBox.Anchor = ((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
            ActionBox.BackColor = SystemColors.Control;
            ActionBox.BorderStyle = BorderStyle.None;
            ActionBox.CausesValidation = false;
            ActionBox.Location = new Point(24, 200);
            ActionBox.Name = "ActionBox";
            ActionBox.ReadOnly = true;
            ActionBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            ActionBox.Size = new Size(440, 92);
            ActionBox.TabIndex = 5;
            ActionBox.Text = "(action)";
            //
            //lblMoreHeading
            //
            lblMoreHeading.AutoSize = true;
            lblMoreHeading.Font = new Font("Tahoma", 8f, FontStyle.Bold);
            lblMoreHeading.Location = new Point(8, 300);
            lblMoreHeading.Name = "lblMoreHeading";
            lblMoreHeading.TabIndex = 6;
            lblMoreHeading.Text = "More information";
            //
            //Button1
            //
            Button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            Button1.DialogResult = DialogResult.Cancel;
            Button1.Location = new Point(220, 544);
            Button1.Name = "btn1";
            Button1.TabIndex = 9;
            Button1.Text = "Button1";
            //
            //Button2
            //
            Button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            Button2.DialogResult = DialogResult.Cancel;
            Button2.Location = new Point(304, 544);
            Button2.Name = "btn2";
            Button2.TabIndex = 10;
            Button2.Text = "Button2";
            //
            //Button3
            //
            Button3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            Button3.Location = new Point(388, 544);
            Button3.Name = "btn3";
            Button3.TabIndex = 11;
            Button3.Text = "Button3";
            //
            //_withEventsField_TxtMore
            //
            TxtMore.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            TxtMore.CausesValidation = false;
            TxtMore.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Convert.ToByte(0));
            TxtMore.Location = new Point(8, 324);
            TxtMore.Multiline = true;
            TxtMore.Name = "TxtMore";
            TxtMore.ReadOnly = true;
            TxtMore.ScrollBars = ScrollBars.Vertical;
            TxtMore.Size = new Size(456, 212);
            TxtMore.TabIndex = 8;
            TxtMore.Text = "(detailed information, such as exception details)";
            //
            //btnMore
            //
            btnMore.Location = new Point(112, 296);
            btnMore.Name = "btnMore";
            btnMore.Size = new Size(28, 24);
            btnMore.TabIndex = 7;
            btnMore.Text = ">>";
            //
            //ExceptionDialog
            //
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(472, 573);
            Controls.Add(btnMore);
            Controls.Add(TxtMore);
            Controls.Add(Button3);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(lblMoreHeading);
            Controls.Add(lblActionHeading);
            Controls.Add(lblScopeHeading);
            Controls.Add(lblErrorHeading);
            Controls.Add(ActionBox);
            Controls.Add(ScopeBox);
            Controls.Add(ErrorBox);
            Controls.Add(PictureBox1);
            MinimizeBox = false;
            Name = "ExceptionDialog";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GRT UI has encountered a problem";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion " Windows Form Designer generated code "

        private const int IntSpacing = 10;

        //--
        //-- security-safe process.start wrapper
        //--
        private static void LaunchLink(string strUrl)
        {
            try
            {
                Process.Start(strUrl);
            }
            catch (SecurityException)
            {
                //-- do nothing; we can't launch without full trust.
            }
        }

        private static void SizeBox(Control ctl)
        {
            Graphics g = null;
            try
            {
                //-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
                g = Graphics.FromHwnd(ctl.Handle);
                var objSizeF = g.MeasureString(ctl.Text, ctl.Font, new SizeF(ctl.Width, ctl.Height));
                g.Dispose();
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5;
            }
            catch (SecurityException)
            {
                //-- do nothing; we can't set control sizes without full trust
            }
            finally
            {
                if ((g != null))
                    g.Dispose();
            }
        }

        private static DialogResult DetermineDialogResult(string strButtonText)
        {
            const DialogResult returnVal = DialogResult.None;

            //-- strip any accelerator keys we might have
            strButtonText = strButtonText.Replace("&", "");
            switch (strButtonText.ToLower())
            {
                case "abort":
                    return DialogResult.Abort;
                    break;
                case "cancel":
                    return DialogResult.Cancel;
                    break;
                case "ignore":
                    return DialogResult.Ignore;
                    break;
                case "no":
                    return DialogResult.No;
                    break;
                case "none":
                    return DialogResult.None;
                    break;
                case "ok":
                    return DialogResult.OK;
                    break;
                case "retry":
                    return DialogResult.Retry;
                    break;
                case "yes":
                    return DialogResult.Yes;
                    break;
            }
            return returnVal;
        }

        private void Button1Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button1.Text);
        }

        private void Button2Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button2.Text);
        }

        private void Button3Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button3.Text);
        }

        private void UserErrorDialog_Load(Object sender, EventArgs e)
        {
            //-- make sure our window is on top
            TopMost = true;
            TopMost = false;

            //-- More >> has to be expanded
            TxtMore.Anchor = AnchorStyles.None;
            TxtMore.Visible = false;

            //-- size the labels' height to accommodate the amount of text in them
            SizeBox(ScopeBox);
            SizeBox(ActionBox);
            SizeBox(ErrorBox);

            //-- now shift everything up
            lblScopeHeading.Top = ErrorBox.Top + ErrorBox.Height + IntSpacing;
            ScopeBox.Top = lblScopeHeading.Top + lblScopeHeading.Height + IntSpacing;

            lblActionHeading.Top = ScopeBox.Top + ScopeBox.Height + IntSpacing;
            ActionBox.Top = lblActionHeading.Top + lblActionHeading.Height + IntSpacing;

            lblMoreHeading.Top = ActionBox.Top + ActionBox.Height + IntSpacing;
            btnMore.Top = lblMoreHeading.Top - 3;

            Height = btnMore.Top + btnMore.Height + IntSpacing + 45;

            CenterToScreen();
        }

        private void btnMore_Click(Object sender, EventArgs e)
        {
            if (btnMore.Text == ">>")
            {
                Height = Height + 300;
                var with1 = TxtMore;
                with1.Location = new Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + IntSpacing);
                with1.Height = ClientSize.Height - TxtMore.Top - 45;
                with1.Width = ClientSize.Width - 2*IntSpacing;
                with1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                               | AnchorStyles.Left | AnchorStyles.Right;
                with1.Visible = true;
                Button3.Focus();
                btnMore.Text = "<<";
            }
            else
            {
                SuspendLayout();
                btnMore.Text = ">>";
                Height = btnMore.Top + btnMore.Height + IntSpacing + 45;
                TxtMore.Visible = false;
                TxtMore.Anchor = AnchorStyles.None;
                ResumeLayout();
            }
        }
    }
}