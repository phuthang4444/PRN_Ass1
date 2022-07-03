using System;
using DataAccess.Repository;
using System.Windows.Forms;
using BusinessObject;

namespace MyStoreWinApp {
    public partial class frmAddMember : Form {
        public frmAddMember() {
            InitializeComponent();
        }
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }    // False: Insert, True: Update
        public MemberObject MemberInfo { get; set; }

        private void frmAddMember_Load(object sender, EventArgs e) {
            txtMemberID.Enabled = !InsertOrUpdate;
            if(InsertOrUpdate == true) {    // Update mode
                // Show member to perform updating
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtMemberName.Text = MemberInfo.MemberName;
                txtEmail.Text = MemberInfo.Email;
                txtPassword.Text = MemberInfo.Password;
                txtCity.Text = MemberInfo.City;
                txtCountry.Text = MemberInfo.Country;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            try {
                var member = new MemberObject {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                if(!InsertOrUpdate) {
                    MemberRepository.InsertMember(member);
                }
                else {
                    MemberRepository.UpdateMember(member);
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, InsertOrUpdate==false?"Add a new member":"Update a member");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
