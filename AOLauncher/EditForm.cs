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

    public bool HidePassword { get; set; }

    public EditForm()
    {
        InitializeComponent();
    }

    //

    //

    private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (!HidePassword || e is not { ColumnIndex: 1, Value: not null })
            return;

        dgvData.Rows[e.RowIndex].Tag = e.Value;
        e.Value = new string('\u25CF', e.Value.ToString()!.Length);
    }
}