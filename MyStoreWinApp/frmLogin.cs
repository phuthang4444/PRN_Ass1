using BusinessObject;
using DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        string ConnectionString = "Server(local);uid=sa;pwd=myPassw0rd;database=MemberManagement;Encrypt=False";
        public frmLogin()
        {
            InitializeComponent();
        }

        // Create a Dataset to store data
        DataSet dsMemberManagement = new DataSet();

        private void frmLogin_Load(object sender, EventArgs e) {
            
            string SQL = "SELECT MemberID, MemberName FROM Member";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SQL, ConnectionString);
                dataAdapter.Fill(dsMemberManagement);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get Data From Database");
            }
        }
        private void btnLogin_Click(object sender, EventArgs e) {
            try
            {
                MemberDAO memberDAO = new MemberDAO();
                string email = txtEmail.Text;
                string password = txtPassword.Text;
                MemberObject member = memberDAO.GetLoginMember(email, password);
                if (member == null)
                {
                    MessageBox.Show("This account is not exist");
                }
                else
                {
                    frmMemberManagement frmMemberManagement = new frmMemberManagement();
                    frmMemberManagement.ShowDialog();
                    Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Login function");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
