using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using Server_Manager.Config;
using Server_Manager.Theme;

namespace Server_Manager
{
    public partial class ConfigEditorForm : Form
    {
        private List<ServerConfig> servers = new();

        public ConfigEditorForm()
        {
            InitializeComponent();

            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Host", "Host / URL");
            dataGridView1.Columns.Add("Port", "Port");
            dataGridView1.Columns.Add("User", "User");
            dataGridView1.Columns.Add("Mode", "Mode");
            dataGridView1.CellDoubleClick += (s, e) => EditSelected();

            btnAdd.Click += (s, e) => AddServer();
            btnEdit.Click += (s, e) => EditSelected();
            btnDelete.Click += (s, e) => DeleteSelected();
            btnSave.Click += (s, e) => SaveAndClose();
            btnCancel.Click += (s, e) => Close();

            ThemeManager.ApplyTo(this);

            LoadFromFile();
        }

        private void ConfigEditorForm_Load(object sender, EventArgs e) { }

        private void LoadFromFile()
        {
            if (!ServerConfigStore.TryLoad(out servers, out var error))
            {
                MessageBox.Show($"Loading error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var s in servers)
            {
                string hostOrUrl = !string.IsNullOrWhiteSpace(s.Url) ? s.Url : s.Host;
                dataGridView1.Rows.Add(s.Name, hostOrUrl, s.Port, s.User, s.Mode);
            }
        }

        private void AddServer()
        {
            var dlg = new ServerEditForm(new ServerConfig());
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                servers.Add(dlg.Result);
                RefreshGrid();
            }
        }

        private void EditSelected()
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0) return;
            int idx = dataGridView1.CurrentRow.Index;

            var copy = JsonSerializer.Deserialize<ServerConfig>(
                JsonSerializer.Serialize(servers[idx]))!;

            var dlg = new ServerEditForm(copy);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                servers[idx] = dlg.Result;
                RefreshGrid();
                dataGridView1.Rows[idx].Selected = true;
            }
        }

        private void DeleteSelected()
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0) return;
            int idx = dataGridView1.CurrentRow.Index;
            var name = servers[idx].Name;
            if (MessageBox.Show($"Delete \"{name}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                servers.RemoveAt(idx);
                RefreshGrid();
            }
        }

        private void SaveAndClose()
        {
            if (!ServerConfigStore.TrySave(servers, out var error))
            {
                MessageBox.Show($"Error when saving the config: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
