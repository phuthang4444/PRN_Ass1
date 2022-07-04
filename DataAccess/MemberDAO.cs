using System;
using System.Collections.Generic;
using System.Data;
using BusinessObject;
using Microsoft.Data.SqlClient;

namespace DataAccess {
    public class MemberDAO : BaseDAL {
        // Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();

        private MemberDAO() { }
        public static MemberDAO Instance {
            get {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<MemberObject> GetMemberList() {
            IDataReader dataReader = null;
            string SQLSelect = "SELECT * FROM tblMember";
            var members = new List<MemberObject>();

            try {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read()) {
                    members.Add(new MemberObject(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetString(4),
                        dataReader.GetString(5)
                    ));
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                dataReader.Close();
                CloseConnection();
            }
            return members;
        }

        public MemberObject GetMemberByID(int id) {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT *\n" +
                "FROM tblMember\n" +
                "WHERE MemberID = @MemberID";
            try {
                var param = dataProvider.CreateParameter("@MemberID", 4, id, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read()) {
                    member = new MemberObject(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetString(4),
                        dataReader.GetString(5)
                    );
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public void AddNew(MemberObject member) {
            try {
                MemberObject mem = GetMemberByID(member.MemberID);
                if (mem == null) {
                    string SQLInsert = "INSERT tblMember\n" +
                        "VALUES(@MemberID, @MemberName, @Email, @Password, @City, @Country)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 50, member.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 50, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 20, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 50, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 50, member.Country, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else {
                    throw new Exception("This member is already exists");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                CloseConnection();
            }
        }

        public void Update(MemberObject member) {
            try {
                MemberObject mem = GetMemberByID(member.MemberID);
                if (mem != null) {
                    string SQLUpdate = "UPDATE tblMember\n" +
                        "SET MemberName = @MemberName" +
                        ", Email = @Email, Password = @Password, City = @City" +
                        ", Country = @Country\n" +
                        "WHERE MemberID = @MemberID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 50, member.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 50, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 20, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 50, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 50, member.Country, DbType.String));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else {
                    throw new Exception("This member does not already exists");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                CloseConnection();
            }
        }

        public void Remove(int memberID) {
            try {
                MemberObject mem = GetMemberByID(memberID);
                if (mem != null) {
                    string SQLDelete = "DELETE tblMember\n" +
                        "WHERE MemberID = @MemberID";
                    var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else {
                    throw new Exception("This member does not already exists");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                CloseConnection();
            }
        }
    }
}
