using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessObject;
using System.Linq;

namespace MyStoreWinApp {
    public partial class frmMemberManagement : Form {
        public frmMemberManagement() {
            InitializeComponent();
        }
        IMemberRepository memberRepository = new MemberRepository();
        // Create a data source
        BindingSource source;
        private void frmMemberManagement_Load(object sender, EventArgs e) {
            btnRemove.Enabled = false;
            // Register this event to open the frmMemberManagement form that performs updating
            dgvMember.CellDoubleClick += DgvCarList_CellDoubleClick;
            /*List<MemberObject> list = new List<MemberObject>();
            SqlConnection connection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand("SELECT MemberID, MemberName, Email, City, Country FROM tblMember", connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            if (reader.HasRows == true) {
                while (reader.Read()) {
                    list.Add(new MemberObject {
                        MemberID = reader.GetInt32("MemberID"),
                        MemberName = reader.GetString("MemberName"),
                        Email = reader.GetString("Email"),
                        Password = "***",
                        City = reader.GetString("City"),
                        Country = reader.GetString("Country")
                    });
                }
                dgvMember.DataSource = list;
            }*/
        }

        private void DgvCarList_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            frmAddMember frmAddMember = new frmAddMember {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmAddMember.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
                // Set focus member updated
                source.Position = source.Count - 1;
            }
        }

        // Clear text on TextBoxes
        private void ClearText() {
            txtMemberID.Text = String.Empty;
            txtMemberName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtCity.Text = String.Empty;
            txtCountry.Text = String.Empty;
        }

        private MemberObject GetMemberObject() {
            MemberObject member = null;
            
            try {
                member = new MemberObject {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get Member");
            }

            return member;
        }

        private void LoadMemberList() {
            var members = memberRepository.GetMembers();

            try {
                /*
                 * The BindingSource component is designed to simplify
                 * the process of binding controls to an unserlying data source
                 */
                source = new BindingSource();
                source.DataSource = members;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMember.DataSource = null;
                dgvMember.DataSource = source;
                if (members.Count() == 0) {
                    ClearText();
                    btnRemove.Enabled = false;
                }
                else {
                    btnRemove.Enabled = true;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void btnView_Click(object sender, EventArgs e) {
            LoadMemberList();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void btnNew_Click(object sender, EventArgs e) {
            frmAddMember frmAddMember = new frmAddMember {
                Text = "Add new member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmAddMember.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
                // Set focus car inserted
                source.Position = source.Position - 1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            try {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberID);
                LoadMemberList();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Remove a member");
            }
        }
    }
}
