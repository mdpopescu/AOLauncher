using System.Data;

namespace AOLauncher;

public partial class EditForm : Form
{
    public EditForm(DataTable data)
    {
        InitializeComponent();

        dgvData.DataSource = data;
    }
}