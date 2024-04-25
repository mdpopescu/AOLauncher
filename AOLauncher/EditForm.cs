namespace AOLauncher;

public partial class EditForm : Form
{
    public string Title
    {
        set => lblTitle.Text = value;
    }

    public object DataSource
    {
        set => dgvData.DataSource = value;
    }

    public EditForm()
    {
        InitializeComponent();
    }
}