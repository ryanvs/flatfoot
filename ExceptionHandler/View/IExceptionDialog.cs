using System.Windows.Forms;

namespace ExceptionHandler.View
{
    public interface IExceptionDialog
    {
        void FormShowDialog();
        string Text { get; set; }
        RichTextBox ErrorBox { get; }
        RichTextBox ScopeBox { get; }
        RichTextBox ActionBox { get; }
        Button Button3 { get; }
        Button Button2 { get; }
        Button Button1 { get; }
        IButtonControl FormAcceptButton { set; }
        PictureBox PictureBox1 { get; }
        TextBox TxtMore { get; }
    }
}