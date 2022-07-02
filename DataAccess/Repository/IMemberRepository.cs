using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository {
    internal interface IMemberRepository {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int id);
        void InsertMember (MemberObject member);
        void DeleteMember (int id);
        void UpdateMember (MemberObject member);
    }
}
