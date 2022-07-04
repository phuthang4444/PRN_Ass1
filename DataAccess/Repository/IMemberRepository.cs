using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository {
    public interface IMemberRepository {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int id);
        void InsertMember (MemberObject member);
        void DeleteMember (int id);
        void UpdateMember (MemberObject member);
    }
}
