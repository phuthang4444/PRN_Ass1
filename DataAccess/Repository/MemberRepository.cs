﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository {
    public class MemberRepository : IMemberRepository {

        public MemberObject GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);

        public IEnumerable<MemberObject> GetMembers() => MemberDAO.Instance.GetMemberList();

        public void InsertMember(MemberObject member) => MemberDAO.Instance.AddNew(member);
        public void DeleteMember(int id) => MemberDAO.Instance.Remove(id);

        public void UpdateMember(MemberObject member) => MemberDAO.Instance.Update(member);
    }
}
